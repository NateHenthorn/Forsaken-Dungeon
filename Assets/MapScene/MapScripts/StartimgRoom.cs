using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartimgRoom : RoomTile
{
    protected override void Start()
    {
        base.Start();
        isCompletedRoom = true;
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
