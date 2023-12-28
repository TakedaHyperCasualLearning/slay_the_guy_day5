using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSystem
{
    private List<DeckComponent> deckList = new List<DeckComponent>();

    public DeckSystem(GameEvent gameEvent)
    {
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(DeckComponent deck)
    {
        for (int i = 0; i < deck.DeckCount; i++)
        {
            deck.CardList.Add(new CardBaseComponent());
            deck.CardList[i].Title = "Card" + i;
            deck.CardList[i].CostPoint = Random.Range(1, 3);
            deck.CardList[i].AttackPoint = Random.Range(1, 3);
            deck.CardList[i].Description = deck.CardList[i].AttackPoint + "Damage";

            Debug.Log(deck.CardList[i].Title + "をデッキに追加");
        }

        // 整数 n の初期値はデッキの枚数
        int n = deck.DeckCount;

        // nが1より小さくなるまで繰り返す
        while (n > 1)
        {
            n--;

            // kは 0 ～ n+1 の間のランダムな値
            int k = UnityEngine.Random.Range(0, n + 1);

            // k番目のカードをtempに代入
            CardBaseComponent temp = deck.CardList[k];
            deck.CardList[k] = deck.CardList[n];
            deck.CardList[n] = temp;
        }
    }

    public void OnUpdate()
    {
        for (int i = 0; i < deckList.Count; i++)
        {
            DeckComponent deck = deckList[i];
            if (!deck.gameObject.activeSelf) continue;

            deck.DeckCountText.text = deck.CardList.Count.ToString();
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        DeckComponent deck = gameObject.GetComponent<DeckComponent>();

        if (deck == null) return;

        deckList.Add(deck);

        Initialize(deck);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        DeckComponent deck = gameObject.GetComponent<DeckComponent>();

        if (deck == null) return;

        deckList.Remove(deck);
    }
}
