using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isSolid = false;
    public bool isPlayerOccupied = false;
    public bool isEnemyOccupied = false;
    public float tileDistance = 26f;
    private float detectionRadius = 20f; // Slightly larger than tile distance
    private Color originalColor;
    private Renderer tileRenderer;
    private bool isHighlighted = false;
    Player player;

    public virtual void Start()
    {
        tileRenderer = this.GetComponent<MeshRenderer>();
        originalColor = tileRenderer.material.color;
        player = FindObjectOfType<Player>();
    }

    public virtual void Update()
    {
        if (player.actionPointsUsed >= player.actionPoints || !player.turnStarted)
        {
            UnhighlightNeighbors();
        }
        if(TurnManager.Instance.state == BattleState.PLAYERTURN)
        {
            player.GetTileAtPosition(player.transform.position).CheckForNeighbors();
        }
    }

    public virtual void CheckForNeighbors()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tile"))
            {
                Tile tile = hitCollider.GetComponent<Tile>();
                if (tile != null && tile != this && tile.isSolid == false )
                {
                    if (tile.isEnemyOccupied)
                    {
                        tile.HighlightEnemy();
                    }
                    else
                    {
                        // Highlight neighboring tiles
                        tile.Highlight();
                    }
                }
            }
        }
    }

    public virtual void CheckForNeighborsEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
            if (hitCollider.CompareTag("Tile"))
            {
                {
                    Tile tile = hitCollider.GetComponent<Tile>();
                }
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
        else
        {
            Debug.LogError("Renderer component is null!");
        }
    }

    public virtual void HighlightEnemy()
    {
        if (tileRenderer != null)
        {
            if (!isHighlighted)
            {
                tileRenderer.material.color = Color.blue; // Change to desired highlight color
                isHighlighted = true;
            }
        }
        else
        {
            Debug.LogError("Renderer component is null!");
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

    public virtual void OccupyTileWithPlayer()
    {
        isPlayerOccupied = true;
        isEnemyOccupied = false;
        CheckForNeighbors();
    }

    public virtual void OccupyTileWithEnemy()
    {
        isEnemyOccupied = true;
        isPlayerOccupied = false;
    }

    public virtual void VacateTile()
    {
        isPlayerOccupied = false;
        isEnemyOccupied = false;
        UnhighlightNeighbors();
    }
    public virtual void eVacateTile()
    {
        isPlayerOccupied = false;
        isEnemyOccupied = false;
    }

    public bool IsOccupiedByPlayer()
    {
        return isPlayerOccupied;
    }

    public bool IsOccupiedByEnemy()
    {
        return isEnemyOccupied;
    }

    private void OnMouseDown()
    {
        if (isHighlighted)
        {
            // Move the player to this tile if it is highlighted
            Player player = FindObjectOfType<Player>();
            if (player.actionPointsUsed < player.actionPoints)
            {
                if (player != null && this.isEnemyOccupied == false)
                {
                    Tile currentTile = player.GetCurrentTile();
                    if (currentTile != null)
                    {
                        currentTile.VacateTile();
                    }
                    player.MoveToTile(this);
                    player.actionPointsUsed++;
                    // UnhighlightNeighbors();
                }
            }
        }
    }

    private void UnhighlightNeighbors()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tile"))
            {
                Tile tile = hitCollider.GetComponent<Tile>();
                if (tile != null && tile != this)
                {
                    tile.Unhighlight();
                }
            }
        }
    }
}