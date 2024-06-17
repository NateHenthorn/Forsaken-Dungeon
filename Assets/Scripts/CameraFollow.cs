using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Transform player;    // Reference to the player's transform
    public Vector3 offset;      // Offset between the player and the camera
    public float leftScreenOffset = 250f;
    public float targetFOV = 30f;        // Target field of view for zoom
    public float zoomSpeed = 2f;         // Speed of zoom

    private Camera cam;

    void Start()
    {
        cam = this.GetComponent<Camera>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        // If the offset is not set, calculate it from the initial positions
        if (playerObj != null)
        {
            player = playerObj.transform;
            // If the offset is not set, calculate it from the initial positions
            if (offset == Vector3.zero)
            {
                offset = transform.position - player.position;
            }
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player has the correct tag.");
        }
        if (cam == null)
        {
            Debug.LogError("Camera component not found on this GameObject.");
        }

    }

    void LateUpdate()
    {
        leftScreenOffset = 250f;
        cam = this.GetComponent<Camera>();
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            // If the offset is not set, calculate it from the initial positions
            if (playerObj != null)
            {
                player = playerObj.transform;
                // If the offset is not set, calculate it from the initial positions
                if (offset == Vector3.zero)
                {
                    offset = transform.position - player.position;
                }
            }

        }
        Vector3 desiredPosition = new Vector3(185, player.position.y + offset.y - 100, -25);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        // Smoothly zoom in
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
    }
}