using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    public Canvas canvas;
    public Camera camera;

    void Start()
    {
        camera = Camera.main;
        canvas = FindObjectOfType<Canvas>();
        AdjustResolution();
    }

    void Update()
    {
        if (Screen.width != canvas.pixelRect.width || Screen.height != canvas.pixelRect.height)
        {
            AdjustResolution();
        }
    }

    void AdjustResolution()
    {
        CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();
        if (canvasScaler != null)
        {
            canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        }

        if (camera.orthographic)
        {
            camera.orthographicSize = Screen.height / 2.0f;
        }
    }
}