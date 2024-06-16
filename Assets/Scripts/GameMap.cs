using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameMap : MonoBehaviour
{
    public GameObject emptyTile;
    public GameObject monsterTile;
    public GameObject obstacleTile;
    public GameObject playerSpawnTile;
    public GameObject player;
    public GameObject mainCamera;
    public GameObject commonEnemy;
    public GameObject rareEnemy;
    public GameObject forsakenEnemy;
    public GameObject legendaryEnemy;
    public StatTable statTable;
    public float heightOffset = 1;
    public bool mapCreated = false;
    int roomSize = 0; //0 Small Room, 1 Med Room, 2 Large Room
    public GameManager manager;
    public int difLevel = -1;
    SceneController sceneController;

    // Start is called before the first frame update

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        difLevel = sceneController.roomDif;
        manager.difLevel = difLevel;
        createNewMap();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void createNewMap()
    {

        roomSize = Random.Range(0, 2);
        createNewRoom(roomSize);
    }

    void createNewRoom(int size)
    {
        float[,] tileMap;
        int mapSize = 0;
        if (size == 0)
        {
            mapSize = 8;
        }
        else if (size == 1)
        {
            mapSize = 12;
        }
        else if (size == 2)
        {
            mapSize = 16;
        }

        tileMap = new float[mapSize, mapSize];
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (i == 0 && j == 0)
                {
                    tileMap[i, j] = 100;
                }
                else
                {
                    tileMap[i, j] = Random.Range(0, 99);
                }


            }
        }
        GameObject[,] mapLayout = new GameObject[mapSize, mapSize];
        mapLayout = tileSetter(tileMap, mapSize);
    }

    GameObject[,] tileSetter(float[,] tileMap, int mapSize)
    {
        GameObject[,] mapLayout = new GameObject[mapSize, mapSize];

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (tileMap[i, j] < 85)
                {
                    mapLayout[i, j] = emptyTile;
                }
                else if (tileMap[i, j] > 94 && tileMap[i, j] < 100)
                {
                    mapLayout[i, j] = monsterTile;
                }
                else if (tileMap[i, j] == 100)
                {
                    mapLayout[i, j] = playerSpawnTile;
                }
                else
                {
                    mapLayout[i, j] = obstacleTile;
                }
            }
        }
        tileSetter(mapLayout, mapSize);
        return mapLayout;
    }

    void tileSetter(GameObject[,] mapLayout, int mapSize)
    {

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                Instantiate(mapLayout[i, j], new Vector3(i * 26, j * 26, -1), transform.rotation);

                if (mapLayout[i, j] == monsterTile)
                {
                    manager.spawnEnemy(i * 26, j * 26);
                }
                if (mapLayout[i, j] == playerSpawnTile)
                {
                    manager.SpawnPlayer(i * 26, j * 26);
                }
            }
        }
        mapCreated = true;
    }
}