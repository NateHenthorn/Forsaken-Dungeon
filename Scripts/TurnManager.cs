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
    public int turnStatusNum = 0;
    public Transform playerTile;
    public Transform enemyTile;
    public int playerInitiative;
    public Player player1;
    public int turnStatus = -1;

    public static TurnManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null){Instance = this;}
        else {Destroy(gameObject);}
    }

    private void Start()
    {
        numEnemies = GameManager.numberOfEntities;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
        if (turnStatus < 0){ turnStatus = turnStatusNum +1; }
        switch (state) {
            case BattleState.ENEMYTURN:if (turnStatus != playerInitiative){StartCoroutine(EnemysTurn());} break;

            case BattleState.PLAYERTURN: if (turnStatus == playerInitiative) { PlayerTurn();}break;

            case BattleState.LOST: LoadSlainScreen(); break;

            case BattleState.ROOMCOMPLETE: state = BattleState.SHOP;break;

            case BattleState.SHOP: LoadShopScene(); break;

            default: break;
        }

        if (player1.playerSlain) {state = BattleState.LOST;}
        else if (numEnemies == 0 && state != BattleState.SHOP){state = BattleState.SHOP;}
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

    public void addToQueue(GameObject entity, int type)
    {
        entityNode tempNode = new entityNode(entity, type);
        currentLength++;
        for (int i = 0; i < currentLength; i++)
        {

            if (turnByInitiative[i] == null)
            {
                turnByInitiative[i] = tempNode;
                break;
            }
        }
        print(currentLength);
        if (currentLength > 1)
        {
            sortQueue();
        }


    }

    public void setTurnStatusNum(int initiative)
    {
        if (turnStatusNum < initiative)
        {
            turnStatusNum = initiative;
        }

    }
    void sortQueue()
    {
        entityNode tempNode = new entityNode();
        int maxInitiative = 0;
        int secondMaxInitiave = 0;
        int currentInitiave = 0;
        int length = currentLength - 1;
        int adjustor = 0;

        for (int j = 0; j < length; j++)
        {
            if (turnByInitiative[j] == null)
            {
                break;
            }
            int index1 = 0;
            int index2 = 0;
            for (int i = 0; (i + adjustor) < length; i++)
            {
                if (turnByInitiative[(i + adjustor)] == null)
                {
                    break;
                }
                tempNode = turnByInitiative[(i + adjustor)];
                currentInitiave = tempNode.initiative;
                if (currentInitiave > maxInitiative)
                {
                    secondMaxInitiave = maxInitiative;
                    maxInitiative = currentInitiave;
                    index2 = index1;
                    index1 = (i + adjustor);
                }
            }
            swapNodes(adjustor, index1);
            if (index1 != index2 && currentLength > (adjustor + 2))
            {
                swapNodes((adjustor + 1), index2);
            }
            adjustor += 2;

        }

    }

    void swapNodes(int a, int b)
    {

        entityNode tempNode = null;
        if (turnByInitiative[a] == turnByInitiative[b])
        {
            return;
        }
        tempNode = turnByInitiative[a];
        turnByInitiative[a] = turnByInitiative[b];
        turnByInitiative[b] = tempNode;
    }
    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        StartCoroutine(EnemysTurn());
    }

    private IEnumerator EnemysTurn()
    {
        StartCoroutine(increaseTurnStatus());
       // turnStatus++;
        yield return new WaitForSeconds(.5f); // Delay between enemy turns 

    }

    private IEnumerator increaseTurnStatus()
    {
        turnStatus--;

        yield return new WaitForSeconds(0f);
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
