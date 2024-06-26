using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BlankTile : Tile
{
    public override void Start()
    {
        base.Start();
        // Additional initialization for SpecialTile
    }

    public override void Update()
    {
        base.Update();
        // Additional update logic for SpecialTile
    }

    public override void CheckForNeighbors()
    {
        base.CheckForNeighbors();
        // Additional logic for checking neighbors in SpecialTile
    }

    public override void OccupyTileWithPlayer()
    {
        base.OccupyTileWithPlayer();
        // Additional logic when the tile is occupied by a player
    }

    public override void OccupyTileWithEnemy()
    {
        base.OccupyTileWithEnemy();
        // Additional logic when the tile is occupied by an enemy
    }

    public override void VacateTile()
    {
        base.VacateTile();
        // Additional logic when the tile is vacated
    }
}