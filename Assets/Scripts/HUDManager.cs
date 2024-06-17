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
        HP.text = "" + GameLogs.Instance.playerCurrentHP;
        BP.text = "" + GameLogs.Instance.playerCurrentBP + "";
        AP.text = "" + GameLogs.Instance.playerCurrentAP + "";
        Luck.text = "" + GameLogs.Instance.playerCurrentLuck + "";
        costToLevel.text = "" + GameLogs.Instance.playerCostToLevel + "";
        DMG.text = GameLogs.Instance.playerItemDamage + "";
        Str.text =  GameLogs.Instance.playerStrength + "";
        coins.text = GameLogs.Instance.playerCurrentCoin + "";
        crit.text =  GameLogs.Instance.playerItemCrit + "";
        range.text = GameLogs.Instance.playerItemRange + "";

    }
}