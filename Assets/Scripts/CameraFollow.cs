using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Transform player;
    public Vector3 offset;
    public float targetFOV = 30f;
    public float zoomSpeed = 2f;
    private Camera cam;

    private float baseResolutionHeight = 1080f;
    private float baseSize = 5f;  // Standard Unity orthographic camera size
    private float prevWidth;
    private float prevHeight;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Camera component not found on this GameObject.");
            return;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            if (offset == Vector3.zero)
            {
                offset = transform.position - player.position;
            }
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player has the correct tag.");
        }

        prevWidth = Screen.width;
        prevHeight = Screen.height;
        AdjustCameraSize();
    }

    void LateUpdate()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                if (offset == Vector3.zero)
                {
                    offset = transform.position - player.position;
                }
            }
        }

        Vector3 desiredPosition = new Vector3(185, player.position.y + offset.y - 100, -25);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);

        if (Screen.width != prevWidth || Screen.height != prevHeight)
        {
            AdjustCameraSize();
        }
    }

    void AdjustCameraSize()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float referenceRatio = 1920f / 1080f; // Your reference resolution aspect ratio
        float scaleFactor = screenRatio / referenceRatio; // How different the current aspect ratio is

        float newSize = baseSize / scaleFactor; // Adjust based on aspect ratio difference

        cam.orthographicSize = Mathf.Clamp(newSize, 100f, 200f); // Adjust min/max zoom levels

        prevWidth = Screen.width;
        prevHeight = Screen.height;
    }
}