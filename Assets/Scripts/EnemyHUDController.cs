using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUDController : MonoBehaviour
{
    public GameObject hudPrefab; // Reference to the HUD prefab
    public Canvas canvas; // Reference to the Canvas
    public EnemyHudImage hudImage;
    public EnemyHudImage currentHUD; // Reference to the current HUD instance


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
    private void Start()
    {
    Name1 = hudImage.Name1;
    HP = hudImage.HP;
    BP = hudImage.BP;
    MBP = hudImage.MBP;
    AP = hudImage.AP;
    Initiative = hudImage.Initiative;
    range = hudImage.range;
    coins = hudImage.coins;
    BaseDmg = hudImage.BaseDmg;

    //Resistances
    FlameRes = hudImage.FlameRes;
    ColdRes = hudImage.ColdRes;
    AcidRes = hudImage.AcidRes;
    ShockRes = hudImage.ShockRes;

    //Damages
    FlameDmg = hudImage.FlameDmg;
    ColdDmg = hudImage.ColdDmg;
    AcidDmg = hudImage.AcidDmg;
    ShockDmg = hudImage.ShockDmg;
    BleedDmg = hudImage.BleedDmg;
    MagicDmg = hudImage.MagicDmg;
    PiercingDmg = hudImage.PiercingDmg;

    //Effects
    Effect1 = hudImage.Effect1;
    Effect2 = hudImage.Effect2;
    Effect3 = hudImage.Effect3;
        // Ensure the HUD is hidden initially
        if (currentHUD == null)
        {
            currentHUD = Instantiate(hudImage, canvas.transform);
        }
        currentHUD.gameObject.SetActive(false);
    }

    public void ShowHUD(Enemies enemy)
    {
        enemy.enemyHUD = this.hudImage;
        enemy.setHUDStats();
         
        if (currentHUD == null)
        {
            currentHUD = Instantiate(hudImage, canvas.transform);
        }
        currentHUD.setHUDStats(enemy);
        currentHUD.gameObject.SetActive(true);
        //currentHUD.transform.position = new Vector3(Screen.width - 1080, Screen.height - 510, 0); // Adjust this as needed

    }

    public void setHUDStats(Enemies enemy)
    {
         //Base Stats
    Name1.text = enemy.name1;
        HP.text = enemy.hitPoints + "";
        BP.text = enemy.blockPoints + "";
        MBP.text = "" + enemy.magicResistance + "";
        AP.text = "" + enemy.actionPoints;
        Initiative.text = "" + enemy.initiative;
        range.text = "" + enemy.attackRange;
        coins.text = "" + enemy.coins;
        BaseDmg = hudImage.BaseDmg;

        //Resistances
        FlameRes.text = "" + enemy.flameResistance;
        ColdRes.text = "" + enemy.frozenResistance;
        AcidRes.text = "" + enemy.acidResistance;
        ShockRes.text = "" + enemy.shockResistance;

        //Damages
        FlameDmg.text = "" + enemy.flameDamage;
        ColdDmg.text = "" + enemy.frozenDamage;
        AcidDmg.text = "" + enemy.acidDamage;
        ShockDmg.text = "" + enemy.shockDamage;
        BleedDmg.text = "" + enemy.bleedDamage;
        MagicDmg.text = "" + enemy.magicDamage;
        PiercingDmg.text = "" + enemy.piercingDamage;

        //Effects
        Effect1.text = "" + enemy.effect1;
        Effect2.text = "" + enemy.effect2;
        Effect3.text = "" + enemy.effect3;
    }

    public void HideHUD()
    {
        if (currentHUD != null)
        {
            currentHUD.gameObject.SetActive(false);
        }
    }
}