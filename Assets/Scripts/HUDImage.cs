using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDImage : MonoBehaviour
{
    // Start is called before the first frame update

    public static HUDImage Instance { get; private set; }
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

    public TextMeshProUGUI HP;
    public TextMeshProUGUI BP;
    public TextMeshProUGUI AP;
    public TextMeshProUGUI Luck;
    public TextMeshProUGUI costToLevel;
    public TextMeshProUGUI DMG;
    public TextMeshProUGUI Str;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI crit;
    public TextMeshProUGUI range;
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "HUD";
        spriteRenderer.sortingOrder = 7;

        // Set Sorting Order for UI elements
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.SetSiblingIndex(7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
