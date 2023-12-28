using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private GameObject damageTextPrefab = null;
    private List<IEnumerator> damageEffectList = new List<IEnumerator>();
    private List<DamageEffectComponent> damageEffectComponentList = new List<DamageEffectComponent>();
    private int damagePoint = 0;
    [SerializeField] private Vector3 positionOffset = Vector3.zero;
    [SerializeField] private Vector3 endPosition = Vector3.zero;
    [SerializeField] private float angle = 0.0f;

    public GameObject DamageTextPrefab { get => damageTextPrefab; set => damageTextPrefab = value; }
    public List<IEnumerator> DamageEffectList { get => damageEffectList; set => damageEffectList = value; }
    public List<DamageEffectComponent> DamageEffectComponentList { get => damageEffectComponentList; set => damageEffectComponentList = value; }
    public int DamagePoint { get => damagePoint; set => damagePoint = value; }
    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
    public Vector3 EndPosition { get => endPosition; set => endPosition = value; }
    public float Angle { get => angle; set => angle = value; }
}

