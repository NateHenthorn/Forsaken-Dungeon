using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PArmor : PShopItem
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override string pickPrefix(int value)
    {
        string rValue = base.pickPrefix(value);
        return rValue;
    }
}
