using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUISystem
{
    private List<ManaUIComponent> manaUIList = new List<ManaUIComponent>();
    private List<CharacterBaseComponent> characterBaseList = new List<CharacterBaseComponent>();

    public ManaUISystem(GameEvent gameEvent)
    {
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(ManaUIComponent manaUI, CharacterBaseComponent characterBase)
    {
        characterBase.ManaPoint = characterBase.ManaPointMax;
        manaUI.ManaPointText.text = characterBase.ManaPoint.ToString();
    }

    public void OnUpdate()
    {
        for (int i = 0; i < characterBaseList.Count; i++)
        {
            CharacterBaseComponent characterBase = characterBaseList[i];
            ManaUIComponent manaUI = manaUIList[i];
            if (!manaUI.gameObject.activeSelf) continue;

            manaUI.ManaPointText.text = characterBase.ManaPoint.ToString() + "/" + characterBase.ManaPointMax.ToString();
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        ManaUIComponent manaUI = gameObject.GetComponent<ManaUIComponent>();
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();

        if (manaUI == null || characterBase == null) return;

        manaUIList.Add(manaUI);
        characterBaseList.Add(characterBase);

        Initialize(manaUI, characterBase);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        ManaUIComponent manaUI = gameObject.GetComponent<ManaUIComponent>();
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();

        if (manaUI == null || characterBase == null) return;

        manaUIList.Remove(manaUI);
        characterBaseList.Remove(characterBase);
    }

}
