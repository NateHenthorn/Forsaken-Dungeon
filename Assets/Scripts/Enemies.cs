using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    //HudImage 
    public EnemyHudImage enemyHUD;
    public bool showHud = false;


    public int vision = 5;
    public GameMap gameMap;
    public int damage = 10;
    public int hitPoints = 100;
    public int moveRate = 500;
    public int coins = 5;
    public int blockPoints = 1;
    public int attackRange = 1;
    public int moveSpeed = 26;
    public int actionPoints = 2;
    public int actionPointsUsed = 0;

    //Resistances
    public int acidResistance = 1;
    public int shockResistance = 1;
    public int flameResistance = 1;
    public int frozenResistance = 1;
    public int magicResistance = 1;

    //Special Damage Types
    public int bleedDamage = 0;
    public int frozenDamage = 0;
    public int flameDamage = 0;
    public int shockDamage = 0;
    public int fireDamage = 0;
    public int magicDamage = 0;
    public int piercingDamage = 0;
    public int acidDamage = 0;

    //Special
    public int stunnedFor = 0;
    public bool stunned = false;
    public int chanceToStun = 0;
    public int stunDuration = 0;
    public int critChance = 0;

    // Reference to the player
    public GameObject player;
    public Player playerScript;
    public GameObject commonEnemyPrefab;
    public GameObject rareEnemyPrefab;
    public GameObject forsakenEnemyPrefab;
    public GameObject legendaryEnemyPrefab;
    public GameObject thisPrefab;
    public int initiative = 0;
    public Enemies next = null;
    public int turnStatus = -1;
    protected bool hasGone = false;
    public bool hasTakenAction = false;
    public bool hasMoved = false;
    // Reference to the game manager
    public GameManager gameManager;
    public TurnManager turnManager;
    //canvas and statblock
    public StatBlock statblock;
    protected Canvas canvas;
    protected StatBlock thisStats;
    public StatTable statTable;
    protected int statBlockNum = -1;
    //Tile
    protected Tile currentTile;
    public Button Next;
    public string name1 = "Enemy";
    public int tileSize = 26;
    //Hover
    private EnemyHover enemyHover;
    //Special Effects
    public string effect1 = "";
    public string effect2 = "";
    public string effect3 = "";
    public Enemies(GameObject prefab)
    {
        thisPrefab = prefab;
    }


    public Enemies()
    {
    }
    protected virtual void Start()
    {
        enemyHover = this.GetComponent<EnemyHover>();
        // Find player object
        vision = vision * tileSize;
        player = GameObject.FindGameObjectWithTag("Player");
        statTable = GameObject.FindGameObjectWithTag("StatTable").GetComponent<StatTable>();
        playerScript = player.GetComponent<Player>();
        playerScript = player.GetComponent<Player>();
        // Find game manager object
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        canvas = FindObjectOfType<Canvas>();
        turnManager = FindObjectOfType<TurnManager>();
        canvas = FindObjectOfType<Canvas>();
        turnManager = GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManager>();
        setTurnStatusNum();
    }


    protected virtual void Update()
    {
        if (initiative == playerScript.initiative)
        {
            initiative--;
        }
        if (hitPoints <= 0)
        {
            enemySlain();
        }
        if (turnManager.turnStatus == initiative && actionPointsUsed < actionPoints && turnManager.state == BattleState.ENEMYTURN)
        {
            hasGone = true;
            startTurn();
            if (IsPlayerAdjacent()) { AttackPlayer(); }
        }
    }

    public virtual void setTurnStatusNum()
    {
    }
    protected virtual void startTurn()
    {
        if (!hasMoved)
        {
            MoveTowardsPlayer();
        }
    }
    protected virtual void MoveTowardsPlayer()
    {
        Tile playerTile = playerScript.GetTileAtPosition(player.transform.position);
        if (playerTile == null)
        {
            Debug.LogWarning("Player tile not found.");
            endTurn();
            return;
        }

        Vector3 direction = (playerTile.transform.position - transform.position).normalized;
        Debug.Log("Direction towards player: " + direction);

        Tile nextTile = FindClosestAdjacentTile(direction);

        if (nextTile != null && !nextTile.IsOccupiedByEnemy() && !nextTile.isSolid && IsPlayerInVision())
        {
            MoveToTile(nextTile);
        }
        else
        {
            Debug.LogWarning("Cannot move towards the player.");
        }
        endTurn();
    }

    protected virtual void makeStatBlock()
    {
        thisStats = statTable.makeStatBlock();
        statBlockNum = statTable.getStatBlockNum() - 1;
        setStatBlock();
    }

    protected virtual void setStatBlock()
    {
        thisStats.setInitiative(initiative);
        thisStats.name1.text = name1;
        thisStats.HP.text = "" + hitPoints;
        thisStats.DMG.text = "" + damage;
        thisStats.SE.text = "NA";
        thisStats.coins.text = "" + coins;
    }

    protected virtual void endTurn()
    {
        if (initiative == playerScript.initiative) { turnManager.state = BattleState.NUETRALTURN; }
        hasGone = false;
        hasTakenAction = false;
        hasMoved = false;
        hasTakenAction = false;
    }



    protected virtual void InitializeEnemyOnTile()
    {
        currentTile = GetCurrentTile();
        if (currentTile != null)
        {
            currentTile.OccupyTileWithEnemy();
        }
        else
        {
            Debug.LogError("Enemy is not on a valid tile at the start.");
        }
    }

    protected virtual Tile GetCurrentTile()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 14f);
        foreach (var hitCollider in hitColliders)
        {
            Tile tile = hitCollider.GetComponent<Tile>();
            if (tile != null)
            {
                return tile;
            }
        }
        return null;
    }

    protected virtual Tile GetTileAtPosition(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 8f);
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

    protected virtual void MoveToTile(Tile newTile)
    {
        if (currentTile != null)
        {
            currentTile.eVacateTile();
        }

        currentTile = newTile;
        if (currentTile != null)
        {
            currentTile.OccupyTileWithEnemy();

            float newZPosition = currentTile.transform.position.z - 12f;
            this.transform.position = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, newZPosition);
            Debug.Log("Moved to tile: " + currentTile.transform.position);
        }
    }

    protected virtual Tile FindClosestAdjacentTile(Vector3 direction)
    {
        List<Tile> adjacentTiles = GetAdjacentTiles();
        if (adjacentTiles == null || adjacentTiles.Count == 0)
        {
            Debug.LogWarning("No adjacent tiles found.");
            return null;
        }

        // Filter and sort tiles based on proximity to the player
        Tile closestTile = null;
        float closestDistance = float.MaxValue;
        foreach (Tile tile in adjacentTiles)
        {
            if (!tile.IsOccupiedByEnemy() && !tile.isSolid)
            {
                float distance = Vector3.Distance(tile.transform.position, player.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTile = tile;
                }
            }
        }

        if (closestTile != null)
        {
            Debug.Log("Chosen tile position: " + closestTile.transform.position);
        }
        else
        {
            Debug.LogWarning("No valid tile found.");
        }

        return closestTile;
    }

    protected virtual List<Tile> GetAdjacentTiles()
    {
        List<Tile> adjacentTiles = new List<Tile>();
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

        foreach (Vector3 dir in directions)
        {
            Tile adjacentTile = GetTileAtPosition(currentTile.transform.position + dir * currentTile.tileDistance);
            if (adjacentTile != null)
            {
                adjacentTiles.Add(adjacentTile);
            }
        }

        return adjacentTiles;
    }

    //Handle Attack

    public virtual bool IsPlayerAdjacent()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 playerPosition = player.transform.position;

        // Calculate the difference in positions
        float diffX = Mathf.Abs(enemyPosition.x - playerPosition.x);
        float diffY = Mathf.Abs(enemyPosition.y - playerPosition.y);

        // Check if the enemy is in an adjacent tile (considering 4-way adjacency)
        return (diffX <= attackRange && diffY <= attackRange) || (diffY <= attackRange && diffX <= 0) || (diffY <= 0 && diffX <= attackRange);
    }
    public virtual bool IsPlayerInVision()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 playerPosition = player.transform.position;

        // Calculate the difference in positions
        float diffX = Mathf.Abs(enemyPosition.x - playerPosition.x);
        float diffY = Mathf.Abs(enemyPosition.y - playerPosition.y);

        // Check if the enemy is in an adjacent tile (considering 4-way adjacency)
        return (diffX <= vision && diffY <= vision) || (diffY <= vision && diffX <= 0) || (diffY <= 0 && diffX <= vision);
    }

    public virtual void AttackPlayer()
    {
        int crit = 1;
        int random = Random.Range(1, 20);
        int stunNum = Random.Range(0, chanceToStun);
        if (stunNum > 0)
        {
            stunNum = stunDuration;
        }
        if (random <= critChance) { crit = 2; print("Crit hit" + this.damage * crit); }
        playerScript.takeDamage(this.damage * crit, this.frozenDamage, this.magicDamage, this.flameDamage, this.shockDamage, this.piercingDamage, this.acidDamage, stunNum);
        hasTakenAction = true;
    }

    //Take Damage and die 
    public virtual void drop()
    {
        playerScript.coins += this.coins;
        playerScript.setStatBlock();
    }
    public virtual void takeDamage(int physicalDmg, int frozenDmg, int magicDmg, int flameDmg, int shockDmg, int pierceDmg, int acidDmg, int stunDiration)
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
    public virtual void becomeStunned(int duration)
    {
        stunnedFor = duration;
        stunned = true;
    }
    public virtual void takePhysicalDamage(int damage)
    {
        if (blockPoints == 0) { blockPoints = 1; }
        hitPoints = hitPoints - (damage / blockPoints);
        //setStatBlock();
    }
    public virtual void takeFrozenDamage(int damage)
    {
        if (frozenResistance == 0) { frozenResistance = 1; }
        hitPoints = hitPoints - (damage / frozenResistance);
       // setStatBlock();
    }
    public virtual void takeMagicalDamage(int damage)
    {
        if (magicResistance == 0) { magicResistance = 1; }
        hitPoints = hitPoints - (damage / magicResistance);
       // setStatBlock();
    }
    public virtual void takeFlameDamage(int damage)
    {
        if (flameResistance == 0) { flameResistance = 1; }
        hitPoints = hitPoints - (damage / flameResistance);
       // setStatBlock();

    }
    public virtual void takeShockDamage(int damage)
    {
        if (shockResistance == 0) { shockResistance = 1; }
        hitPoints = hitPoints - (damage / shockResistance);
        //setStatBlock();

    }
    public virtual void takePiercingkDamage(int damage)
    {
        if (blockPoints == 0) { blockPoints = 1; }
        hitPoints = hitPoints - (damage);
        //setStatBlock();

    }
    public virtual void takeAcidDamage(int damage)
    {
        if (acidResistance == 0) { acidResistance = 1; }
        hitPoints = hitPoints - (damage / acidResistance);
       // setStatBlock();
    }
    public virtual void enemySlain()
    {
        currentTile.eVacateTile();
        //currentTile.Unhighlight();
        drop();
        GameManager.numberOfEntities--;
        //Destroy(thisStats.gameObject);
        // Remove the corresponding StatBlock
        // statTable.updateStatTable(statBlockNum);
        GameLogs.Instance.numEnemiesKilled++;
        Destroy(this.gameObject);
    }

    public virtual void setHUDStats()
    {
        //Base Stats
        enemyHUD.Name1.text = this.name1;
        enemyHUD.HP.text = this.hitPoints + "";
        enemyHUD.BP.text = this.blockPoints + "";
        enemyHUD.MBP.text = "" + this.magicResistance + "";
        enemyHUD.AP.text = "" + this.actionPoints;
        enemyHUD.Initiative.text = "" + this.initiative;
        enemyHUD.range.text = "" + this.attackRange;
        enemyHUD.coins.text = "" + this.coins;
        enemyHUD.BaseDmg.text = this.damage + "";

        //Resistances
        enemyHUD.FlameRes.text = "" + this.flameResistance;
        enemyHUD.ColdRes.text = "" + this.frozenResistance;
        enemyHUD.AcidRes.text = "" + this.acidResistance;
        enemyHUD.ShockRes.text = "" + this.shockResistance;

        //Damages
        enemyHUD.FlameDmg.text = "" + this.flameDamage;
        enemyHUD.ColdDmg.text = "" + this.frozenDamage;
        enemyHUD.AcidDmg.text = "" + this.acidDamage;
        enemyHUD.ShockDmg.text = "" + this.shockDamage;
        enemyHUD.BleedDmg.text = "" + this.bleedDamage;
        enemyHUD.MagicDmg.text = "" + this.magicDamage;
        enemyHUD.PiercingDmg.text = "" + this.piercingDamage;

        //Effects
        enemyHUD.Effect1.text = "" + this.effect1;
        enemyHUD.Effect2.text = "" + this.effect2;
        enemyHUD.Effect3.text = "" + this.effect3;
    }

}