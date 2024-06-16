using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public GameMap gameMap;
    public int damage = 10;
    public int hitPoints = 100;
    public int moveRate = 500;
    public int coins = 5;
    public int blockPoints = 0;
    public int attackRange = 0;
    public int moveSpeed = 26;
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

    public Enemies(GameObject prefab)
    {
        thisPrefab = prefab;
    }


    public Enemies()
    {
    }
    protected virtual void Start()
    {
        // Find player object
        player = GameObject.FindGameObjectWithTag("Player");
        statTable = GameObject.FindGameObjectWithTag("StatTable").GetComponent<StatTable>();
        playerScript = player.GetComponent<Player>();
        //playerScript = player.GetComponent<Player>();
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
        if (hitPoints <= 0)
        {
            enemySlain();
        }
        if (turnManager.turnStatus == initiative && !hasGone && turnManager.state == BattleState.ENEMYTURN) 
        {
            if (IsPlayerAdjacent() && !hasTakenAction){AttackPlayer();}
        }
    }

    public virtual void setTurnStatusNum()
    {
    }
    protected virtual void startTurn()
    { 
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

        if (nextTile != null && !nextTile.IsOccupiedByEnemy() && !nextTile.isSolid)
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
            currentTile.VacateTile();
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
        return (diffX <= 26 && diffY <= 26) || (diffY <= 26 && diffX <= 0) || (diffY <= 0 && diffX <= 26);
    }

    public virtual void AttackPlayer()
    {
        playerScript.takePhysicalDamage(this.damage);
        hasTakenAction = true;
    }

    //Take Damage and die 
    public virtual void drop()
    {
        playerScript.coins += this.coins;
        playerScript.setStatBlock();
      }
    public virtual void takePhysicalDamage(int damage)
    {
        if (damage > blockPoints)
        {
            hitPoints = hitPoints - (damage - blockPoints);
            setStatBlock();
        }

    }
    public virtual void enemySlain()
    {
        currentTile.VacateTile();
        currentTile.Unhighlight();
        drop();
        GameManager.numberOfEntities--;
        Destroy(thisStats.gameObject);
        // Remove the corresponding StatBlock
       // statTable.updateStatTable(statBlockNum);
        Destroy(this.gameObject);
    }

}
