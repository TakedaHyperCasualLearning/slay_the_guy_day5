using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckComponent : MonoBehaviour
{
    private List<CardBaseComponent> cardList = new List<CardBaseComponent>();
    private List<CardBaseComponent> afterCardList = new List<CardBaseComponent>();
    [SerializeField] private int deckCount = 0;
    [SerializeField] private TextMeshProUGUI deckCountText = null;

    public List<CardBaseComponent> CardList { get => cardList; set => cardList = value; }
    public List<CardBaseComponent> AfterCardList { get => afterCardList; set => afterCardList = value; }
    public int DeckCount { get => deckCount; set => deckCount = value; }
    public TextMeshProUGUI DeckCountText { get => deckCountText; set => deckCountText = value; }
}
