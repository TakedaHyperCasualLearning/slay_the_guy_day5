using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseComponent : MonoBehaviour
{
    private int hitPoint = 0;
    [SerializeField] private int hitPointMax = 0;
    [SerializeField] private int attackPoint = 0;
    private int defensePoint = 0;
    private int manaPoint = 0;
    [SerializeField] private int manaPointMax = 0;

    public int HitPoint { get => hitPoint; set => hitPoint = value; }
    public int HitPointMax { get => hitPointMax; set => hitPointMax = value; }
    public int AttackPoint { get => attackPoint; set => attackPoint = value; }
    public int DefensePoint { get => defensePoint; set => defensePoint = value; }
    public int ManaPoint { get => manaPoint; set => manaPoint = value; }
    public int ManaPointMax { get => manaPointMax; set => manaPointMax = value; }
}
