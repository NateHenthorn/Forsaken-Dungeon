using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MapCreator : MonoBehaviour
{

    public static MapCreator Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }


    public int roomTileSize = 50;
    public int mapSize = 13;
    public int difficulty = 0; //0-4
    public int layersKnown = 2;
    public int layer = 0;
    public int currentRoomX = 0;
    public int currentRoomY = 0;
    public GameObject blankRoom; //100
    public GameObject D0RT; //1-19
    public GameObject D1RT; //20-39
    public GameObject D2RT; //40-69
    public GameObject D3RT; //70-89
    public GameObject D4RT; //90-99
    public GameObject startingRoom; //0
    public GameObject completedRoom; //101
    public GameObject bossRoom; //102
    public StartimgRoom StartimgRoom; //0
    public CompletedRoom CompletedRoom; //101
    public BlankRoom blankScript; //100
    public D0RoomTile D0RoomTile; //1-19
    public D1RoomTile D1RoomTile; //20-39
    public D2RoomTile D2RoomTile; //40-69
    public D3RoomTile D3RoomTile; //70-89
    public D4RoomTile D4RoomTile; //90-99

    int[,] map;
    GameObject[,] mapObjects;

    void Start()
    {
        map = new int[mapSize, mapSize];
        mapObjects = new GameObject[mapSize, mapSize];
        createMap();
    }

    void createMap()
    {
        for (int i = 0; i < mapSize; i++)
        {
            layer = i;
            for (int j = 0; j < mapSize; j++)
            {
                if (j > layer)
                {
                    layer++;
                }
                if (i == currentRoomX && j == currentRoomY)
                {
                    map[i, j] = 0;
                }
                else if (i<= 13)
                {
                    map[i, j] = assaignRoom(layer);
                }
                else
                {
                    map[i, j] = 100;
                }
            }
            
        }

        roomSetter();
    }

    int assaignRoom(int difNum)
    {
        int roomNum = 0;
        switch (difNum)
        {
            case 0: roomNum = Random.Range(1, 20); break;
            case 1: roomNum = Random.Range(1, 25); break;
            case 2: roomNum = Random.Range(10, 35); break;
            case 3: roomNum = Random.Range(15, 45); break;
            case 4: roomNum = Random.Range(19, 55); break;
            case 5: roomNum = Random.Range(25, 65); break;
            case 6: roomNum = Random.Range(35, 70); break;
            case 7: roomNum = Random.Range(45, 75); break;
            case 8: roomNum = Random.Range(55, 85); break;
            case 9: roomNum = Random.Range(70, 92); break;
            case 10: roomNum = Random.Range(70, 96); break;
            case 11: roomNum = Random.Range(80, 95); break;
            case 12: roomNum = Random.Range(90, 99); break;
            default: break;
        }
        return roomNum;
    }

    void roomSetter()
    {

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                switch (map[i, j])
                {
                    case 0: mapObjects[i, j] = setStartRoom(i, j); break;
                    case < 20: mapObjects[i, j] = level0(i ,j); break;
                    case < 40: mapObjects[i, j] = level1(i, j); break;
                    case < 60: mapObjects[i, j] = level2(i, j); break;
                    case < 80: mapObjects[i, j] = level3(i, j); break;
                    case < 100: mapObjects[i, j] = level4(i, j); break;
                    case 100: mapObjects[i, j] = blankTile(i, j); break;
                    default: break;
                }

            }
        }

    }


    GameObject level0(int x, int y)
    {
        GameObject room = Instantiate(D0RT, new Vector3(x * roomTileSize, y * roomTileSize, -1), transform.rotation);
        return room;
    }
    GameObject level1(int x, int y)
    {
        GameObject room = Instantiate(D1RT, new Vector3(x * roomTileSize, y * roomTileSize, -1), transform.rotation);
        return room;
    }
    GameObject level2(int x, int y)
    {
        GameObject room = Instantiate(D2RT, new Vector3(x * roomTileSize, y * roomTileSize, -1), transform.rotation);
        return room;
    }
    GameObject level3(int x, int y)
    {
        GameObject room = Instantiate(D3RT, new Vector3(x * roomTileSize, y * roomTileSize, -1), transform.rotation);
        return room;
    }
    GameObject level4(int x, int y)
    {
        GameObject room = Instantiate(D4RT, new Vector3(x * roomTileSize, y * roomTileSize, -1), transform.rotation);
        return room;
    }
    GameObject blankTile(int x, int y)
    {
        GameObject room = Instantiate(blankRoom, new Vector3(x * roomTileSize, y * roomTileSize, -1), transform.rotation);
        return room;
    }
    GameObject completeTile(int x, int y)
    {
        GameObject room = Instantiate(completedRoom, new Vector3(x * roomTileSize, y * roomTileSize, -1), transform.rotation);
        return room;
    }
    GameObject setStartRoom(int x, int y)
    {
        GameObject room = Instantiate(startingRoom, new Vector3(x * roomTileSize, y * roomTileSize, -1), transform.rotation);
        return room;
    }

}
