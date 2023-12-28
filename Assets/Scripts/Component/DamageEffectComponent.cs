using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageEffectComponent : MonoBehaviour
{
    private float timer = 0.0f;
    [SerializeField] private float limitTime = 0.0f;
    [SerializeField] private TextMeshPro damageText = null;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;
    private float angle = 0.0f;

    public float Timer { get => timer; set => timer = value; }
    public float LimitTime { get => limitTime; set => limitTime = value; }
    public TextMeshPro DamageText { get => damageText; set => damageText = value; }
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public Vector3 EndPosition { get => endPosition; set => endPosition = value; }
    public float Angle { get => angle; set => angle = value; }
}
