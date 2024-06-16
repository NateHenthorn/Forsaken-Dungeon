using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D4RoomTile : RoomTile
{
    protected override void Start()
    {
        base.Start();
        roomDifficulty = 4;
    }

    // Update is called once per frame
    protected override void Updete()
    {
        base.Updete();
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
