using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D3RoomTile : RoomTile
{
    protected override void Start()
    {
        base.Start();
        roomDifficulty = 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
}