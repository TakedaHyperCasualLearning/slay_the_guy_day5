using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitPointUIComponent : MonoBehaviour
{
    [SerializeField] private TextMeshPro hitPointText = null;

    public TextMeshPro HitPointText { get => hitPointText; set => hitPointText = value; }
}
