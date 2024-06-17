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
    }

    // Update is called once per frame
    void Update()
    {

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
        BaseDmg.text = enemy.damage + "";

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
}
