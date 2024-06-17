using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, NUETRALTURN, WON, LOST, ROOMCOMPLETE, SHOP }
public class TurnManager : MonoBehaviour
{
    public GameManager gameManager;
    public Enemies enemies;
    public GameObject commonEnemyPrefab;
    public GameObject rareEnemyPrefab;
    public GameObject forsakenEnemyPrefab;
    public GameObject legendaryEnemyPrefab;
    private Queue<GameObject> turnQueue = new Queue<GameObject>();
    public static entityNode[] turnByInitiative = new entityNode[99];
    public bool isPlayerTurn = false;
    public int numEnemies = GameManager.numberOfEntities;
    public static int currentLength = 0;
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public StatTable statTable;
    public GameObject player;
    public GameMap gameMap;
    public int turnStatusNum = 0;
    public Transform playerTile;
    public Transform enemyTile;
    public int playerInitiative;
    public Player player1;
    public int turnStatus = -1;

    public static TurnManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        numEnemies = GameManager.numberOfEntities;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameMap = GameObject.FindGameObjectWithTag("GameMap").GetComponent<GameMap>();
        initializeList();
        state = BattleState.START;
        SetupBattle();
        turnStatus++;
        playerInitiative = player1.initiative;
        turnStatusNum = playerInitiative;
    }
    private void Update()
    {
        numEnemies = GameManager.numberOfEntities;
        if (turnStatus < 0) { turnStatus = turnStatusNum + 1; }
        if (turnStatus == playerInitiative)
        {
            state = BattleState.PLAYERTURN;
        }
        if (gameMap.mapCreated)
        {
            switch (state)
            {
                case BattleState.ENEMYTURN: if (turnStatus != playerInitiative) { StartCoroutine(EnemysTurn()); } break;

                case BattleState.PLAYERTURN: if (turnStatus == playerInitiative) { PlayerTurn(); StopCoroutine(EnemysTurn()); } break;

                case BattleState.LOST: LoadSlainScreen(); break;

                case BattleState.ROOMCOMPLETE: state = BattleState.SHOP; break;

                case BattleState.SHOP: LoadShopScene(); break;

                default: break;
            }
        }
        if (player1.playerSlain) { state = BattleState.LOST; }
        else if (numEnemies == 0 && state != BattleState.SHOP) { state = BattleState.SHOP; }
    }

    private void LoadSlainScreen()
    {
        SceneManager.LoadScene("Died");
        StatTable.currentYOffset = 0;
    }
    private void LoadShopScene()
    {
        Debug.Log("Loading PrestigeShop scene...");
        SceneManager.LoadScene("PrestigeShop");
        StatTable.currentYOffset = 0;
    }
    void SetupBattle()
    {
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        isPlayerTurn = true;
    }

    public void initializeList()
    {
        entityNode newNode = new entityNode();
        for (int i = 0; i < 99; i++)
        {
            turnByInitiative[i] = newNode;
        }
    }



    public void setTurnStatusNum(int initiative)
    {
        if (turnStatusNum < initiative)
        {
            turnStatusNum = initiative;
        }

    }


    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        StartCoroutine(EnemysTurn());
    }

    IEnumerator EnemysTurn()
    {
        
        yield return new WaitForSeconds(0.2f); // Delay between enemy turns
        decreaseTurnStatus();
    }

    private void decreaseTurnStatus()
    {
        if (turnStatus != playerInitiative)
        {
    turnStatus--;
        }
        
    }
    }






public class entityNode
{
    public int initiative;
    public GameObject entity;
    public entityNode next;
    public int entityType;
    public entityNode()
    {
        initiative = 0;
        entity = null;
        next = null;
        entityType = 0;
    }
    public entityNode(GameObject enemy, int type)
    {
        //type 0 player, 1 common, 2 rare, 3 legendary, 4 forsaken
        entity = enemy;
        next = null;
        entityType = type;

    }

    public int getType()
    {
        return entityType;
    }
}