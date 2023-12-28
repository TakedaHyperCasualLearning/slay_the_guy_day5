using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private Movement movement;
    private List<CharacterBaseComponent> characterBaseList = new List<CharacterBaseComponent>();
    private List<DamageComponent> damageList = new List<DamageComponent>();

    public DamageSystem(GameEvent gameEvent, ObjectPool objectPool, Movement movement)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        this.movement = movement;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < characterBaseList.Count; i++)
        {
            CharacterBaseComponent characterBase = characterBaseList[i];
            DamageComponent damage = damageList[i];

            if (!damage.gameObject.activeSelf) continue;

            if (damage.DamageEffectList.Count > 0)
            {
                foreach (var damageEffect in damage.DamageEffectList)
                {
                    if (damageEffect.MoveNext()) continue;
                    damage.DamageEffectList.Remove(damageEffect);
                    break;
                }
            }

            damage.DamagePoint -= characterBase.DefensePoint;
            if (damage.DamagePoint <= 0)
            {
                damage.DamagePoint = 0;
                characterBase.DefensePoint = 0;
                continue;
            };

            characterBase.HitPoint -= damage.DamagePoint;
            GenerateDamageEffect(damage);
            damage.DamagePoint = 0;
            characterBase.DefensePoint = 0;

            if (characterBase.HitPoint <= 0)
            {
                characterBase.HitPoint = 0;
                damage.DamagePoint = 0;
                characterBase.gameObject.SetActive(false);
            }
        }
    }

    private void GenerateDamageEffect(DamageComponent damage)
    {
        GameObject damageEffect = objectPool.GetGameObject(damage.DamageTextPrefab);
        damageEffect.SetActive(true);
        damageEffect.transform.SetParent(damage.transform);
        damageEffect.transform.position = damage.transform.position + damage.PositionOffset;
        damageEffect.GetComponent<TextMeshPro>().text = damage.DamagePoint.ToString();
        DamageEffectComponent damageEffectComponent = damageEffect.GetComponent<DamageEffectComponent>();
        damageEffectComponent.StartPosition = damageEffect.transform.position;
        damageEffectComponent.EndPosition = damage.EndPosition;
        damageEffectComponent.Angle = damage.Angle;
        damage.DamageEffectComponentList.Add(damageEffectComponent);
        damage.DamageEffectList.Add(PlayDamageEffect(damageEffectComponent));
        if (!objectPool.IsNewCreate) return;
        objectPool.IsNewCreate = false;
    }

    private IEnumerator PlayDamageEffect(DamageEffectComponent damageEffect)
    {
        while (true)
        {
            damageEffect.Timer += Time.deltaTime;
            Vector3 tempPosition = damageEffect.transform.position;
            tempPosition = movement.Parabola(damageEffect.StartPosition, damageEffect.EndPosition, damageEffect.Angle, damageEffect.Timer);
            // tempPosition.y += 3.0f * Time.deltaTime;
            damageEffect.transform.position = tempPosition;
            float ratio = damageEffect.Timer / damageEffect.LimitTime;
            damageEffect.GetComponent<TextMeshPro>().color = new Color(1.0f, ratio, ratio, 1.0f - ratio);
            if (damageEffect.Timer >= damageEffect.LimitTime)
            {
                damageEffect.Timer = 0.0f;
                gameEvent.ReleaseObject?.Invoke(damageEffect.gameObject);
                yield break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();
        DamageComponent damage = gameObject.GetComponent<DamageComponent>();

        if (characterBase == null || damage == null) return;

        characterBaseList.Add(characterBase);
        damageList.Add(damage);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();
        DamageComponent damage = gameObject.GetComponent<DamageComponent>();

        if (characterBase == null || damage == null) return;

        characterBaseList.Remove(characterBase);
        damageList.Remove(damage);
    }
}
