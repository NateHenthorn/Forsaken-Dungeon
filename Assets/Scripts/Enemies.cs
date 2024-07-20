using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Unity.VisualScripting;
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
    public int origionalHitPoints = 0;
    public int moveRate = 500;
    public int coins = 5;
    public int blockPoints = 1;
    public int attackRange = 1;
    public int moveSpeed = 26;
    public int actionPoints = 2;
    public int actionPointsUsed = 0;
    public int enemyNum = -1;
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
    public int stunChance = 0;
    public int critChance = 0;
    public bool friendlyFire = false; //Used for Abnormal
    public bool killedEnemy = false; //Used for Abnormal
    public bool isAcidic = false; //Used for Acidic
    public bool bounded = false; // Used for bounded
    public bool charged = false; //Used for charged enemies
    public bool ashen = false; //Used for ashen enemies
    public int numEnemiesDead = 0; //Used for ashen enemies
    public bool duplicating = false; //Used for duplicating
    public bool thawed = false; //Used for thawed enemies
    public bool statued = false; // for statued enemies
    public bool hasTakenDamage = false; //Used for statued enemies
    public bool isSlimy = false; //used for slimy creatures
    public bool shocking = false; //used for shocking creatures
    public bool sacrificing = false; //Used for sacrificing
    public bool rotting = false; //used for rotting
    public bool rastraining = false; //used for rastraining
    public bool ressurecting = false; //used for ressurecting
    public bool pulling = false; //Used for pulling
    public bool nether = false; //used for nether
    public bool plagued = false; //used for plagued
    public bool leaching = false; //used for leaching/lifeSteal
    public bool grabbing = false; //used for grabbing
    public bool invincible = false; //used for invincible
    public bool hel = false; //Used for hel
    public bool healing = false; //Used for healing
    public bool hidden = false; // Used for Hidden
    public bool flying = false; //Used for flying
    public bool enraging = false; //used for enraging
    public bool flowerCovered = false; //used for Flower Covered

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
    protected Tile startingTile;
    protected Tile currentTile;
    public Button Next;
    public int tileSize = 26;
    //Naming
    public string name1 = "Enemy";
    public string baseName = "";
    public string prefixName = "";
    public int prefixNum = -1;
    //Hover
    private EnemyHover enemyHover;
    //Special Effects
    public string effect1 = "";
    public string effect2 = "";
    public string effect3 = "";
    public int damageTypeNum = -1;
    public int specialEffectNuMCounter = 0;
    public Enemies(GameObject prefab)
    {
        thisPrefab = prefab;
    }


    public Enemies()
    {
    }
    protected virtual void Start()
    {
        startingTile = GetCurrentTile();
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
        if (duplicating) { duplicating = false; EnemySpawner.Instance.spawnEnemy((int)this.transform.position.x, (int)this.transform.position.y, enemyNum); GameManager.Instance.addNumberOfEntities(); }
        if (ashen) {if (numEnemiesDead != GameManager.Instance.numEnemiesDead) {this.damage++; numEnemiesDead++; } }

        if (initiative == playerScript.initiative)
        {
            initiative--;
        }
        if (hitPoints <= 0)
        {
            enemySlain();
        }
        if (turnManager.turnStatus == initiative && actionPointsUsed < actionPoints && turnManager.state == BattleState.ENEMYTURN && !turnManager.turnHasBeenTaken && !statued)
        {
            if (!friendlyFire){ hasGone = true; startTurn();
                if (IsPlayerAdjacent()) { AttackPlayer(); }
            }
            else {hasGone = true;startTurn();
                if (IsAnyoneAdjacent()) { AttackAnyone(); }}

            if (isAcidic) { playerScript.takeAcidDamage(1); }

        }
        if (killedEnemy) { killedEnemy = false;prefixNum = Random.Range(0, 87); applySpecialEffect();}
    }

    public virtual void setTurnStatusNum()
    {
    }
    protected virtual void startTurn()
    {
        if (enraging && hitPoints <= (origionalHitPoints/2)) { enraging = false; hitPoints = (int)(hitPoints * 1.5); damage = (int)(damage * 1.5); }
        if (healing)
        {
            int healingChance = Random.Range(0, 1);
            if(healingChance == 1) { this.hitPoints += damage; }
        }
        if (pulling) {
            int rangeHolder = attackRange;
            attackRange = 3 * tileSize;
            if (IsPlayerAdjacent()) { playerScript.MoveOneTileTowards(this.transform.position); playerScript.MoveOneTileTowards(this.transform.position); playerScript.MoveOneTileTowards(this.transform.position);}
            attackRange = rangeHolder; }

        if (!hasMoved && !statued)
        {
            if (!friendlyFire)
            {
            MoveTowardsPlayer();
            }
            else
            {
                MoveTowardsAnyone();
            }
        }
        turnManager.turnHasBeenTaken = true;
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
        float distance = Vector3.Distance(currentTile.transform.position, startingTile.transform.position);
        if (nextTile != null && !nextTile.IsOccupiedByEnemy())
        {
           
            if (!bounded)
            {
            MoveToTile(nextTile);
            }
            else if (distance < 3 * tileSize)
            {
                MoveToTile(nextTile);
            }
        }
        else
        {
            Debug.LogWarning("Cannot move towards the player.");
        }
        endTurn();
    }

    protected virtual void MoveTowardsAnyone()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Assuming enemies have "Enemy" tag
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming player has "Player" tag
        float tileDistance = Vector3.Distance(currentTile.transform.position, startingTile.transform.position);
        Vector3 enemyPosition = transform.position;
        GameObject target = null;
        float nearestDistance = Mathf.Infinity;

        // Find the nearest enemy or player
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Vector3 targetPosition = enemy.transform.position;
                float distance = Vector3.Distance(enemyPosition, targetPosition);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    target = enemy;
                }
            }
        }

        // Check distance to player
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            float distanceToPlayer = Vector3.Distance(enemyPosition, playerPosition);

            if (distanceToPlayer < nearestDistance)
            {
                target = player;
            }
        }

        // If a target is found, move towards it
        if (target != null)
        {
            Tile targetTile = playerScript.GetTileAtPosition(target.transform.position);
            if (targetTile == null)
            {
                Debug.LogWarning("Target tile not found.");
                endTurn();
                return;
            }

            Vector3 direction = (targetTile.transform.position - transform.position).normalized;
            Debug.Log("Direction towards target: " + direction);

            Tile nextTile = FindClosestAdjacentTile(direction);

            if (nextTile != null && !nextTile.IsOccupiedByEnemy() && !nextTile.isSolid && IsPlayerInVision())
            {
                if (!bounded)
                {
                    MoveToTile(nextTile);
                }
                else if (tileDistance < 3 * tileSize)
                {
                    MoveToTile(nextTile);
                }
            }
            else
            {
                Debug.LogWarning("Cannot move towards the target.");
            }
        }
        else
        {
            Debug.LogWarning("No valid target found to move towards.");
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
            if (!tile.IsOccupiedByEnemy())
            {
                if (flying)
                {
                float distance = Vector3.Distance(tile.transform.position, player.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTile = tile;
                }
                }
                else if (!tile.isSolid)
                {
                    float distance = Vector3.Distance(tile.transform.position, player.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTile = tile;
                    }
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
    public virtual bool IsAnyoneAdjacent()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Assuming enemies have "Enemy" tag
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming player has "Player" tag

        Vector3 enemyPosition = transform.position;
        float nearestDistance = Mathf.Infinity;
        bool isAdjacent = false;
        float distanceToPlayer;
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            distanceToPlayer = Vector3.Distance(enemyPosition, playerPosition);

            if (distanceToPlayer <= attackRange)
            {
                isAdjacent = true;
            }
        }
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Vector3 targetPosition = enemy.transform.position;
                float distance = Vector3.Distance(enemyPosition, targetPosition);

                // Check if the enemy is adjacent or within attack range
                if (distance <= attackRange)
                {
                    isAdjacent = true;
                    break;
                }

                // Track the nearest enemy
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                }
            }
        }

        return isAdjacent;
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
        int restrainNum = Random.Range(0, 3);
        if (sacrificing) { playerScript.takeDamage(15, this.frozenDamage, this.magicDamage, this.flameDamage, this.shockDamage, this.piercingDamage, this.acidDamage, 0);  enemySlain(); }
        if (rotting) { playerScript.MoveOneTileTowards(playerScript.FindClosestEnemy().transform.position); }
        int chargeNum = 1;
        if (charged){chargeNum = Random.Range(1, 2);}
        int crit = 1;
        int random = Random.Range(1, 20);
        int stunNum = Random.Range(1, 100);
        if (stunNum <= stunChance)
        {
            stunNum = stunDuration;
        }
        if (random <= critChance) { crit = 2; print("Crit hit" + this.damage * crit); }
        if (chargeNum == 1)
        {
            playerScript.takeDamage(this.damage * crit, this.frozenDamage, this.magicDamage, this.flameDamage, this.shockDamage, this.piercingDamage, this.acidDamage, stunNum);
            if (leaching) { hitPoints += damage;}
            if (charged) { shockDamage = 0; }
        }
        else {shockDamage += 1;}
        if (hel) { playerScript.TransportPlayerToRandomTile(); }
        if (rastraining && restrainNum == 3) { playerScript.restrainPlayer(2); }
        int flowerRandom = Random.Range(0, 3);
        if (flowerCovered && flowerRandom == 3) { playerScript.takeBurnDamage(1, damage, 3);}
        if (thawed) {playerScript.takeBurnDamage(1, damage, 3); }
        if (nether) { playerScript.takeBurnDamage(3, flameDamage, 3); }
        if (plagued) { playerScript.takeBurnDamage(5, acidDamage, 3); }
        if (shocking) { shockDamage = shockDamage * 2; }
        hasTakenAction = true;
    }

    public virtual void AttackAnyone()
    {
        int chargeNum = 1;
        if (charged) { chargeNum = Random.Range(1, 2); }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Assuming enemies have "Enemy" tag
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming player has "Player" tag

        Vector3 enemyPosition = transform.position;
        GameObject target = null;
        float nearestDistance = Mathf.Infinity;
        bool targetEnemy = false;
        // Find the nearest enemy
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Vector3 targetPosition = enemy.transform.position;
                float distance = Vector3.Distance(enemyPosition, targetPosition);

                if (distance <= attackRange && distance < nearestDistance)
                {
                    nearestDistance = distance;
                    target = enemy;
                    targetEnemy = true;
                }
            }
        }

        // Check distance to player
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            float distanceToPlayer = Vector3.Distance(enemyPosition, playerPosition);

            if (distanceToPlayer <= attackRange && distanceToPlayer < nearestDistance)
            {
                nearestDistance = distanceToPlayer;
                target = player;
                targetEnemy = false;
            }
        }

        // If a target is found, attack it
        if (target != null)
        {
            int crit = 1;
            int random = Random.Range(1, 20);
            int stunNum = Random.Range(0, chanceToStun);

            if (stunNum > 0)
            {
                stunNum = stunDuration;
            }

            if (random <= critChance)
            {
                crit = 2;
                Debug.Log("Critical hit! Damage: " + this.damage * crit);
            }

            // Attack the target
            if (targetEnemy)
            {
            Enemies targetScript = target.GetComponent<Enemies>();
            if (targetScript != null)
            {
                if (targetScript.hitPoints - (this.damage * crit + this.frozenDamage + this.magicDamage + this.flameDamage + this.shockDamage + this.piercingDamage + this.acidDamage) <=0)
                {
                    killedEnemy = true;
                }
                targetScript.takeDamage(this.damage * crit, this.frozenDamage, this.magicDamage, this.flameDamage, this.shockDamage, this.piercingDamage, this.acidDamage, stunNum);
                hasTakenAction = true;
            }
            }
            else{AttackPlayer();}
        }
    }

    //Take Damage and die 
    public virtual void drop()
    {
        playerScript.coins += this.coins;
        playerScript.setStatBlock();
    }
    public virtual void takeDamage(int physicalDmg, int frozenDmg, int magicDmg, int flameDmg, int shockDmg, int pierceDmg, int acidDmg, int stunDiration)
    {
        if (invincible) { invincible = false; return;}
        if (statued) {statued = false;return;}
        if (isSlimy) { isSlimy = false; playerScript.luck -= 2; }
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
        int ressurectingNum = Random.Range(0, 1);
        if (ressurecting && ressurectingNum == 1) { hitPoints = origionalHitPoints; ressurecting = false; return; }
        GameManager.Instance.numEnemiesDead++;
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

    protected virtual string pickPrefix(int value)
    {
        string rValue = "";
        damageTypeNum = value;
        switch (value)
        {
            case 0: rValue = "Abnormal "; break;
            case 1: rValue = "Acidic "; break;
            case 2: rValue = "Aggressing "; break;
            case 3: rValue = "Apocalyptic "; break;
            case 4: rValue = "Ashen "; break;
            case 5: rValue = "Blackened "; break;
            case 6: rValue = "Blighted "; break;
            case 7: rValue = "Bloody "; break;
            case 8: rValue = "Blue "; break;
            case 9: rValue = "Bounded "; break;
            case 10: rValue = "Charged "; break;
            case 11: rValue = "Charred "; break;
            case 12: rValue = "Cold "; break;
            case 13: rValue = "Combative "; break;
            case 14: rValue = "Conductive "; break;
            case 15: rValue = "Crazed "; break;
            case 16: rValue = "Crimson "; break;
            case 17: rValue = "Crowned "; break;
            case 18: rValue = "Dire "; break;
            case 19: rValue = "Duplicating "; break;
            case 20: rValue = "Eldritch "; break;
            case 21: rValue = "Electric "; break;
            case 22: rValue = "Enchanting  "; break;
            case 23: rValue = "Enormous "; break;
            case 24: rValue = "Enraging "; break;
            case 25: rValue = "Fast "; break;
            case 26: rValue = "Feasting  "; break;
            case 27: rValue = "Flaming  "; break;
            case 28: rValue = "Flower-Covered "; break;
            case 29: rValue = "Flying  "; break;
            case 30: rValue = "Furious  "; break;
            case 40: rValue = "Fluttering "; break;
            case 41: rValue = "Glacial "; break;
            case 42: rValue = "Grabbing "; break;
            case 43: rValue = "Graceful  "; break;
            case 44: rValue = "Green  "; break;
            case 45: rValue = "Healing "; break;
            case 46: rValue = "Hel "; break;
            case 47: rValue = "Hidden "; break;
            case 48: rValue = "Holy "; break;
            case 49: rValue = "Icy "; break;
            case 50: rValue = "Infernal "; break;
            case 51: rValue = "Invincible "; break;
            case 52: rValue = "Jade  "; break;
            case 53: rValue = "Leeching "; break;
            case 54: rValue = "Luxurious  "; break;
            case 55: rValue = "Mimic "; break;
            case 56: rValue = "Morphing "; break;
            case 57: rValue = "Nether "; break;
            case 58: rValue = "Nightfallen "; break;
            case 59: rValue = "Piercing "; break;
            case 60: rValue = "Plagued "; break;
            case 61: rValue = "Pulling "; break;
            case 62: rValue = "Purple  "; break;
            case 63: rValue = "Rabid  "; break;
            case 64: rValue = "Reanimating "; break;
            case 65: rValue = "Resurrecting "; break;
            case 66: rValue = "Restraining  "; break;
            case 67: rValue = "Rotting  "; break;
            case 68: rValue = "Royal "; break;
            case 69: rValue = "Sacrificing  "; break;
            case 70: rValue = "Scrawny  "; break;
            case 71: rValue = "Screeching  "; break;
            case 72: rValue = "Shocking  "; break;
            case 73: rValue = "Soaring  "; break;
            case 74: rValue = "Silky "; break;
            case 75: rValue = "SkinHarvesting "; break;
            case 76: rValue = "Slimy  "; break;
            case 77: rValue = "Smoldering "; break;
            case 78: rValue = "Statued "; break;
            case 79: rValue = "Summoning  "; break;
            case 80: rValue = "Sweeping  "; break;
            case 81: rValue = "Tenebrous "; break;
            case 82: rValue = "Thawed  "; break;
            case 83: rValue = "Thundering  "; break;
            case 84: rValue = "Tiny "; break;
            case 85: rValue = "Trading  "; break;
            case 86: rValue = "Transposing "; break;
            case 87: rValue = "Wise "; break;
            default: rValue = ""; break;
        }
        // damageType = rValue;

        return rValue;

    }

    protected virtual string applySpecialEffect()
    {
        string rValue = "";
        switch (prefixNum)
        {
            case 0: rValue = "Abnormal "; this.friendlyFireOn(); break;
            case 1: rValue = "Acidic "; this.isAcidic = true;  break;
            case 2: rValue = "Aggressing "; this.actionPoints += 1; break;
            case 3: rValue = "Apocalyptic "; this.damage += 2;  break;
            case 4: rValue = "Ashen "; this.damage += GameManager.Instance.numEnemiesDead * 2; this.ashen = true; break;
            case 5: rValue = "Blackened "; break;
            case 6: rValue = "Blighted "; break;
            case 7: rValue = "Bloody "; this.damage += (damage / 2); this.actionPoints += 1; break;
            case 8: rValue = "Blue "; this.frozenDamage += 1; this.hitPoints -= (hitPoints/4); break;
            case 9: rValue = "Bounded "; this.bounded = true;  break;
            case 10: rValue = "Charged "; this.charged = true;  break;
            case 11: rValue = "Charred "; this.flameDamage += (this.damage/2); break;
            case 12: rValue = "Cold "; this.frozenDamage += 1; break;
            case 13: rValue = "Combative "; this.actionPoints += 2; break;
            case 14: rValue = "Conductive "; shockDamage = GameManager.Instance.getNumEntites() * 2; ; break;
            case 15: rValue = "Crazed "; break;
            case 16: rValue = "Crimson "; break;
            case 17: rValue = "Crowned "; break;
            case 18: rValue = "Dire "; break;
            case 19: rValue = "Duplicating "; duplicating = true; break;
            case 20: rValue = "Eldritch "; break;
            case 21: rValue = "Electric "; actionPoints = actionPoints +3; break;
            case 22: rValue = "Enchanting  "; break;
            case 23: rValue = "Enormous "; hitPoints += (int)(hitPoints * 1.5); actionPoints =1; break;
            case 24: rValue = "Enraging "; enraging = true;  break;
            case 25: rValue = "Fast "; actionPoints = actionPoints + 1; ; break;
            case 26: rValue = "Feasting  "; leaching = true; damage = (int)(damage * .25) ; break;
            case 27: rValue = "Flaming  "; flameDamage = flameDamage + 2; break;
            case 28: rValue = "Flower-Covered "; flowerCovered = true;  break;
            case 29: rValue = "Flying  "; flying = true; break;
            case 30: rValue = "Furious  "; attackRange = attackRange * 2; break;
            case 40: rValue = "Fluttering "; flying = true; pulling = true;  break;
            case 41: rValue = "Glacial "; frozenDamage += 2; break;
            case 42: rValue = "Grabbing "; grabbing = true; break;
            case 43: rValue = "Graceful  "; damage = damage + (damage / 4); break;
            case 44: rValue = "Gray  "; hitPoints = (hitPoints / 2); acidDamage += 1; break;
            case 45: rValue = "Healing "; healing = true; break;
            case 46: rValue = "Hel "; hel = true; break;
            case 47: rValue = "Hidden "; hidden = true; break;
            case 48: rValue = "Holy "; shockDamage += damage; ; break;
            case 49: rValue = "Icy "; stunDuration = 1; stunChance = 30; frozenDamage += 1; break;
            case 50: rValue = "Infernal "; break;
            case 51: rValue = "Invincible "; invincible = true; break;
            case 52: rValue = "Jade  "; break;
            case 53: rValue = "Leeching "; leaching = true; break;
            case 54: rValue = "Luxurious  "; break;
            case 55: rValue = "Mimic "; damage += playerScript.itemDamage; grabbing = true; break;
            case 56: rValue = "Morphing "; damage = playerScript.dmg; attackRange = playerScript.attackRange; break;
            case 57: rValue = "Nether "; nether = true; fireDamage = damage; damage = 0; break;
            case 58: rValue = "Nightfallen "; break;
            case 59: rValue = "Piercing "; piercingDamage = damage; damage = 0; break;
            case 60: rValue = "Plagued "; plagued = true; acidDamage = damage; damage = 0; break;
            case 61: rValue = "Pulling "; pulling = true;  break;
            case 62: rValue = "Purple  "; acidDamage = (this.actionPoints *2); break;
            case 63: rValue = "Rabid  "; break;
            case 64: rValue = "Reanimating "; break;
            case 65: rValue = "Resurrecting "; ressurecting = true; origionalHitPoints = hitPoints; break;
            case 66: rValue = "Restraining  "; rastraining = true; break;
            case 67: rValue = "Rotting  "; rotting = true; break;
            case 68: rValue = "Royal "; break;
            case 69: rValue = "Sacrificing  "; sacrificing = true; break;
            case 70: rValue = "Scrawny  "; hitPoints = (hitPoints / 2); break;
            case 71: rValue = "Screeching  "; break;
            case 72: rValue = "Shocking  "; shocking = true; shockDamage = 1; break;
            case 73: rValue = "Soaring  "; break;
            case 74: rValue = "Silky "; break;
            case 75: rValue = "SkinHarvesting "; break;
            case 76: rValue = "Slimy  "; isSlimy = true;  break;
            case 77: rValue = "Smoldering "; break;
            case 78: rValue = "Statued "; statued = true; break;
            case 79: rValue = "Summoning  "; duplicating = true;  break;
            case 80: rValue = "Sweeping  "; break;
            case 81: rValue = "Tenebrous "; break;
            case 82: rValue = "Thawed  "; thawed = true; break;
            case 83: rValue = "Thundering  "; break;
            case 84: rValue = "Tiny "; actionPoints += 2; hitPoints = (hitPoints / 2);  break;
            case 85: rValue = "Trading  "; coins += 10; break;
            case 86: rValue = "Transposing "; moveRate += 120; moveSpeed += 120; break;
            case 87: rValue = "Wise "; magicDamage += 1; break;
            default: rValue = "";break;
        }
        // damageType = rValue;
        switch (specialEffectNuMCounter)
        {
            case 0: effect1 = rValue; break;
            case 1: effect2 = rValue; break;
            case 2: effect3 = rValue; break;
            default: break;
        }
        return rValue;

    }

    public virtual void friendlyFireOn()
    {
        friendlyFire = true;

    }
}