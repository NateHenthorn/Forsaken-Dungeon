using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCSizeAdjustment : MonoBehaviour
{
    // Start is called before the first frame update
    public float prevWidth = Screen.width;
    public float prevHeight = Screen.height;
    public float baseSize = 5f;
    public float baseResolutionWidth = 1920f;
    void Start()
    {
        AdjustCameraSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width != prevWidth || Screen.height != prevHeight)
        {

            AdjustCameraSize();
        }
    }

    void AdjustCameraSize()
    {
        float percentChange = 0.0f;
        float wChange = 0;
        float hChange = 0;
        if (Screen.width!= prevWidth)
        {
            wChange = prevWidth - Screen.width;
            percentChange += prevWidth / wChange;
        }
        if (Screen.height != prevHeight)
        {
            wChange = prevHeight - Screen.height;
            //percentChange += prevHeight / Screen.height;
        }
        baseResolutionWidth = 1920f; // Set this to your design resolution width
        baseResolutionWidth = 1080;
        baseSize = 270f; // Default Orthographic Size at base resolution

        float screenRatio = (float)Screen.width / baseResolutionWidth ;
        Camera.main.orthographicSize = baseSize * percentChange;// screenRatio + 55;
        prevWidth = Screen.width;
        prevHeight = Screen.height;
    }
}
