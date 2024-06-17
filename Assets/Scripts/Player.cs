using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    //Tile 
    private Tile currentTile;
    public bool nextButtonClicked = false;

    //ItemStats
    public int itemDamage = 0;
    public int itemDamageType = 0;
    public int itemRange = 1;
    public int itemCritChance = 0;
    public int chanceToPierce = 0;

    //Resistances
    public int acidResistance = 1;
    public int shockResistance = 1;
    public int flameResistance = 1;
    public int frozenResistance = 1;
    public int magicResistance = 1;

    //Special Abilities
    public int chanceToChainLightning = 0;
    public int canHeal = 0;
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

    //Special Damage Types
    public int bleedDamage = 0;
    public int frozenDamage = 0;
    public int flameDamage = 0;
    public int shockDamage = 0;
    public int fireDamage = 0;
    public int magicDamage = 0;
    public int piercingDamage = 0;
    public int acidDamage = 0;

    //Player Sats
    public int dmg = 40;
    public int hitPoints = 100;
    public int currentHP = 100;
    public int moveRate = 1;
    public int initiative = 10;
    public int actionPoints = 3;
    public int actionPointsUsed = 0;
    public int blockPoints = 1;
    public int luck = 0;
    public int costToLevel = 1;
    public int strength = 1;
    public int dextartity = 1;
    public int intelligence = 1;
    public int faith = 1;
    public int moveSpeed = 1;
    public int attackRange = 1;

    //Hands
    public GameObject rightHand;
    public GameObject leftHand;

    //Coins and XP
    public int prevCoins = 0;
    public int coins = 0;
    public int levelPoints = 0;

    //inventory
    public PShopItem[] pShopItems = new PShopItem[99];

    //Turn based 
    public bool playerSlain = false;
    public bool actionDone = false;
    public int turnStatus = 0;
    private bool hasGone = false;
    public bool hasMoved = false;
    public bool hasTakenAction = false;
    public int tileSize = 26;
    public int stunnedFor = 0;
    public bool stunned = false;

    //References
    private GameManager gameManager;
    private TurnManager turnManager;
    private Rigidbody2D rb;
    public StatBlock statblock;
    private Canvas canvas;
    public Button Next;
    private Camera mainCamera;
    private StatBlock thisStatBlock;
    public StatTable statTable;
    public GameLogs gameLogs;


    private void Start()
    {
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
        statTable = GameObject.FindGameObjectWithTag("StatTable").GetComponent<StatTable>();
        canvas = FindObjectOfType<Canvas>();
        turnManager = FindObjectOfType<TurnManager>();
        gameLogs = GameObject.FindGameObjectWithTag("GameLogs").GetComponent<GameLogs>();
        setStats();
        makeStatBlock();
        if (Next != null)
        {
            Next.onClick.AddListener(OnNextButtonClick);
        }
        InitializePlayerOnTile();
    }

    private void Update()
    {
        HandleMouseInput();
        if ((turnManager.turnStatus == initiative) && !hasGone && !stunned)
        {
            hasGone = true;
            turnManager.isPlayerTurn = false;
            startTurn();
        }
        if (currentHP <= 0)
        {
            playerIsSlain();
        }
        if (prevCoins != coins)
        {
            prevCoins = coins;
            GameLogs.Instance.playerCurrentCoin = coins;
        }
        if (actionPointsUsed == actionPoints)
        {
            endTurn();
        }
    }


    void setStats()
    {
        actionPoints = GameLogs.Instance.playerActionPoints;
        currentHP = GameLogs.Instance.playerCurrentHP;
        coins = GameLogs.Instance.playerCurrentCoin;
        moveRate = 1 * tileSize;
        pShopItems = GameLogs.Instance.pShopItems;
        itemRange = GameLogs.Instance.playerItemRange;
        itemDamage = GameLogs.Instance.playerItemDamage;
        itemDamageType = GameLogs.Instance.playerItemDamageType;
        strength = GameLogs.Instance.playerStrength;
        attackRange = itemRange * tileSize;
        dmg = itemDamage + (strength * 4);
        itemCritChance = GameLogs.Instance.playerItemCrit;
        luck = GameLogs.Instance.playerCurrentLuck;
        blockPoints = GameLogs.Instance.playerCurrentBP;
        actionPoints = GameLogs.Instance.playerCurrentAP;
        costToLevel = GameLogs.Instance.playerCostToLevel;
        //Resistances
        acidResistance = GameLogs.Instance.playerAcidResist;
        shockResistance = GameLogs.Instance.playerShockResist;
        flameResistance = GameLogs.Instance.playerFlameResist;
        frozenResistance = GameLogs.Instance.playerFrozenResist;
        magicResistance = GameLogs.Instance.playerMagicResist;
        //Damage
        bleedDamage = GameLogs.Instance.playerBleedDamage;
        frozenDamage = GameLogs.Instance.playerFrozenDamage;
        flameDamage = GameLogs.Instance.playerFlameDamage;
        shockDamage = GameLogs.Instance.playerShockDamage;
        fireDamage = GameLogs.Instance.playerFireDamage;
        magicDamage = GameLogs.Instance.playerMagicResist;
        piercingDamage = GameLogs.Instance.playerPiercingDamage;
        acidDamage = GameLogs.Instance.playerAcidDamage;
        //Special Abilities
        chanceToChainLightning = GameLogs.Instance.chanceToChainLightning;
        canHeal = GameLogs.Instance.playerCanHeal;
        cloudDamage = GameLogs.Instance.cloudDamage;
        canDealCloudDamage = GameLogs.Instance.canDealCloudDamage;
        cloudDamageType = GameLogs.Instance.cloudDamageType;
        cloudDamageRange = GameLogs.Instance.cloudDamageRange;
        chanceToCauseBleed = GameLogs.Instance.chanceToCauseBleed;
        numLavaPools = GameLogs.Instance.numLavaPools;
        canCreateLavaPools = GameLogs.Instance.canCreateLavaPools;
        lavaPoolDamage = GameLogs.Instance.lavaPoolDamage;
        lavaPoolPercentToHit = GameLogs.Instance.lavaPoolPercentToHit;
        bloodyBargain = GameLogs.Instance.bloodyBargain;
        slashingStrike = GameLogs.Instance.slashingStrike;
        flameBreath = GameLogs.Instance.flameBreath;
        flameBreathHitChance = GameLogs.Instance.flameBreathHitChance;
        flameBreathDamage = GameLogs.Instance.flameBreathDamage;
        KnightsStance = GameLogs.Instance.KnightsStance;
        KnightsStanceNum = GameLogs.Instance.KnightsStanceNum;
        warpingIllusion = GameLogs.Instance.warpingIllusion;
        quickPop = GameLogs.Instance.quickPop;
        chanceToStun = GameLogs.Instance.chanceToStun;
        stunDuration = GameLogs.Instance.stunDuration;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
        }
    }

    protected virtual void makeStatBlock()
    {
        thisStatBlock = statTable.makeStatBlock();
        setStatBlock();
    }

    public void setStatBlock()
    {
        thisStatBlock.setInitiative(initiative);
        thisStatBlock.name1.text = "Player";
        thisStatBlock.HP.text = "" + currentHP;
        thisStatBlock.DMG.text = "" + dmg;
        thisStatBlock.SE.text = "" + levelPoints;
        thisStatBlock.coins.text = "" + coins;

    }

    public void startTurn()
    {
        Vector3 playerPosition = transform.position;
        Tile thisTile = GetTileAtPosition(playerPosition);
        if (thisTile != null)
        {
            thisTile.CheckForNeighbors();
        }
        else
        {
            Debug.LogError("Player is not on a valid tile at the start of turn.");
        }
    }

    private void InitializePlayerOnTile()
    {
        Vector3 playerPosition = transform.position;
        currentTile = GetTileAtPosition(playerPosition);
        if (currentTile != null)
        {
            currentTile.OccupyTileWithPlayer();
        }
        else
        {
            Debug.LogError("Player is not on a valid tile at the start.");
        }
    }


    void AttackEnemy(Enemies enemy)
    {
        int crit = 1;
        int random = Random.Range(1, 20);
        int stunNum = Random.Range(0, chanceToStun);
        if (stunNum > 0)
        {
            stunNum = stunDuration;
        }
        if (random <= itemCritChance) { crit = 2; print("Crit hit" + this.dmg * crit); }
        enemy.takeDamage(this.dmg * crit, this.frozenDamage, this.magicDamage, this.flameDamage, this.shockDamage, this.piercingDamage, this.acidDamage, stunNum);
        hasTakenAction = true;
        actionPointsUsed++;
    }
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //  Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.CompareTag("Enemy"))
                {
                    Enemies enemyScript = clickedObject.GetComponent<Enemies>();
                    if (enemyScript != null && actionPointsUsed < actionPoints)
                    {
                        if (IsEnemyAdjacent(clickedObject))
                        {
                            AttackEnemy(enemyScript);
                        }

                    }
                }
            }

        }
    }

    bool IsEnemyAdjacent(GameObject enemy)
    {

        Vector3 playerPosition = transform.position;
        Vector3 enemyPosition = enemy.transform.position;

        // Calculate the difference in positions
        float diffX = Mathf.Abs(playerPosition.x - enemyPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - enemyPosition.y);

        // Check if the enemy is in an adjacent tile (considering 4-way adjacency)
        return (diffX <= attackRange && diffY <= attackRange) || (diffY <= attackRange && diffX <= 0) || (diffY <= 0 && diffX <= attackRange);
    }
    void endTurn()
    {
        turnManager.turnStatus--;
        hasGone = false;
        hasMoved = false;
        hasTakenAction = false;
        turnManager.state = BattleState.ENEMYTURN;
        actionPointsUsed = 0;
        if (stunnedFor != 0)
        {
            stunnedFor--;
        }
        else
        {
            stunned = false;
        }
    }
    private void OnNextButtonClick()
    {
        nextButtonClicked = true;
        nextButtonClicked = false;
        endTurn();
    }


    //Tile Code

    public Tile GetCurrentTile()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tile"))
            {
                Tile tile = hitCollider.GetComponent<Tile>();
                if (tile != null)
                {
                    return tile;
                }
            }
        }
        return null;
    }

    public Tile GetTileAtPosition(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 14f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tile"))
            {
                Tile tile = hitCollider.GetComponent<Tile>();
                if (tile != null)
                {
                    return tile;
                }
            }
        }
        return null;
    }

    public void MoveToTile(Tile newTile)
    {
        if (currentTile != null)
        {
            currentTile.VacateTile();
        }

        currentTile = newTile;
        if (currentTile != null)
        {
            currentTile.OccupyTileWithPlayer();

            // Calculate the z position 17 units above the current tile's z position
            float newZPosition = currentTile.transform.position.z - 12f;

            // Move the player to the new position while keeping x and y unchanged
            this.transform.position = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, newZPosition);
        }
    }

    //Handle Damage and Death
    public void takeDamage(int physicalDmg, int frozenDmg, int magicDmg, int flameDmg, int shockDmg, int pierceDmg, int acidDmg, int stunDiration)
    {
        if (physicalDmg > 0)
        {
            takePhysicalDamage(physicalDmg);
        }
        if (frozenDmg > 0)
        {
            takeFrozenDamage(frozenDmg);
        }
        if (magicDmg > 0)
        {
            takeMagicalDamage(magicDmg);
        }
        if (flameDmg > 0)
        {
            takeFlameDamage(flameDmg);
        }
        if (shockDmg > 0)
        {
            takeShockDamage(shockDmg);
        }
        if (pierceDmg > 0)
        {
            takePiercingkDamage(pierceDmg);
        }
        if (acidDmg > 0)
        {
            takeAcidDamage(acidDmg);
        }
        if (stunDiration > 0)
        {
            becomeStunned(stunDiration);
        }
    }
    public void becomeStunned(int diration)
    {
        stunnedFor = diration;
        stunned = true;
    }
    public void takePhysicalDamage(int damage)
    {
        if (blockPoints == 0) { blockPoints = 1; }
        hitPoints = hitPoints - (damage / blockPoints);
        currentHP = currentHP - (damage / blockPoints);
        GameLogs.Instance.playerCurrentHP = currentHP;
        setStatBlock();
    }
    public void takeFrozenDamage(int damage)
    {
        if (frozenResistance == 0) { frozenResistance = 1; }
        hitPoints = hitPoints - (damage / frozenResistance);
        currentHP = currentHP - (damage / frozenResistance);
        GameLogs.Instance.playerCurrentHP = currentHP;
        setStatBlock();
    }
    public void takeMagicalDamage(int damage)
    {
        if (magicResistance == 0) { magicResistance = 1; }
        hitPoints = hitPoints - (damage / magicResistance);
        currentHP = currentHP - (damage / magicResistance);
        GameLogs.Instance.playerCurrentHP = currentHP;
        setStatBlock();
    }
    public void takeFlameDamage(int damage)
    {
        if (flameResistance == 0) { flameResistance = 1; }
        hitPoints = hitPoints - (damage / flameResistance);
        currentHP = currentHP - (damage / flameResistance);
        GameLogs.Instance.playerCurrentHP = currentHP;
        setStatBlock();

    }
    public void takeShockDamage(int damage)
    {
        if (shockResistance == 0) { shockResistance = 1; }
        hitPoints = hitPoints - (damage / shockResistance);
        currentHP = currentHP - (damage / shockResistance);
        GameLogs.Instance.playerCurrentHP = currentHP;
        setStatBlock();

    }
    public void takePiercingkDamage(int damage)
    {
        if (blockPoints == 0) { blockPoints = 1; }
        hitPoints = hitPoints - (damage);
        currentHP = currentHP - (damage);
        GameLogs.Instance.playerCurrentHP = currentHP;
        setStatBlock();

    }
    public void takeAcidDamage(int damage)
    {
        if (acidResistance == 0) { acidResistance = 1; }
        hitPoints = hitPoints - (damage / acidResistance);
        currentHP = currentHP - (damage / acidResistance);
        GameLogs.Instance.playerCurrentHP = currentHP;
        setStatBlock();
    }

    void playerIsSlain()
    {
        playerSlain = true;
        turnManager.state = BattleState.LOST;
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        // GameLogs.Instance.playerCurrentHP = currentHP;
    }

    // Healing
    public void Heal(int amount)
    {
        currentHP += amount;
        // Update the GameManager with the new HP value
        GameLogs.Instance.playerCurrentHP = currentHP;
    }
}