using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGloves : PArmor
{
    protected override void Start()
    {
        name1 = "Gloves ";
        rarityLevel = 1;
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
        if (nameSuffix != "") { return namePrefix + armorLevel + name1 + "of the " + nameSuffix; }
        return namePrefix + armorLevel + name1 + nameSuffix;
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
