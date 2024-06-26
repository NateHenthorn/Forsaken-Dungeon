using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryUI; // Reference to the inventory UI GameObject
    public List<GameObject> inventory = new List<GameObject>();
    public HUDImage hudImage;
    public HUDManager hudManager;
    public GameObject HUDObject;
    public Canvas canvas;
    public static InventoryController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventoryUI = GameObject.FindGameObjectWithTag("Inventory");
        hudImage = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDImage>();
        canvas = FindObjectOfType<Canvas>();
        HUDObject = hudImage.gameObject;
    }
    void Update()
    {
        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }
        // Check for input to open the inventory (e.g., "I" key)
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleHUD();
        }
        if (hudImage == null)
        {
            hudImage = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDImage>();
            HUDObject = hudImage.gameObject;
        }
    }

    public void ToggleHUD()
    {
        HUDObject.SetActive(!HUDObject.activeSelf);
    }
    // Toggle the visibility of the inventory UI
    public void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    public void AddItem(GameObject item)
    {
        inventory.Add(item);
        // Optionally, update the UI to reflect the change
    }

    // Remove an item from the inventory
    public void RemoveItem(GameObject item)
    {
        inventory.Remove(item);
        // Optionally, update the UI to reflect the change
    }
}