using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D0RoomTile : RoomTile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        roomDifficulty = 0;
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
