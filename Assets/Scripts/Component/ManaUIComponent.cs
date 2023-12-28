using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaUIComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaPointText = null;

    public TextMeshProUGUI ManaPointText { get => manaPointText; set => manaPointText = value; }
}
