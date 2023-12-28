using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.TextCore.Text;

public class EnemyAttackSystem
{
    private GameEvent gameEvent;
    private GameObject playerObject = null;
    private ObjectPool objectPool;
    private List<CharacterBaseComponent> characterBaseLis = new List<CharacterBaseComponent>();
    private List<TurnComponent> turnList = new List<TurnComponent>();
    private List<EnemyAttackComponent> enemyAttackList = new List<EnemyAttackComponent>();

    public EnemyAttackSystem(GameEvent gameEvent, ObjectPool objectPool, GameObject player)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        playerObject = player;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < turnList.Count; i++)
        {
            TurnComponent turn = turnList[i];
            CharacterBaseComponent characterBase = characterBaseLis[i];


            if (enemyAttackList[i].ShieldEffectList.Count > 0)
            {
                List<int> removeIndex = new List<int>();
                enemyAttackList[i].ShieldEffectList.ForEach(x =>
                {
                    if (x.MoveNext()) return;
                    removeIndex.Add(enemyAttackList[i].ShieldEffectList.IndexOf(x));
                    return;
                });

                if (removeIndex.Count < 0) break;
                foreach (var x in removeIndex)
                {
                    enemyAttackList[i].ShieldEffectList.RemoveAt(x);
                }
            }


            if (!turn.gameObject.activeSelf) continue;

            if (!turn.IsMyTurn || turn.TurnState != TurnState.Play) continue;

            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                DamageComponent damage = playerObject.gameObject.GetComponent<DamageComponent>();
                damage.DamagePoint = characterBase.AttackPoint;
                Debug.Log(turn.gameObject.name + " attack " + playerObject.name);
            }
            else
            {
                characterBase.DefensePoint += 1;
                Debug.Log(turn.gameObject.name + " defense " + playerObject.name);
                GenerateShieldEffect(enemyAttackList[i]);
            }

            turn.TurnState = TurnState.End;
            gameEvent.TurnEnd?.Invoke(turn.gameObject);
        }
    }

    private void GenerateShieldEffect(EnemyAttackComponent enemyAttack)
    {
        GameObject shieldEffect = objectPool.GetGameObject(enemyAttack.ShieldEffectPrefab);
        shieldEffect.transform.position = enemyAttack.transform.position + new Vector3(-1, 1, -1);
        shieldEffect.transform.localScale = new Vector3(1, 1, 1);
        shieldEffect.SetActive(true);
        shieldEffect.transform.parent = enemyAttack.transform;
        ShieldEffectComponent shieldEffectComponent = shieldEffect.GetComponent<ShieldEffectComponent>();
        shieldEffectComponent.ShieldMaterial = shieldEffect.GetComponent<Renderer>().material;
        shieldEffectComponent.ShieldMaterial.SetColor("_Color", new Color(0, 1, 1, 1));
        enemyAttack.ShieldEffectList.Add(ShieldEffect(shieldEffectComponent));
    }

    private IEnumerator ShieldEffect(ShieldEffectComponent shieldEffect)
    {
        while (true)
        {
            shieldEffect.Timer += Time.deltaTime;
            shieldEffect.transform.position += new Vector3(0, 0.05f, 0);
            shieldEffect.ShieldMaterial.SetColor("_Color", new Color(0, 1, 1, 1 - shieldEffect.Timer / shieldEffect.TimerLimit));

            if (shieldEffect.Timer >= shieldEffect.TimerLimit)
            {
                shieldEffect.Timer = 0.0f;
                gameEvent.ReleaseObject?.Invoke(shieldEffect.gameObject);
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        TurnComponent turn = gameObject.GetComponent<TurnComponent>();
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();
        EnemyAttackComponent enemyAttack = gameObject.GetComponent<EnemyAttackComponent>();

        if (turn == null || characterBase == null || enemyAttack == null) return;

        turnList.Add(turn);
        characterBaseLis.Add(characterBase);
        enemyAttackList.Add(enemyAttack);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        TurnComponent turn = gameObject.GetComponent<TurnComponent>();
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();
        EnemyAttackComponent enemyAttack = gameObject.GetComponent<EnemyAttackComponent>();

        if (turn == null || characterBase == null || enemyAttack == null) return;

        turnList.Remove(turn);
        characterBaseLis.Remove(characterBase);
        enemyAttackList.Remove(enemyAttack);
    }
}
