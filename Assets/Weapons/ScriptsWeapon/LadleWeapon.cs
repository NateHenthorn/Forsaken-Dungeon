using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadleWeapon : PShopItem
{
    // Start is called before the first frame update
    protected override void Start()
    {
        name1 = "Ladle ";
        rarityLevel = 0;
        damageMulitplier = 0;
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