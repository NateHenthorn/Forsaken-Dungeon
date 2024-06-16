using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PShotItemButton : MonoBehaviour
{
    public GameLogs log;
    public ShopItem shopItem;
    public int cost = 0;

    private void Start()
    {
        cost = shopItem.cost;
        Debug.Log("PShotItemButton Start: cost set to " + cost);

        // Get the Button component attached to this GameObject and add the click listener
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("Button component not found on " + gameObject.name);
        }
    }

    public void OnClick()
    {
        Debug.Log("Button clicked");
        buyItem();
    }

    public void buyItem()
    {
        cost = shopItem.cost;
        Debug.Log("Attempting to buy item with cost: " + cost);

        if (GameLogs.Instance.playerCurrentCoin >= cost)
        {
            GameLogs.Instance.playerCurrentCoin -= cost;
            GameLogs.Instance.pShopItems[GameLogs.Instance.numItems] = shopItem.item;
            GameLogs.Instance.numItems++;
            Debug.Log("Item bought successfully");
        }
        else
        {
            Debug.Log("Not Enough Coin");
        }
    }
}