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
    protected virtual void Start()
    {
        tileRenderer = this.GetComponent<MeshRenderer>();
        originalColor = tileRenderer.material.color;
        mainCamera = Camera.main;
        mapManager = FindObjectOfType<MapManager>();
    }
    protected virtual void Updete()
    {
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
        if (!blank)
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
}
