using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyAttackSystem
{
    private GameEvent gameEvent;
    private GameObject playerObject = null;
    private List<CharacterBaseComponent> characterBaseLis = new List<CharacterBaseComponent>();
    private List<TurnComponent> turnList = new List<TurnComponent>();
    private List<EnemyAttackComponent> enemyAttackList = new List<EnemyAttackComponent>();

    public EnemyAttackSystem(GameEvent gameEvent, GameObject player)
    {
        this.gameEvent = gameEvent;
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
            }

            turn.TurnState = TurnState.End;
            gameEvent.TurnEnd?.Invoke(turn.gameObject);
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
