using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointUISystem
{
    private List<HitPointUIComponent> hitPointUIList = new List<HitPointUIComponent>();
    private List<CharacterBaseComponent> characterBaseList = new List<CharacterBaseComponent>();

    public HitPointUISystem(GameEvent gameEvent)
    {
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(HitPointUIComponent hitPointUI, CharacterBaseComponent characterBase)
    {
        characterBase.HitPoint = characterBase.HitPointMax;
        hitPointUI.HitPointText.text = characterBase.HitPoint.ToString();
    }

    public void OnUpdate()
    {
        for (int i = 0; i < characterBaseList.Count; i++)
        {
            CharacterBaseComponent characterBase = characterBaseList[i];
            HitPointUIComponent hitPointUI = hitPointUIList[i];
            if (!hitPointUI.gameObject.activeSelf) continue;

            hitPointUI.HitPointText.text = characterBase.HitPoint.ToString() + " / " + characterBase.HitPointMax.ToString();
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        HitPointUIComponent hitPointUI = gameObject.GetComponent<HitPointUIComponent>();
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();

        if (hitPointUI == null || characterBase == null) return;

        hitPointUIList.Add(hitPointUI);
        characterBaseList.Add(characterBase);

        Initialize(hitPointUI, characterBase);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        HitPointUIComponent hitPointUI = gameObject.GetComponent<HitPointUIComponent>();
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();

        if (hitPointUI == null || characterBase == null) return;

        hitPointUIList.Remove(hitPointUI);
        characterBaseList.Remove(characterBase);
    }
}
