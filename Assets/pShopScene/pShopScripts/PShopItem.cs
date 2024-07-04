using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PShopItem : MonoBehaviour
{
    public int abilityNum = -1;
    public string ability = "";
    public int damageTypeNum = -1;
    public string damageType = "";
    public int handsUsed = 1;
    public int attacks = 1;
    public int cost = 1;
    public string description = "";
    public string name1 = "Item ";
    public string namePrefix = "";
    public string nameSuffix = "";
    public int damage = 1;
    public int range = 1;
    public int preNum = 0;
    public int postNum = 0;
    public int rarityLevel = 0;
    public int damageMulitplier = 0;
    public int critChance = 1;
    public bool isRanged = false;
    public string baseName;
    public int magicDamage = 0;
    public int specialMoveNum = -1;
    public string specialMove = "None ";
    public int acidDamage = 0;
    public int shockDamage = 0;
    public int flameDamage = 0;
    public int toxicDamage = 0;
    public int frozenDamage = 0;
    public int bleedDamage = 0;
    public int piercingDamage = 0;
    public int chanceToPierce = 0;

    public virtual void rStart()
    {
        Start();
    }
    protected virtual void Start()
    {
        baseName = name1;
        setCost();
        setDamage();
        preNum = Random.Range(0, 15);
        postNum = Random.Range(0, 50);
        namePrefix = pickPrefix(preNum);
        nameSuffix = pickSuffix(postNum);
        name1 = setName();
        setSpecialMove();
        setDescription();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    protected virtual void setDescription()
    {
        description = "Range: " + range + ", Damage Type: " + getDamageType() + ", Damage: " + damage + ", CritChance: " + critChance + ", HandsUsed: " + handsUsed + ", Special Move: " + specialMove;
    }

    protected virtual string setName()
    {
        if (nameSuffix != "") { return namePrefix + name1 + "of the " + nameSuffix; }
        return namePrefix + name1 + nameSuffix;
    }
    protected virtual string pickPrefix(int value)
    {
        string rValue = "";
        damageTypeNum = value;
        switch (value)
        {
            case 0: rValue = prePhysical(); break;
            case 1: rValue = preMagic(); break;
            case 2: rValue = preFire(); break;
            case 3: rValue = preLightning(); break;
            case 4: rValue = preBleed(); break;
            case 5: rValue = preCold(); break;
            case 6: rValue = preAcid(); break;
            case 7: rValue = prePetrify(); break;
            case 8: rValue = preToxic(); break;
            case 9: rValue = preStun(); break;
            case 10: rValue = preIllusion(); break;

            default: rValue = ""; break;
        }
        damageType = rValue;

        return rValue;

    }

    protected virtual string pickSuffix(int value)
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

    protected virtual string prePhysical()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 25: value = "Quick "; specialMoveNum = 27; break;
            case < 50: value = "Stancing "; specialMoveNum = 35; break;
            case < 75: value = "Piercing "; specialMoveNum = 27; break;
            case <= 100: value = "Precise "; specialMoveNum = 24; break;
            default: break;
        }
        return value;
    }

    protected virtual string preMagic()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 50: value = "Magical "; specialMoveNum = 20; break;
            case <= 100: value = "Arcane "; specialMoveNum = 1; break;
            default: break;
        }
        return value;
    }

    protected virtual string preFire()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 10: value = "Crackling "; specialMoveNum = 5; break;
            case < 30: value = "Smouldering "; specialMoveNum = 34; break;
            case < 50: value = "Flaming "; specialMoveNum = 8; break;
            case < 60: value = "Cindering "; specialMoveNum = 4; break;
            case < 70: value = "Lava "; specialMoveNum = 18; break;
            case < 80: value = "Molten "; specialMoveNum = 22; break;
            case < 84: value = "Crackling "; specialMoveNum = 5; break;
            case < 88: value = "Bolstering "; specialMoveNum = 3; break;
            case < 92: value = "Blistering "; specialMoveNum = 2; break;
            case <= 100: value = "Hellish "; specialMoveNum = 14; break;
            default: break;
        }
        return value;
    }

    protected virtual string preBleed()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 10: value = "Jagged "; specialMoveNum = 16; break;
            case < 30: value = "Lacerating "; specialMoveNum = 17; break;
            case < 50: value = "Gashing "; specialMoveNum = 13; break;
            case < 60: value = "Tearing "; specialMoveNum = 36; break;
            case < 70: value = "Slashing "; specialMoveNum = 33; break;
            case < 80: value = "Ripping "; specialMoveNum = 29; break;
            case < 84: value = "Mangling "; specialMoveNum = 21; break;
            case <= 100: value = "Notched "; specialMoveNum = 23; break;
            default: break;
        }
        return value;
    }

    protected virtual string preLightning()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 10: value = "Electric "; specialMoveNum = 7; break;
            case < 30: value = "Shocking "; specialMoveNum = 32; break;
            case < 50: value = "Zapping "; specialMoveNum = 39; break;
            case < 60: value = "Galvanic "; specialMoveNum = 12; break;
            case < 70: value = "Voltaic "; specialMoveNum = 37; break;
            case < 80: value = "Bolting "; specialMoveNum = 6; break;
            case <= 100: value = "Flashing "; specialMoveNum = 9; break;
            default: break;
        }
        return value;
    }

    protected virtual string prePetrify()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 10: value = "Cracking "; specialMoveNum = 40; break;
            case < 30: value = "Deforming "; specialMoveNum = 40; break;
            case < 50: value = "Malforming "; specialMoveNum = 40; break;
            case < 60: value = "Contorting "; specialMoveNum = 40; break;
            case < 70: value = "Solidifying "; specialMoveNum = 40; break;
            case < 80: value = "Cementing "; specialMoveNum = 40; break;
            case <= 100: value = "Petrified "; specialMoveNum = 40; break;
            default: break;
        }
        return value;
    }

    protected virtual string preIllusion()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 10: value = "Tricky "; specialMoveNum = 38; break;
            case < 30: value = "Illusioned "; specialMoveNum = 38; break;
            case < 50: value = "Miraged "; specialMoveNum = 38; break;
            case < 60: value = "Deceiving "; specialMoveNum = 38; break;
            case < 80: value = "Conjured "; specialMoveNum = 38; break;
            case <= 100: value = "Spirited "; specialMoveNum = 38; break;
            default: break;
        }
        return value;
    }
    protected virtual string preCold()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 10: value = "Frigid "; specialMoveNum = 11; break;
            case < 30: value = "Frozen "; specialMoveNum = 11; break;
            case < 60: value = "Chilling "; specialMoveNum = 11; break;
            case < 80: value = "Iced "; specialMoveNum = 15; break;
            case <= 100: value = "Numbing "; specialMoveNum = 15; break;
            default: break;
        }
        return value;
    }

    protected virtual string preStun()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 10: value = "Blunt "; specialMoveNum = 40; break;
            case < 30: value = "Stunning "; specialMoveNum = 40; break;
            case < 60: value = "Bewildering "; specialMoveNum = 40; break;
            case < 80: value = "Dazzling "; specialMoveNum = 40; break;
            case <= 100: value = "Smashing "; specialMoveNum = 40; break;
            default: break;
        }
        return value;
    }

    protected virtual string preAcid()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 30: value = "Reeking "; specialMoveNum = 27; break;
            case < 60: value = "Acidic "; specialMoveNum = 0; break;
            case < 80: value = "Revolting "; specialMoveNum = 28; break;
            case <= 100: value = "Fetid "; specialMoveNum = 27; break;
            default: break;
        }
        return value;
    }
    protected virtual string preToxic()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 30: value = "Flesh-Eating "; specialMoveNum = 10; break;
            case < 60: value = "Rotten "; specialMoveNum = 30; break;
            case <= 100: value = "Corrupted "; specialMoveNum = 10; break;
            default: break;
        }
        return value;
    }


    protected virtual string subMetal()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 10: value = "Iron "; break;
            case < 30: value = "Stone "; break;
            case < 50: value = "Bronze "; break;
            case < 60: value = "Steel "; break;
            case < 70: value = "Bone "; break;
            case < 80: value = "Ivory "; break;
            case < 84: value = "Obsidian "; break;
            case < 88: value = "Crystal "; break;
            case < 92: value = "Damascus "; break;
            case < 96: value = "Titanium "; break;
            case < 99: value = "Dragon Bone "; break;
            case 100: value = "Ender "; break;
            default: break;
        }
        return value;
    }
    protected virtual string subBroken()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 25: value = "Rustic "; break;
            case < 50: value = "Crude "; break;
            case < 75: value = "Worn "; break;
            case <= 100: value = "Cracked "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subSavage()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 33: value = "Savage "; break;
            case < 66: value = "Twisted "; break;
            case <= 100: value = "Spiked "; break;
            default: break;
        }
        return value;
    }
    protected virtual string subPeople()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 20: value = "Dwarven "; break;
            case < 40: value = "Elvish "; break;
            case < 60: value = "Orkish "; break;
            case < 80: value = "Avian "; break;
            case <= 100: value = "Giants "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subRoyal()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 15: value = "Gilded "; break;
            case < 30: value = "Ornate "; break;
            case < 45: value = "Embellished "; break;
            case < 80: value = "Regal "; break;
            case < 90: value = "Queen's "; break;
            case <= 100: value = "King's "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subGodly()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 20: value = "Holy "; break;
            case < 40: value = "Godly "; break;
            case < 60: value = "Silver "; break;
            case < 80: value = "Heavenly "; break;
            case <= 100: value = "Celestial "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subCold()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 20: value = "Frigid "; break;
            case < 40: value = "Frozen "; break;
            case < 60: value = "Iced "; break;
            case < 80: value = "Chilling "; break;
            case <= 100: value = "Numbing "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subKingdom()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 20: value = "Commoner's "; break;
            case < 40: value = "Peasant's "; break;
            case < 60: value = "Jester's "; break;
            case < 80: value = "Soldier's "; break;
            case <= 100: value = "Knight's "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subWoods()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 15: value = "Ashwood "; break;
            case < 30: value = "Driftwood "; break;
            case < 45: value = "Birch "; break;
            case < 80: value = "Willow "; break;
            case < 90: value = "Oak "; break;
            case < 95: value = "Ironwood"; break;
            case <= 100: value = "Warpwood "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subMagic()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 15: value = "Enchanting "; break;
            case < 30: value = "Magic "; break;
            case < 45: value = "Rune-Forged "; break;
            case < 80: value = "Moonlit "; break;
            case < 90: value = "Cosmic "; break;
            case < 95: value = "Stellar "; break;
            case <= 100: value = "Majestic "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subBetter()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 25: value = "Grand "; break;
            case < 50: value = "Enhanced "; break;
            case < 75: value = "Strengthened "; break;
            case <= 100: value = "Grand "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subLevels()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 25: value = "Lesser "; break;
            case < 50: value = "Grand "; break;
            case < 75: value = "Greater "; break;
            case <= 100: value = "Master's "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subColor()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 15: value = "Green "; break;
            case < 30: value = "Red "; break;
            case < 45: value = "Blue "; break;
            case < 60: value = "Pink "; break;
            case < 75: value = "Yellow "; break;
            case < 90: value = "Black "; break;
            case <= 100: value = "White "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subUnholy()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 15: value = "Blasphemous "; break;
            case < 30: value = "Abnormal "; break;
            case < 45: value = "Terrible "; break;
            case < 60: value = "Unnatural "; break;
            case < 75: value = "Rugged "; break;
            case < 90: value = "Unholy "; break;
            case <= 100: value = "Forsaken "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subGrim()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 15: value = "Skeletal "; break;
            case < 30: value = "Dark "; break;
            case < 45: value = "Grim "; break;
            case < 60: value = "Intimidating "; break;
            case < 75: value = "Foreboding "; break;
            case <= 100: value = "Unsympathetic "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subLiquid()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 50: value = "Dripping "; break;
            case <= 100: value = "Oozing "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subGhost()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 25: value = "Ghostly "; break;
            case < 50: value = "Phantom "; break;
            case < 75: value = "Ghouls  "; break;
            case <= 100: value = "Banshee  "; break;
            default: break;
        }
        return value;
    }
    protected virtual string subBlood()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case <= 100: value = "Blood Forged "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subTwo()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case <= 100: value = "Two-handed "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subFire()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case <= 100: value = "Fireweaver "; break;
            default: break;
        }
        return value;
    }
    protected virtual string subStun()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case <= 100: value = "Madusa's "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subPetrify()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case <= 100: value = "Yeti's "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subToxic()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 33: value = "Toxic "; break;
            case < 66: value = "Revolting "; break;
            case <= 100: value = "Foul  "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subBrawl()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 33: value = "Brawlers "; break;
            case < 66: value = "Fierce "; break;
            case <= 100: value = "Furious  "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subAcid()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 33: value = "Dreadful "; break;
            case < 66: value = "Acidic "; break;
            case <= 100: value = "Nasty  "; break;
            default: break;
        }
        return value;
    }

    protected virtual string subTwinBlades()
    {
        string value = "";
        int randNum = Random.Range(0, 100);
        switch (randNum)
        {
            case < 33: value = "Prick and Stick "; break;
            case < 66: value = "Strength and Honor "; break;
            case <= 100: value = "Heart and Soul  "; break;
            default: break;
        }
        return value;
    }


    protected virtual void setCost()
    {
        cost = Random.Range(1, 10) + (rarityLevel * 10);
    }

    protected virtual void setDamage()
    {
        damage = Random.Range(1, 4) + damageMulitplier;
    }

    public virtual void applySpecialMove(int moveNum)
    {
        switch (moveNum)
        {
            case 0: specialMove = "Acidic "; acidDamage += (damage / 5); damage = damage + acidDamage; GameLogs.Instance.playerAcidDamage += acidDamage; break;
            case 1: specialMove = "Magical Wrath "; magicDamage += magicDamage + GameLogs.Instance.playersCurrentInt; break;
            case 2: specialMove = "Bubbling Skin "; GameLogs.Instance.playerIgnoreShields = true; GameLogs.Instance.playerIgnoreFireRes = true; break;
            case 3: specialMove = "Clashing Blades "; GameLogs.Instance.playerDoubleAttack = true; break;
            case 4: specialMove = "Spark of Life "; GameLogs.Instance.playerCanHeal = GameLogs.Instance.playerFireDamage; break;
            case 5: specialMove = "Blissful Popping "; GameLogs.Instance.canCreateFire = true; GameLogs.Instance.campFireCreated = true; break;
            case 6: specialMove = "Vicious Bolt "; GameLogs.Instance.playerCanLightningStrike = true; break;
            case 7: specialMove = "Electrify "; shockDamage += (damage / 10); GameLogs.Instance.playerShockDamage += shockDamage; break;
            case 8: specialMove = "Flaming "; flameDamage += (damage / 10); GameLogs.Instance.playerFlameDamage += flameDamage; break;
            case 9: specialMove = "Arcing Light "; GameLogs.Instance.chanceToChainLightning += 5; break;
            case 10: specialMove = "Unending Craving "; GameLogs.Instance.canDealCloudDamage = true; GameLogs.Instance.cloudDamage = GameLogs.Instance.playerAcidResist; break;
            case 11: specialMove = "Frozen "; frozenDamage += (damage / 5); GameLogs.Instance.playerFrozenDamage = frozenDamage; break;
            case 12: specialMove = "Galvanic "; shockDamage += (damage / 5); break;
            case 13: specialMove = "Dull Blade "; bleedDamage += (damage / 10); GameLogs.Instance.chanceToCauseBleed = 5; GameLogs.Instance.playerBleedDamage += bleedDamage; break;
            case 14: specialMove = "Hellish Weapon "; flameDamage += (damage / 3); GameLogs.Instance.playerFlameDamage += flameDamage; break;
            case 15: specialMove = "Iced "; frozenDamage += (damage / 10); GameLogs.Instance.playerFrozenDamage += frozenDamage; break;
            case 16: specialMove = "Tearing Swipe "; bleedDamage += (damage / 10); GameLogs.Instance.chanceToCauseBleed = 5; GameLogs.Instance.playerBleedDamage += bleedDamage; break;
            case 17: specialMove = "Sharp Blade "; critChance += 2; GameLogs.Instance.playerItemCrit += 2; break;
            case 18: specialMove = "Pools of Agony "; GameLogs.Instance.lavaPoolDamage += flameDamage; GameLogs.Instance.canCreateLavaPools = true; GameLogs.Instance.numLavaPools = 3; break;
            case 19: specialMove = "Gift of Light "; GameLogs.Instance.playerCurrentAP += 1; break;
            case 20: specialMove = "Magical Ambistions "; magicDamage += (damage / 5); GameLogs.Instance.playerMagicDamage += magicDamage; break;
            case 21: specialMove = "Mangled "; bleedDamage += ((damage / 3) + GameLogs.Instance.playerCurrentLuck); GameLogs.Instance.playerBleedDamage += bleedDamage; GameLogs.Instance.chanceToCauseBleed += 5; break;
            case 22: specialMove = "Molten Weapon "; flameDamage += (damage / 5); GameLogs.Instance.playerFlameDamage += flameDamage; break;
            case 23: specialMove = "Notched "; bleedDamage += ((damage / 5) + GameLogs.Instance.playerCurrentLuck); GameLogs.Instance.playerBleedDamage += bleedDamage; GameLogs.Instance.chanceToCauseBleed += 5; break;
            case 24: specialMove = "Ricardo's Finesse "; chanceToPierce += 10; piercingDamage = damage; GameLogs.Instance.chanceToPierce += chanceToPierce; GameLogs.Instance.playerPiercingDamage += piercingDamage; break;
            case 25:
                specialMove = "Perfect Hit "; chanceToPierce += 10; piercingDamage = damage; GameLogs.Instance.chanceToPierce += chanceToPierce; GameLogs.Instance.playerPiercingDamage += piercingDamage;
                critChance += 1; GameLogs.Instance.playerItemCrit += 1; break;
            case 26: specialMove = "Flurry of Blows "; GameLogs.Instance.playerDoubleAttack = true; break;
            case 27: specialMove = "Reaking "; acidDamage += (damage / 10); damage = damage + acidDamage; GameLogs.Instance.playerAcidDamage += acidDamage; break;
            case 28: specialMove = "Revolting "; acidDamage += ((damage / 4) + GameLogs.Instance.playerCurrentLuck); GameLogs.Instance.playerAcidDamage += acidDamage; break;
            case 29: specialMove = "Ripping "; bleedDamage += ((damage / 3) + GameLogs.Instance.playerCurrentLuck); GameLogs.Instance.playerBleedDamage += bleedDamage; GameLogs.Instance.chanceToCauseBleed += 5; break;
            case 30: specialMove = "Frail Flesh "; acidDamage += (GameLogs.Instance.playerAcidResist * GameLogs.Instance.playerCurrentLuck); GameLogs.Instance.playerAcidDamage += acidDamage; break;
            case 31: specialMove = "Bloody Bargain "; GameLogs.Instance.bloodyBargain = true; break;
            case 32: specialMove = "Frightening Shock "; GameLogs.Instance.cloudDamage = 3; GameLogs.Instance.cloudDamage = 3; GameLogs.Instance.canDealCloudDamage = true; break;
            case 33: specialMove = "Slashing Strike "; GameLogs.Instance.slashingStrike = true; break;
            case 34: specialMove = "Awaken the Dragon "; GameLogs.Instance.flameBreath = true; GameLogs.Instance.flameBreathHitChance = 8; GameLogs.Instance.flameBreathDamage += damage; break;
            case 35: specialMove = "Knight's Stance "; GameLogs.Instance.KnightsStance = true; break;
            case 36: specialMove = "Rigid Blade "; piercingDamage += (damage / 2); chanceToPierce = 10; GameLogs.Instance.chanceToPierce += chanceToPierce; GameLogs.Instance.playerPiercingDamage += piercingDamage; break;
            case 37: specialMove = "Voltic "; shockDamage += (damage / 3); GameLogs.Instance.playerShockDamage += shockDamage; break;
            case 38: specialMove = "Warping Illusion "; GameLogs.Instance.warpingIllusion = true; break;
            case 39: specialMove = "Quick Pop "; GameLogs.Instance.quickPop = true; break;
            case 40: specialMove = "Stun "; GameLogs.Instance.chanceToStun += 10; GameLogs.Instance.stunDuration = 1; break;
            default: break;
        }
    }
    public string setSpecialMove()
    {

        switch (specialMoveNum)
        {
            case 0: specialMove = "Acidic "; break;
            case 1: specialMove = "Magical Wrath "; break;
            case 2: specialMove = "Bubbling Skin "; break;
            case 3: specialMove = "Clashing Blades "; break;
            case 4: specialMove = "Spark of Life "; break;
            case 5: specialMove = "Blissful Popping"; break;
            case 7: specialMove = "Electrify "; break;
            case 8: specialMove = "Flaming "; break;
            case 9: specialMove = "Arcing Light "; break;
            case 10: specialMove = "Unending Craving "; break;
            case 11: specialMove = "Frozen "; break;
            case 12: specialMove = "Galvanic "; break;
            case 13: specialMove = "Dull Blade "; break;
            case 14: specialMove = "Hellish Weapon "; break;
            case 15: specialMove = "Iced "; break;
            case 16: specialMove = "Tearing Swipe "; break;
            case 17: specialMove = "Sharp Blade "; break;
            case 18: specialMove = "Pools of Agony "; break;
            case 19: specialMove = "Gift of Light "; break;
            case 20: specialMove = "Magical Ambistions "; break;
            case 21: specialMove = "Mangled "; break;
            case 22: specialMove = "Molten Weapon "; break;
            case 23: specialMove = "Notched "; break;
            case 24: specialMove = "Ricardo's Finesse "; break;
            case 25: specialMove = "Perfect Hit "; break;
            case 26: specialMove = "Flurry of Blows "; break;
            case 27: specialMove = "Reaking "; break;
            case 28: specialMove = "Revolting "; break;
            case 29: specialMove = "Ripping "; break;
            case 30: specialMove = "Frail Flesh "; break;
            case 31: specialMove = "Bloody Bargain "; break;
            case 32: specialMove = "Frightening Shock "; break;
            case 33: specialMove = "Slashing Strike "; break;
            case 34: specialMove = "Awaken the Dragon "; break;
            case 35: specialMove = "Knight's Stance "; break;
            case 36: specialMove = "Rigid Blade "; break;
            case 37: specialMove = "Voltic "; break;
            case 38: specialMove = "Warping Illusion "; break;
            case 39: specialMove = "Quick Pop "; break;
            case 40: specialMove = "Stun "; break;
            default: break;
        }

        return specialMove;

    }
    public string getDamageType()
    {
        string type = "";
        switch (damageTypeNum)
        {
            case 0: type = "Physical"; break;
            case 1: type = "Magic"; break;
            case 2: type = "Fire"; break;
            case 3: type = "Lightning"; break;
            case 4: type = "Bleed"; break;
            case 5: type = "Cold"; break;
            case 6: type = "Acid"; break;
            case 7: type = "Petrify"; break;
            case 8: type = "Toxic"; break;
            case 9: type = "Stun"; break;
            case 10: type = "Illusion"; break;

            default: type = "None"; break;
        }
        return type;
    }

    public string getAbility()
    {
        string rValue = "";

        switch (abilityNum)
        {
            case 0: rValue = "Metal"; break;
            case 1: rValue = "Broken"; break;
            case 2: rValue = "People"; break;
            case 3: rValue = "Royal"; break;
            case 4: rValue = "Godly"; break;
            case 5: rValue = "Wood"; break;
            case 6: rValue = "Magic"; break;
            case 7: rValue = "Better"; break;
            case 8: rValue = "Color"; break;
            case 9: rValue = "Liquid"; break;
            case 10: rValue = "Ghost"; break;
            case 11: rValue = "Blood"; break;
            case 12: rValue = "Fire"; break;
            case 13: rValue = "Acid"; break;
            case 14: rValue = "Toxic"; break;
            case 15: rValue = "Kingdom"; break;
            case 16: rValue = "Two-Handed"; break;
            case 17: rValue = "Savage"; break;
            case 18: rValue = "Levels"; break;
            case 19: rValue = "Unholy"; break;
            case 20: rValue = "Grim"; break;
            case 21: rValue = "Brawl"; break;
            case 22: rValue = "TwinBlades"; break;
            case 23: rValue = "Cold"; break;
            case 24: rValue = "Petrify"; break;
            case 25: rValue = "Stunned"; break;

            default: rValue = "None"; break;
        }
        return rValue;
    }
}