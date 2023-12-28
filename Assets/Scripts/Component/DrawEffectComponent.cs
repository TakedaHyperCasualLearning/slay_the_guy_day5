using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEffectComponent : MonoBehaviour
{
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;
    private Quaternion endRotation = Quaternion.identity;
    private float timer = 0.0f;
    [SerializeField] private float limitTime = 0.0f;

    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public Vector3 EndPosition { get => endPosition; set => endPosition = value; }
    public Quaternion EndRotation { get => endRotation; set => endRotation = value; }
    public float Timer { get => timer; set => timer = value; }
    public float LimitTime { get => limitTime; set => limitTime = value; }
}
