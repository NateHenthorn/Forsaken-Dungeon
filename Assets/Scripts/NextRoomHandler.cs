using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoomHandler : MonoBehaviour
{
    // Start is called before the first frame update
    MapManager mapManager;
    SceneController controller;
    public int roomDifficulty = 0;
    public void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        controller = FindObjectOfType<SceneController>();
    }
    public void nextRoom()
    {
        if (mapManager.tileSelected)
        {
            controller.GoToRoomScene();
        }
        else
        {
            print("No room selected");
        }
    }
}