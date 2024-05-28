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
    public int dmg = 40;
    public int hitPoints = 100;
    public int moveRate = 1;
    public int initiative = 10;
    public GameObject rightHand;
    public GameObject leftHand;
    public int actionPoints = 0;
    public int blockPoints = 0;
    public int luck = 0;
    public int coins = 0;
    public int levelPoints = 0;
    public int costToLevel = 1;
    public int strength;
    public int dextartity;
    public int intelligence;
    public int faith;
    public bool playerSlain = false;   
    public GameObject turnHolder;
    public int test = 0;
    public bool actionDone = false;
    public int moveSpeed = 5000;
    public int attackRange = 1;
    public LayerMask enemyLayer;
    public float moveDuration = 5;
    public int turnStatus = 0;
    private GameManager gameManager;
    private TurnManager turnManager;
    private bool hasGone = false;
    private Rigidbody2D rb;
    //canvas and statblock
    public StatBlock statblock;
    private Canvas canvas;
    public Button Next;
    public bool hasMoved = false;
    public bool hasTakenAction = false;
    private Camera mainCamera;
    public int tileSize = 26;
    private StatBlock thisStatBlock;
    public StatTable statTable;


    private void Start()
    {
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
        statTable = GameObject.FindGameObjectWithTag("StatTable").GetComponent<StatTable>();
        canvas = FindObjectOfType<Canvas>();
        turnManager = FindObjectOfType<TurnManager>();
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
        if ((turnManager.turnStatus == initiative) && !hasGone)
        {
            hasGone = true;
            turnManager.isPlayerTurn = false;
            startTurn();
            }
        if (hitPoints <= 0)
        {
            playerIsSlain();
        }
    }


    void setStats()
    {
        moveRate = 1 * tileSize;
        attackRange = (attackRange * tileSize);
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
        thisStatBlock.HP.text = "" + hitPoints;
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
        HandleAttack();
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

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                // Attack enemy
                Destroy(enemy.gameObject);
            }
        }
    }

    void AttackEnemy(Enemies enemy)
    { 
        enemy.takePhysicalDamage(this.dmg);
        print("DMG" + this.dmg + " " + dmg);
        hasTakenAction = true;
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
                    if (enemyScript != null && !hasTakenAction)
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

    public void takePhysicalDamage(int damage)
    {
        if (dmg > blockPoints)
        {
            hitPoints = hitPoints - (damage - blockPoints);
            setStatBlock();
        }
    }

    void playerIsSlain()
    {
        playerSlain = true;
        turnManager.state = BattleState.LOST;
        Destroy(this.gameObject);
    }
}