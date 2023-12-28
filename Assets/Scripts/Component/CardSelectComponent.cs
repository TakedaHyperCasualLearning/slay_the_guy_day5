using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectComponent : MonoBehaviour
{
    private Vector3 basePosition = Vector3.zero;
    [SerializeField] private Vector3 positionOffset = Vector3.zero;
    [SerializeField] private float useHeight = 0.0f;
    private Vector3 liftPosition = Vector3.zero;

    public Vector3 BasePosition { get => basePosition; set => basePosition = value; }
    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
    public float UseHeight { get => useHeight; set => useHeight = value; }
    public Vector3 LiftPosition { get => liftPosition; set => liftPosition = value; }
}
