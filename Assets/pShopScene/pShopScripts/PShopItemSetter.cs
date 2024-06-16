using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PShopItemSetter : MonoBehaviour
{
    public AxeWeapon Axe;
    public CurvedWeapon Curved;
    public DaggerWeapon Dagger;
    public FistWeapon Fist;
    public GreataxeWeapon GreatAxe;
    public GreatCurvedWeapon GreatCurved;
    public GreatHammerWeapon GreatHammer;
    public GreatLanceWeapon GreatLance;
    public GreatswordWeapon Greatsword;
    public HalberdWeapon Halberd;
    public HammerWeapon Hammer;
    public KatanaWeapon Katana;
    public LadleWeapon Ladle;
    public LanceWeapon Lance;
    public LongbowWeapon Longbow;
    public ScytheWeapon Scythe;
    public SpearWeapon Spear;
    public ShortbowWeapon Shortbow;
    public StaffWeapon Staff;
    public SwordWeapon Sword;
    public TalismanWeapon Talisman;
    public TorchWeapon Torch;
    public UltraGreatswordWeapon UltraGreatsword;
    public TwinbladesWeapon Twinblades;
    public WhipWeapon Whip;

    public PShopItem pickItem(int randNum)
    {
        PShopItem shopItem = new PShopItem();
        switch (randNum)
        {
            case 0: shopItem = Axe; Axe.rStart(); break;
            case 1: shopItem = Curved; Curved.rStart(); break;
            case 2: shopItem = Dagger; Dagger.rStart(); break;
            case 3: shopItem = Fist; Fist.rStart(); break;
            case 4: shopItem = GreatAxe; GreatAxe.rStart(); break;
            case 5: shopItem = GreatCurved; GreatCurved.rStart(); break;
            case 6: shopItem = GreatHammer; GreatHammer.rStart(); break;
            case 7: shopItem = GreatLance; GreatLance.rStart(); break;
            case 8: shopItem = Greatsword; Greatsword.rStart(); break;
            case 9: shopItem = Halberd; Halberd.rStart(); break;
            case 10: shopItem = Hammer; Hammer.rStart(); break;
            case 11: shopItem = Katana; Katana.rStart(); break;
            case 12: shopItem = Ladle; Ladle.rStart(); break;
            case 13: shopItem = Lance; Lance.rStart(); break;
            case 14: shopItem = Longbow; Longbow.rStart(); break;
            case 15: shopItem = Scythe; Scythe.rStart(); break;
            case 16: shopItem = Spear; Spear.rStart(); break;
            case 17: shopItem = Shortbow; Shortbow.rStart(); break;
            case 18: shopItem = Staff; Staff.rStart(); break;
            case 19: shopItem = Sword; Sword.rStart(); break;
            case 20: shopItem = Talisman; Talisman.rStart(); break;
            case 21: shopItem = Torch; Torch.rStart(); break;
            case 22: shopItem = UltraGreatsword; UltraGreatsword.rStart(); break;
            case 23: shopItem = Twinblades; Twinblades.rStart(); break;
            case 24: shopItem = Whip; Whip.rStart(); break;
            default: break;
        }
        return shopItem;
    }
}