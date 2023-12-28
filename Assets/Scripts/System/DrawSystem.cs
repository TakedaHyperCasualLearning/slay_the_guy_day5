using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem
{
    private List<DrawComponent> drawList = new List<DrawComponent>();
    private List<DeckComponent> deckList = new List<DeckComponent>();

    public DrawSystem(GameEvent gameEvent)
    {
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
        gameEvent.Draw += Draw;
    }

    private List<CardBaseComponent> Draw()
    {
        List<CardBaseComponent> tempCardBaseComponentList = new List<CardBaseComponent>();
        for (int i = 0; i < deckList.Count; i++)
        {
            DeckComponent deckComponent = deckList[i];
            if (deckComponent.CardList.Count <= 0)
            {
                int count = deckComponent.AfterCardList.Count;
                for (int j = 0; j < count; j++)
                {
                    CardBaseComponent cardBaseComponent = deckComponent.AfterCardList[0];
                    deckComponent.CardList.Add(cardBaseComponent);
                    deckComponent.AfterCardList.RemoveAt(0);
                    Debug.Log(cardBaseComponent.Title + "をデッキに追加");
                }
            }

            for (int j = 0; j < drawList[0].DrawCount; j++)
            {
                CardBaseComponent cardBaseComponent = deckComponent.CardList[0];
                tempCardBaseComponentList.Add(cardBaseComponent);
                deckComponent.AfterCardList.Add(cardBaseComponent);
                deckComponent.CardList.RemoveAt(0);
            }
        }
        return tempCardBaseComponentList;
    }

    private void AddComponentList(GameObject gameObject)
    {
        DrawComponent draw = gameObject.GetComponent<DrawComponent>();
        DeckComponent deck = gameObject.GetComponent<DeckComponent>();

        if (draw == null || deck == null) return;

        drawList.Add(draw);
        deckList.Add(deck);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        DrawComponent draw = gameObject.GetComponent<DrawComponent>();
        DeckComponent deck = gameObject.GetComponent<DeckComponent>();

        if (draw == null || deck == null) return;

        drawList.Remove(draw);
        deckList.Remove(deck);
    }
}
