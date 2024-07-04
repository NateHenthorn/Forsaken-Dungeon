using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public PShopItem item;
    public int cost = 1;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Description;
    public Button BuyButton;
    public bool selected = false;
    public ShopItem shopItem;
    private Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        // Ensure the BuyButton's onClick event is set up
        if (BuyButton != null)
        {
            BuyButton.onClick.AddListener(buyItem);
        }
        else
        {
            Debug.LogError("BuyButton is not assigned in the inspector.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buyItem();
    }
    public void buyItem()
    {
        if (GameLogs.Instance.playerCurrentCoin >= cost)
        {
            GameLogs.Instance.playerCurrentCoin -= cost;
            GameLogs.Instance.pShopItems[GameLogs.Instance.numItems] = item;
            GameLogs.Instance.numItems++;
            Debug.Log("Item bought successfully");
            itemBought();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not Enough Coin");
        }
    }

    public void itemBought()
    {
        if (item.CompareTag("Weapon"))
        {
        GameLogs.Instance.playerItemRange = item.range;
        GameLogs.Instance.playerItemDamage = item.damage;
        GameLogs.Instance.playerItemCrit = item.critChance;
        item.applySpecialMove(item.specialMoveNum);
        }
        if (item.CompareTag("Armor"))
        {
        GameLogs.Instance.playerCurrentBP += item.protectionLevel;
        item.applySpecialMove(item.specialMoveNum);
        }

        }
}