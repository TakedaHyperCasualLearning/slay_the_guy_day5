using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBaseComponent : MonoBehaviour
{
    private float timer = 0.0f;
    [SerializeField] private float timerLimit = 0.0f;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;

    public float Timer { get => timer; set => timer = value; }
    public float TimerLimit { get => timerLimit; set => timerLimit = value; }
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public Vector3 EndPosition { get => endPosition; set => endPosition = value; }
}
