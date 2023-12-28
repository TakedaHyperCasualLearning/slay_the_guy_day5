using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBaseComponent : MonoBehaviour
{
    private int costPoint = 0;
    private int attackPoint = 0;
    private string title = "";
    private string description = "";
    [SerializeField] private TextMeshProUGUI costPointText = null;
    [SerializeField] private TextMeshProUGUI titleText = null;
    [SerializeField] private TextMeshProUGUI descriptionText = null;
    private Vector3 basePosition = Vector3.zero;
    private Quaternion baseRotation = Quaternion.identity;

    public int CostPoint { get => costPoint; set => costPoint = value; }
    public int AttackPoint { get => attackPoint; set => attackPoint = value; }
    public string Title { get => title; set => title = value; }
    public string Description { get => description; set => description = value; }
    public TextMeshProUGUI CostPointText { get => costPointText; set => costPointText = value; }
    public TextMeshProUGUI TitleText { get => titleText; set => titleText = value; }
    public TextMeshProUGUI DescriptionText { get => descriptionText; set => descriptionText = value; }
    public Vector3 BasePosition { get => basePosition; set => basePosition = value; }
    public Quaternion BaseRotation { get => baseRotation; set => baseRotation = value; }
}
