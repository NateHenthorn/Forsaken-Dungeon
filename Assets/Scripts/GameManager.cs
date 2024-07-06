using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static GameObject[] entities = new GameObject[99]; // Updated to hold instances of Enemies
    public int mapWidth = 8;
    public int mapHeight = 8;
    public int maxEnemies = 2;
    public GameObject commonEnemyPrefab;
    public GameObject rareEnemyPrefab;
    public GameObject forsakenEnemyPrefab;
    public GameObject legendaryEnemyPrefab;
    public static int numberOfEntities = 0;
    public PlayerSpawner playerSpawner;
    public EnemySpawner enemySpawner;
    StatTable statTable;
    public int playerHP = 100;
    public int difLevel = -1;
    private GameObject player1;
    private bool isPlayerTurn = true;
    public TurnManager TurnManager;
    public SceneController sceneController;
    public GameMap gameMap;
    public int numEnemiesDead = 0;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    public bool IsPlayerTurn
    {
        get { return isPlayerTurn; }
    }

    private void Start()
    {
        TurnManager = GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManager>();
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner").GetComponent<PlayerSpawner>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        gameMap = GameObject.FindGameObjectWithTag("GameMap").GetComponent<GameMap>();
        sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();

        difLevel = GameLogs.Instance.roomDifficultLevel;
    }
    public int getNumEntites()
    {
        return numEnemiesDead;
    }
    public void addNumberOfEntities()
    {
        numberOfEntities++;
    }
    public void SpawnPlayer(int x, int y)
    {
        playerSpawner.spawnPlayer(x, y);
    }

    public void spawnEnemy(int x, int y)
    {
        int enemyNumber;

        enemyNumber = Random.Range(0, 99) + (difLevel * 10);

        if (enemyNumber < 60)
        {
            enemySpawner.spawnEnemy(x, y, 0);
        }
        else if (enemyNumber > 59 && enemyNumber < 80)
        {
            enemySpawner.spawnEnemy(x, y, 1);
        }
        else if (enemyNumber > 79 && enemyNumber < 97)
        {
            enemySpawner.spawnEnemy(x, y, 2);
        }
        else
        {
            enemySpawner.spawnEnemy(x, y, 3);
        }

        numberOfEntities++;
    }
}