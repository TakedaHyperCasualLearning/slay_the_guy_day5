using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndButtonSystem
{
    private GameEvent gameEvent = null;
    private List<TurnEndButtonComponent> turnEndButtonList = new List<TurnEndButtonComponent>();

    public TurnEndButtonSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(TurnEndButtonComponent turnEndButton)
    {
        turnEndButton.TurnEndButton.onClick.AddListener(() => OnClickTurnEndButton(turnEndButton));
    }

    private void OnClickTurnEndButton(TurnEndButtonComponent turnEndButton)
    {
        Debug.Log("Turn end");
        gameEvent.TurnEnd?.Invoke(turnEndButton.gameObject);
    }

    private void AddComponentList(GameObject gameObject)
    {
        TurnEndButtonComponent turnEndButton = gameObject.GetComponent<TurnEndButtonComponent>();

        if (turnEndButton == null) return;

        turnEndButtonList.Add(turnEndButton);

        Initialize(turnEndButton);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        TurnEndButtonComponent turnEndButton = gameObject.GetComponent<TurnEndButtonComponent>();

        if (turnEndButton == null) return;

        turnEndButtonList.Remove(turnEndButton);
    }
}
