using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public static MapManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }
    }

    public MapCreator creator;
    public bool tileSelected = false;
    public RoomTile selectedTile;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void selectTile(RoomTile tile)
    {
        if (tileSelected)
        {
            selectedTile.Unhighlight();
        }
        tile.Highlight();
        selectedTile = tile;
        tileSelected = true;
        GameLogs.Instance.currentRoomX = tile.xCord;
        GameLogs.Instance.currentRoomY = tile.yCord;
    }

    public void loadMap()
    {
        creator.loadMap();
    }
}