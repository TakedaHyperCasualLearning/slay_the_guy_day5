using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TurnState
{
    None,
    Start,
    Draw,
    Play,
    End,
}

public class TurnSystem
{
    private GameObject playerObject;
    private List<TurnComponent> turnList = new List<TurnComponent>();

    public TurnSystem(GameEvent gameEvent, GameObject player)
    {
        playerObject = player;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(TurnComponent turn)
    {
        if (turn.gameObject == playerObject)
        {
            turn.IsMyTurn = true;
            turn.TurnState = TurnState.Start;
            return;
        }

        turn.IsMyTurn = false;
        turn.TurnState = TurnState.None;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < turnList.Count; i++)
        {
            TurnComponent turn = turnList[i];

            if (!turn.gameObject.activeSelf) continue;

            turn.IsMyTurn = true;
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        TurnComponent turn = gameObject.GetComponent<TurnComponent>();

        if (turn == null) return;

        turnList.Add(turn);

        Initialize(turn);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        TurnComponent turn = gameObject.GetComponent<TurnComponent>();

        if (turn == null) return;

        turnList.Remove(turn);
    }
}
