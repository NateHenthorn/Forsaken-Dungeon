using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PArmor : PShopItem
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        setRarity();
        baseName = name1;
        setCost();
        setArmor();
        preNum = Random.Range(0, 15);
        postNum = Random.Range(0, 50);
        namePrefix = pickPrefix(preNum);
        nameSuffix = pickSuffix(postNum);
        setArmorLevel();
        name1 = setName();
        setSpecialMove();
        setDescription();
        setRarity();
    }
    protected override void setDescription()
    {
        description = "Armor Type: " + getArmorType() + ", Protection Level: " + protectionLevel + ", Special Ability: " + specialMove;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override string pickPrefix(int value)
    {
        string rValue = base.pickPrefix(value);
        return rValue;
    }

    protected virtual void setArmor()
    {
        protectionLevel = Random.Range(1, (rarityLevel * 2));
    }

    protected virtual string getArmorType()
    {
        armorType = getDamageType();
        if (armorType == "None") { armorType = "Basic "; }
        return armorType;
    }
    public virtual void setRarity()
    {
        rarityLevel = Random.Range(1, 7);
    }
    public virtual void setArmorLevel()
    {
        int armorNum = Random.Range(0 + rarityLevel, rarityLevel * 2)/2;
        switch (armorNum)
        {
            case < 3: armorLevel = "Light "; break;
            case < 5: armorLevel = "Medium "; break;
            case < 8: armorLevel = "Heavy "; break;
            default: armorLevel = "Light "; break;
        }
    }

    protected override string pickSuffix(int value)
    {
        string rValue = "";
        abilityNum = value;
        switch (value)
        {
            case 0: ability = "Metal "; rValue = subMetal(); break;
            case 1: ability = "Broken "; rValue = subBroken(); break;
            case 2: ability = "People "; rValue = subPeople(); break;
            case 3: ability = "Royal "; rValue = subRoyal(); break;
            case 4: ability = "Godly "; rValue = subGodly(); break;
            case 5: ability = "Wood "; rValue = subWoods(); break;
            case 6: ability = "Magic "; rValue = subMagic(); break;
            case 7: ability = "Better "; rValue = subBetter(); break;
            case 8: ability = "Color "; rValue = subColor(); break;
            case 9: ability = "Liquid "; rValue = subLiquid(); break;
            case 10: ability = "Ghost "; rValue = subGhost(); break;
            case 11: ability = "Blood "; rValue = subBlood(); break;
            case 12: ability = "Fire "; rValue = subFire(); break;
            case 13: ability = "Acid "; rValue = subAcid(); break;
            case 14: ability = "Toxic "; rValue = subToxic(); break;
            case 15: ability = "Kingdom "; rValue = subKingdom(); break;
            case 16: ability = "Two-Handed "; rValue = subTwo(); break;
            case 17: ability = "Savage "; rValue = subSavage(); break;
            case 18: ability = "Levels "; rValue = subLevels(); break;
            case 19: ability = "Unholy "; rValue = subUnholy(); break;
            case 20: ability = "Grim "; rValue = subGrim(); break;
            case 21: ability = "Brawl "; rValue = subBrawl(); break;
            case 22: ability = "TwinBlades "; rValue = subTwinBlades(); break;
            case 23: ability = "Cold "; rValue = subCold(); break;
            case 24: ability = "Petrify "; rValue = subPetrify(); break;
            case 25: ability = "Stunned "; rValue = subStun(); break;

            default: ability = ""; break;
        }
        return rValue;

    }
}
