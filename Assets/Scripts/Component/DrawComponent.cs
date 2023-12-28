using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawComponent : MonoBehaviour
{
    [SerializeField] private int drawCount = 0;

    public int DrawCount { get => drawCount; set => drawCount = value; }
}
