using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffWeapon : PShopItem
{
    protected override void Start()
    {
        isRanged = false;
        range = 2;
        name1 = "Staff ";
        rarityLevel = 1;
        damageMulitplier = 1;
        critChance = 1;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override string setName()
    {
        string value = base.setName();
        return value;
    }
    protected override string pickPrefix(int value)
    {
        string rValue = base.pickPrefix(value);
        return rValue;
    }

    protected override string pickSuffix(int value)
    {
        string rValue = base.pickSuffix(value);
        return rValue;
    }

    protected override void setCost()
    {
        base.setCost();
    }

    protected override void setDamage()
    {
        base.setDamage();
    }
}