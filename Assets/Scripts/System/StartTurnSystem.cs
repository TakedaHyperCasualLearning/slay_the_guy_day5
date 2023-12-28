using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class StartTurnSystem
{
    private GameObject playerObject;
    private List<TurnComponent> turnList = new List<TurnComponent>();
    private List<CharacterBaseComponent> characterBaseList = new List<CharacterBaseComponent>();

    public StartTurnSystem(GameEvent gameEvent, GameObject player)
    {
        playerObject = player;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }


    public void OnUpdate()
    {
        for (int i = 0; i < turnList.Count; i++)
        {
            TurnComponent turn = turnList[i];
            if (!turn.gameObject.activeSelf) continue;

            if (!turn.IsMyTurn || turn.TurnState != TurnState.Start) continue;

            if (turn.gameObject == playerObject)
            {
                turn.TurnState = TurnState.Draw;
                characterBaseList[i].ManaPoint = characterBaseList[i].ManaPointMax;
                Debug.Log(turn.gameObject.name + "Draw phase");
                continue;
            }

            turn.TurnState = TurnState.Play;
            Debug.Log(turn.gameObject.name + "Battle phase");

        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        TurnComponent turn = gameObject.GetComponent<TurnComponent>();
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();

        if (turn == null || characterBase == null) return;

        turnList.Add(turn);
        characterBaseList.Add(characterBase);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        TurnComponent turn = gameObject.GetComponent<TurnComponent>();
        CharacterBaseComponent characterBase = gameObject.GetComponent<CharacterBaseComponent>();

        if (turn == null || characterBase == null) return;

        turnList.Remove(turn);
        characterBaseList.Remove(characterBase);
    }
}
