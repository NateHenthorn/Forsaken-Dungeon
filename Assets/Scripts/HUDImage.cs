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
    }

    // Update is called once per frame
    void Update()
    {
        //Set Base Stats
        HP.text = "" + GameLogs.Instance.playerCurrentHP;
        BP.text = "" + GameLogs.Instance.playerCurrentBP + "";
        MBP.text = "" + GameLogs.Instance.playerMagicResist;
        AP.text = "" + GameLogs.Instance.playerCurrentAP + "";
        costToLevel.text = "" + GameLogs.Instance.playerCostToLevel + "";
        DMG.text = GameLogs.Instance.playerItemDamage + "";
        coins.text = GameLogs.Instance.playerCurrentCoin + "";
        crit.text = GameLogs.Instance.playerItemCrit + "";
        range.text = GameLogs.Instance.playerItemRange + "";
        //Set Stats
        Str.text = GameLogs.Instance.playerStrength + "";
        Dex.text = "" + GameLogs.Instance.playerCurrentDex;
        Int.text = "" + GameLogs.Instance.playersCurrentInt;
        Con.text = "" + GameLogs.Instance.playerConstitution;
        Luck.text = "" + GameLogs.Instance.playerCurrentLuck + "";
        Faith.text = "" + GameLogs.Instance.playerCurrentFaith;
        //Set Resistances
        FlameRes.text = "" + GameLogs.Instance.playerFlameResist;
        ColdRes.text = "" + GameLogs.Instance.playerFrozenResist;
        AcidRes.text = "" + GameLogs.Instance.playerAcidResist;
        ShockRes.text = "" + GameLogs.Instance.playerShockResist;
        //Set Damages
        FlameDmg.text = "" + GameLogs.Instance.playerFlameDamage;
        ColdDmg.text = "" + GameLogs.Instance.playerFrozenDamage;
        AcidDmg.text = "" + GameLogs.Instance.playerAcidDamage;
        ShockDmg.text = "" + GameLogs.Instance.playerShockDamage;
        BleedDmg.text = "" + GameLogs.Instance.playerBleedDamage;
        MagicDmg.text = "" + GameLogs.Instance.playerMagicDamage;
        PiercingDmg.text = "" + GameLogs.Instance.playerPiercingDamage;
        StunChance.text = "" + GameLogs.Instance.chanceToStun;
    }
}