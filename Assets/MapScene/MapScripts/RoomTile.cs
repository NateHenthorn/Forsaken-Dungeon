using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTile : MonoBehaviour
{
    public GameObject Room;
    private Camera mainCamera;
    private MapManager mapManager;
    public bool blank = false;
    private Color originalColor;
    private Renderer tileRenderer;
    public bool isHighlighted = false;
    public NextRoomHandler roomHandler;
    public int roomDifficulty = 0;
    public SceneController sceneController;
    public int xCord = -1;
    public int yCord = -1;
    public int mapNum = -1;
    public bool adjuacentToCompletedRoom = false;
    public int detectionRadius = 75;
    public bool isCompletedRoom = false;
    protected virtual void Start()
    {
        tileRenderer = this.GetComponent<MeshRenderer>();
        originalColor = tileRenderer.material.color;
        mainCamera = Camera.main;
        mapManager = FindObjectOfType<MapManager>();
        sceneController = FindObjectOfType<SceneController>();

    }
    protected virtual void Update()
    {
        CheckForNeighbors();
    }

    protected virtual void OnMouseDown()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //  Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            GameObject clickedObject = hit.collider.gameObject;
            if (clickedObject.CompareTag("RoomTile"))
            {
                RoomTile roomTile = clickedObject.GetComponent<RoomTile>();
                if (roomTile != null)
                {
                    roomTile.selectTile();

                }
            }
        }


    }

    public virtual void selectTile()
    {
        if (!blank && adjuacentToCompletedRoom)
        {
            mapManager.selectTile(this);
            Highlight();
        }
    }

    public virtual void Highlight()
    {
        if (tileRenderer != null)
        {
            if (!isHighlighted)
            {
                tileRenderer.material.color = Color.yellow; // Change to desired highlight color
                isHighlighted = true;
                sceneController.roomDif = roomDifficulty;
            }
        }
    }

    public virtual void Unhighlight()
    {
        if (isHighlighted)
        {
            tileRenderer.material.color = originalColor;
            isHighlighted = false;
        }
    }
    public virtual void CheckForNeighbors()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("RoomTile"))
            {
                RoomTile tile = hitCollider.GetComponent<RoomTile>();
                if (tile != null)
                {
                    if (isCompletedRoom)
                    {
                        tile.adjuacentToCompletedRoom = true;
                        if (tile.blank && SceneLoadTracker.Instance.layerIncreased == false)
                        {
                            SceneLoadTracker.Instance.layerIncreased = true;
                            MapCreator.Instance.layersKnown++;
                        }
                    }

                }
            }
        }
    }
}