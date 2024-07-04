using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogs : MonoBehaviour
{
    //GameManage Stuff
    public int roomDifficultLevel = 0;
    //For Map Creater
    public int numLayers = 0;
    public int currentRoomX = 0;
    public int currentRoomY = 0;
    //Rooms
    string currentRoom = "";
    string room = "Room";
    string dungeon = "Map";
    string pShop = "PrestigeShop";
    string dead = "Died";
    string title = "TitleScreen";
    //Scene
    Scene currentScene;
    string currentSceneName = "";

    //Player stats
    //Damages
    public int playerBleedDamage = 0;
    public int playerFrozenDamage = 0;
    public int playerFlameDamage = 0;
    public int playerShockDamage = 0;
    public int playerFireDamage = 0;
    public int playerMagicDamage = 0;
    public int playerPiercingDamage = 0;
    public int playerAcidDamage = 0;


    //Resistances
    public int playerAcidResist = 1;
    public int playerShockResist = 1;
    public int playerFlameResist = 1;
    public int playerFrozenResist = 1;
    public int playerMagicResist = 1;

    //Booleans
    public bool playerIgnoreShields = false;
    public bool playerIgnoreFireRes = false;
    public bool playerDoubleAttack = false;
    public bool playerCanLightningStrike = false;
    public bool canCreateFire = false;
    public bool campFireCreated = false;

    //Special Abilities
    public int chanceToChainLightning = 0;
    public int playerCanHeal = 0;
    public int cloudDamage = 0;
    public bool canDealCloudDamage = false;
    public int cloudDamageType = -1;
    public int cloudDamageRange = 1;
    public int chanceToCauseBleed = 0;
    public int numLavaPools = 0;
    public bool canCreateLavaPools = false;
    public int lavaPoolDamage = 0;
    public int lavaPoolPercentToHit = 10;
    public bool bloodyBargain = false;
    public bool slashingStrike = false;
    public bool flameBreath = false;
    public int flameBreathHitChance = 0;
    public int flameBreathDamage = 0;
    public bool KnightsStance = false;
    public int KnightsStanceNum = -1; //0 or 1
    public bool warpingIllusion = false;
    public bool quickPop = false;
    public int chanceToStun = 0;
    public int stunDuration = 0;

    //BaseStats
    public int playerActionPoints = 3;
    public int numEnemiesKilled = 0;
    public int playerCurrentHP = 100;
    public int playerCurrentCoin = 0;
    public int playerCurrentBP = 1;
    public int playerCurrentAP = 2;
    public int playerCurrentMBP = 0;
    public int playerCostToLevel = 1;

    //Player Stats
    public int playerStrength = 1;
    public int playerCurrentDex = 1;
    public int playerCurrentLuck = 1;
    public int playersCurrentInt = 1;
    public int playerConstitution = 1;
    public int playerCurrentFaith = 1;

    //Item Stats
    public int playerItemRange = 1;
    public int playerItemDamage = 0;
    public int playerItemDamageType = 0;
    public int playerItemCrit = 0;
    public int chanceToPierce = 0;



    //References 
    public Player player;

    //Items
    public int numItems = 0;
    public PShopItem[] pShopItems = new PShopItem[99];

    public static GameLogs Instance { get; private set; }
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
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentSceneName = checkScene();
        loadRoom();
    }

    void Update()
    {

    }

    string checkScene()
    {
        currentScene = SceneManager.GetActiveScene();

        // Get the scene name
        string sceneName = currentScene.name;

        // Get the build index of the scene
        int sceneBuildIndex = currentScene.buildIndex;
        return currentScene.name;
    }

    void loadRoom()
    {
        switch (currentSceneName)
        {
            case "Room": loadRoomScene(); currentRoom = "Room"; break;
            case "Map": loadMapScene(); currentRoom = "Map"; break;
            case "PrestigeShop": loadpShopScene(); currentRoom = "PrestigeShop"; break;
            case "Died": loadDiedScene(); currentRoom = "Died"; break;
            case "TitleScreen": loadTitleScreenScene(); currentRoom = "TitleScreen"; break;
            default: break;
        }
    }

    void loadRoomScene()
    {
        player.currentHP = playerCurrentHP;
        player.coins = playerCurrentCoin;
    }
    void loadMapScene()
    {

    }
    void loadpShopScene()
    {

    }
    void loadDiedScene()
    {

    }
    void loadTitleScreenScene()
    {

    }
}