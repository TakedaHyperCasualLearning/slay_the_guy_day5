using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : MonoBehaviour
{
    [SerializeField] GameObject shieldEffectPrefab = null;
    private List<IEnumerator> shieldEffectList = new List<IEnumerator>();

    public GameObject ShieldEffectPrefab { get => shieldEffectPrefab; set => shieldEffectPrefab = value; }
    public List<IEnumerator> ShieldEffectList { get => shieldEffectList; set => shieldEffectList = value; }
}
