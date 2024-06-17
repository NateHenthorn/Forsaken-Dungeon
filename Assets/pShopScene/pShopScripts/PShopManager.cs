using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PShopManager : MonoBehaviour
{
    public static PShopManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }
    //References
    private Canvas canvas;
    public Button button;
    public PShopItemSetter itemSetter;
    //
    public static float currentYOffset = 0f;
    public ShopItem shopItem;
    public List<ShopItem> shopItems = new List<ShopItem>();
    private int shopItemNum = 0;
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        makeShopItem();
        makeShopItem();
        makeShopItem();
        makeShopItem();
        makeShopItem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public ShopItem makeShopItem()
    {
        if (shopItem != null && canvas != null)
        {
            ShopItem newShopItem = Instantiate(shopItem, canvas.transform);
            RectTransform shopItemRect = newShopItem.GetComponent<RectTransform>();

            shopItemRect.anchorMin = new Vector2(1, 1);
            shopItemRect.anchorMax = new Vector2(1, 1);
            shopItemRect.pivot = new Vector2(1, 1);

            int insertIndex = 0;

            while (insertIndex < shopItem.cost)
            {
                insertIndex++;
            }
            insertIndex = Mathf.Clamp(shopItem.cost, 0, shopItems.Count);

            newShopItem = setShopItem(newShopItem);
            shopItems.Insert(insertIndex, newShopItem);

            // Reposition
            RepositionStatBlocks();
            return newShopItem;
        }
        else
        {
            return null;
        }
    }

    public ShopItem setShopItem(ShopItem shopItem)
    {
        int randNum = Random.Range(0, 24);
        shopItem.item = itemSetter.pickItem(randNum);
        shopItem.cost = shopItem.item.cost;
        shopItem.Cost.text = shopItem.item.cost + "g";
        shopItem.name1.text = shopItem.item.name1;
        shopItem.Description.text = shopItem.item.description;

        return shopItem;
    }
    private void RepositionStatBlocks()
    {
        currentYOffset = 0f;
        foreach (ShopItem block in shopItems)
        {
            RectTransform shopItemRect = block.GetComponent<RectTransform>();
            shopItemRect.anchoredPosition = new Vector2(-185, -100 - currentYOffset);
            currentYOffset += shopItemRect.sizeDelta.y + 10f;
        }
    }
}