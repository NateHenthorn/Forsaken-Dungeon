using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingWithCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform UIRect = this.GetComponent<RectTransform>();

        // Anchor to the middle-top so it adjusts well on different screen sizes
        UIRect.anchorMin = new Vector2(0.5f, 1f);
        UIRect.anchorMax = new Vector2(0.5f, 1f);

        // Keep the item's position relative to its parent
        UIRect.pivot = new Vector2(0.5f, 1f);
        UIRect.anchoredPosition = new Vector2(0, -100); // Adjust vertical spacing
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
