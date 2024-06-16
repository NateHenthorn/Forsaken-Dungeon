using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }
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

    public TextMeshProUGUI HP;
    public TextMeshProUGUI BP;
    public TextMeshProUGUI AP;
    public TextMeshProUGUI Luck;
    public TextMeshProUGUI costToLevel;
    public TextMeshProUGUI DMG;
    public TextMeshProUGUI Str;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI crit;
    public TextMeshProUGUI range;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HP.text = "HP: " + GameLogs.Instance.playerCurrentHP;
        BP.text = "BP: " + GameLogs.Instance.playerCurrentBP + "";
        AP.text = "AP: " + GameLogs.Instance.playerCurrentAP + "";
        Luck.text = "Luck: " + GameLogs.Instance.playerCurrentLuck + "";
        costToLevel.text = "Cost to Level: " + GameLogs.Instance.playerCostToLevel + "";
        DMG.text = "Damage: " + GameLogs.Instance.playerItemDamage + "";
        Str.text = "Strength: " + GameLogs.Instance.playerStrength + "";
        coins.text = "Coins: " + GameLogs.Instance.playerCurrentCoin + "";
        crit.text = "Crit: " + GameLogs.Instance.playerItemCrit + "";
        range.text = "Range: " + GameLogs.Instance.playerItemRange + "";

    }
}