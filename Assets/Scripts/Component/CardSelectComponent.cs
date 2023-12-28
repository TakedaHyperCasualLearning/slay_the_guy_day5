using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectComponent : MonoBehaviour
{
    private Vector3 basePosition = Vector3.zero;
    [SerializeField] private Vector3 positionOffset = Vector3.zero;
    [SerializeField] private float useHeight = 0.0f;
    private Vector3 liftPosition = Vector3.zero;
    [SerializeField] private GameObject attackEffectPrefab = null;
    private GameObject attackEffect = null;
    private Vector3 endPosition = Vector3.zero;
    private List<IEnumerator> attackEffectList = new List<IEnumerator>();
    [SerializeField] private GameObject effectRoot = null;


    public Vector3 BasePosition { get => basePosition; set => basePosition = value; }
    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
    public float UseHeight { get => useHeight; set => useHeight = value; }
    public Vector3 LiftPosition { get => liftPosition; set => liftPosition = value; }
    public GameObject AttackEffectPrefab { get => attackEffectPrefab; set => attackEffectPrefab = value; }
    public GameObject AttackEffect { get => attackEffect; set => attackEffect = value; }
    public Vector3 EndPosition { get => endPosition; set => endPosition = value; }
    public List<IEnumerator> AttackEffectList { get => attackEffectList; set => attackEffectList = value; }
    public GameObject EffectRoot { get => effectRoot; set => effectRoot = value; }
}
