using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    public Camera mainCamera; // Reference to the camera
    public float referenceHeight = 800; // The reference height resolution (e.g., 1080p)
    public GameObject obj; 
    void Start()
    {
        // Get the current screen height and calculate the scaling factor
        float screenHeight = Screen.height;
        float scaleFactor = screenHeight / referenceHeight;

        // Adjust the local scale of the sprite based on the scale factor
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        if (obj != null)
        {
            obj.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);

        }
    }
}
