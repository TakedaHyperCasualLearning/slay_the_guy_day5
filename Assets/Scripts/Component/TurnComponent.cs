using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnComponent : MonoBehaviour
{
    private bool iisMyTurn = false;
    private TurnState turnState = TurnState.None;

    public bool IsMyTurn { get => iisMyTurn; set => iisMyTurn = value; }
    public TurnState TurnState { get => turnState; set => turnState = value; }
}
