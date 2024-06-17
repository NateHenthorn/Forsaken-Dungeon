using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDImage : MonoBehaviour
{
    // Start is called before the first frame update

    public static HUDImage Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Base Stats
    public TextMeshProUGUI HP;
    public TextMeshProUGUI BP;
    public TextMeshProUGUI MBP;
    public TextMeshProUGUI AP;
    public TextMeshProUGUI DMG;
    public TextMeshProUGUI costToLevel;
    public TextMeshProUGUI range;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI crit;

    //Stats
    public TextMeshProUGUI Str;
    public TextMeshProUGUI Dex;
    public TextMeshProUGUI Int;
    public TextMeshProUGUI Con;
    public TextMeshProUGUI Luck;
    public TextMeshProUGUI Faith;

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
    public TextMeshProUGUI StunChance;


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
