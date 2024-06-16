using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

public class MapCreator : MonoBehaviour
{
    public static MapCreator Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure MapCreator is not destroyed when loading a new scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int roomTileSize = 50;
    public int mapSize = 13;
    public int difficulty = 0; // 0-4
    public int prevLayersKnown = -1;
    public int layersKnown = -1;
    public int layer = 0;
    public int currentRoomX = 0;
    public int currentRoomY = 0;
    public GameObject blankRoom; // 100
    public GameObject D0RT; // 1-19
    public GameObject D1RT; // 20-39
    public GameObject D2RT; // 40-69
    public GameObject D3RT; // 70-89
    public GameObject D4RT; // 90-99
    public GameObject startingRoom; // 0
    public GameObject completedRoom; // 101
    public GameObject bossRoom; // 102
    public int numTimesLoaded = 0;
    public bool bossRoomMade = false;
    int[,] map;
    GameObject[,] mapObjects;

    void Start()
    {
        currentRoomX = GameLogs.Instance.currentRoomX;
        currentRoomY = GameLogs.Instance.currentRoomY;
        map = new int[mapSize, mapSize];
        mapObjects = new GameObject[mapSize, mapSize];
        createMap();
    }
    private void Update()
    {
        currentRoomX = GameLogs.Instance.currentRoomX;
        currentRoomY = GameLogs.Instance.currentRoomY;
        if (prevLayersKnown != layersKnown)
        {
            roomSetter();
            prevLayersKnown = layersKnown;

        }
    }

    void OnApplicationQuit()
    {
        // SaveMapData();
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
                else if (i <= layersKnown && j <= layersKnown)
                {
                    map[i, j] = assignRoom(layer, true);
                }
                else
                {
                    map[i, j] = assignRoom(layer, false);
                }
            }
        }

        roomSetter();
    }

    int assignRoom(int difNum, bool visible)
    {
        int roomNum = 0;
        if (visible)
        {
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
        }
        else
        {
            switch (difNum)
            {
                case 0: roomNum = Random.Range(1, 20) + 100; break;
                case 1: roomNum = Random.Range(1, 25) + 100; break;
                case 2: roomNum = Random.Range(10, 35) + 100; break;
                case 3: roomNum = Random.Range(15, 45) + 100; break;
                case 4: roomNum = Random.Range(19, 55) + 100; break;
                case 5: roomNum = Random.Range(25, 65) + 100; break;
                case 6: roomNum = Random.Range(35, 70) + 100; break;
                case 7: roomNum = Random.Range(45, 75) + 100; break;
                case 8: roomNum = Random.Range(55, 85) + 100; break;
                case 9: roomNum = Random.Range(70, 92) + 100; break;
                case 10: roomNum = Random.Range(70, 96) + 100; break;
                case 11: roomNum = Random.Range(80, 95) + 100; break;
                case 12: roomNum = Random.Range(90, 99) + 100; break;
                default: break;
            }

        }
        return roomNum;
    }

    public void roomSetter()
    {
        bossRoomMade = false;
        for (int i = 0; i < mapSize; i++)
        {
            layer = i;
            for (int j = 0; j < mapSize; j++)
            {
                if (layer == 12 && !bossRoomMade && map[i, j] == 97)
                {
                    bossRoomMade = true;
                }
                else if (map[i, j] == 97)
                {
                    map[i, j] = 98;
                }
                if (j > layer)
                {
                    layer++;
                }
                if (mapObjects[i, j] != null)
                {
                    Destroy(mapObjects[i, j]); // Ensure to destroy previous objects
                }
                if (i == currentRoomX && j == currentRoomY)
                {
                    map[i, j] = 0;
                }
                else if (layer >= prevLayersKnown && layer < layersKnown && layersKnown != prevLayersKnown && map[i, j] - 100 >= 0)
                {
                    map[i, j] = map[i, j] - 100;
                }
                switch (map[i, j])
                {
                    case 0: mapObjects[i, j] = setStartRoom(i, j); break;
                    case < 20: mapObjects[i, j] = level0(i, j); break;
                    case < 40: mapObjects[i, j] = level1(i, j); break;
                    case < 60: mapObjects[i, j] = level2(i, j); break;
                    case < 80: mapObjects[i, j] = level3(i, j); break;
                    case 97: mapObjects[i, j] = makeBossRoom(i, j); break;
                    case < 100: mapObjects[i, j] = level4(i, j); break;
                    case 100: mapObjects[i, j] = blankTile(i, j); break;
                    //  case < 120: mapObjects[i, j] = level0(i, j); break;
                    // case < 140: mapObjects[i, j] = level1(i, j); break;
                    // case < 160: mapObjects[i, j] = level2(i, j); break;
                    // case < 180: mapObjects[i, j] = level3(i, j); break;
                    case < 200: mapObjects[i, j] = blankTile(i, j); break;
                    default: break;
                }
                mapObjects[i, j].GetComponent<RoomTile>().xCord = i;
                mapObjects[i, j].GetComponent<RoomTile>().yCord = j;
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
    GameObject makeBossRoom(int x, int y)
    {
        GameObject room = Instantiate(bossRoom, new Vector3(x * roomTileSize, y * roomTileSize, -1), transform.rotation);
        return room;
    }

    // Save the current map state
    public void SaveMapData()
    {
        MapData mapData = new MapData();
        mapData.tiles = new List<RoomTileData>();

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                RoomTileData tileData = new RoomTileData();
                tileData.x = i;
                tileData.y = j;
                tileData.type = map[i, j];
                mapData.tiles.Add(tileData);
            }
        }

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/mapdata.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, mapData);
        stream.Close();
        Debug.Log("Map data saved to " + path);
    }

    // Load the saved map state
    public void LoadMapData()
    {
        string path = Application.persistentDataPath + "/mapdata.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MapData mapData = formatter.Deserialize(stream) as MapData;
            stream.Close();
            Debug.Log("Map data loaded from " + path);

            ClearMap();
            foreach (var tileData in mapData.tiles)
            {
                map[tileData.x, tileData.y] = tileData.type;
            }

            roomSetter();
        }
        else
        {
            Debug.LogError("No saved map data found, creating new map.");
            createMap();
        }
    }

    public void ClearMap()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (mapObjects[i, j] != null)
                {
                    Destroy(mapObjects[i, j]);
                }
            }
        }
    }

    [System.Serializable]
    public class MapData
    {
        public List<RoomTileData> tiles;
    }

    [System.Serializable]
    public class RoomTileData
    {
        public int x;
        public int y;
        public int type;
    }

    public void loadMap()
    {
        roomSetter();
    }
}