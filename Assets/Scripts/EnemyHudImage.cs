using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHudImage : MonoBehaviour
{
    //Base Stats
    public TextMeshProUGUI Name1;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI BP;
    public TextMeshProUGUI MBP;
    public TextMeshProUGUI AP;
    public TextMeshProUGUI Initiative;
    public TextMeshProUGUI range;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI BaseDmg;

    //Resistances
    public TextMeshProUGUI FlameRes;
    public TextMeshProUGUI ColdRes;
    public TextMeshProUGUI AcidRes;
    public TextMeshProUGUI ShockRes;

    //Damages
    public TextMeshProUGUI FlameDmg;
    public TextMeshProUGUI ColdDmg;
    public TextMeshProUGUI AcidDmg;
    public TextMeshProUGUI ShockDmg;
    public TextMeshProUGUI BleedDmg;
    public TextMeshProUGUI MagicDmg;
    public TextMeshProUGUI PiercingDmg;

    //Effects
    public TextMeshProUGUI Effect1;
    public TextMeshProUGUI Effect2;
    public TextMeshProUGUI Effect3;


    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "HUD";
        spriteRenderer.sortingOrder = 7;

        // Set Sorting Order for UI elements
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.SetSiblingIndex(7);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
