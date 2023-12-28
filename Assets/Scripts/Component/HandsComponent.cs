using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsComponent : MonoBehaviour
{
    [SerializeField] private List<CardBaseComponent> cardList = new List<CardBaseComponent>();
    private List<IEnumerator> drawEffectList = new List<IEnumerator>();
    private float drawEffectTimer = 0.0f;
    [SerializeField] private float drawEffectLimitTime = 0.0f;
    private int drawCount = 0;

    public List<CardBaseComponent> CardList { get => cardList; set => cardList = value; }
    public List<IEnumerator> DrawEffectList { get => drawEffectList; set => drawEffectList = value; }
    public float DrawEffectTimer { get => drawEffectTimer; set => drawEffectTimer = value; }
    public float DrawEffectLimitTime { get => drawEffectLimitTime; set => drawEffectLimitTime = value; }
    public int DrawCount { get => drawCount; set => drawCount = value; }
}
