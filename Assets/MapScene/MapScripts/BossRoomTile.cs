using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTile : RoomTile
{
    protected override void Start()
    {
        base.Start();
        roomDifficulty = 5;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckForNeighbors();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    public override void selectTile()
    {
        base.selectTile();
    }

    public override void Highlight()
    {
        base.Highlight();
    }

    public override void Unhighlight()
    {
        base.Unhighlight();
    }
    public override void CheckForNeighbors()
    {
        base.CheckForNeighbors();
    }
}
