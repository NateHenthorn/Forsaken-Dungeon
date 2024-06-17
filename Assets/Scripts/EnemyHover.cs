using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHover : MonoBehaviour
{
    public string enemyName; // Set this in the inspector
    public GameObject nameTextPrefab; // Reference to the Text prefab
    public GameObject nameTextInstance; // Instance of the Text prefab
    public TextMeshProUGUI nameText; // Reference to the Text component
    public Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        nameTextInstance = Instantiate(nameTextPrefab);
        nameTextInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        nameText = nameTextInstance.GetComponent<TextMeshProUGUI>();
        nameText.text = "";
    }

    private void Update()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(this.transform.position - Vector3.up); // Adjust the height as needed
        //nameTextInstance.transform.position = screenPosition;
    }

    private void OnMouseEnter()
    {
        // Show the enemy name when the mouse enters
        nameText.text = enemyName;
    }

    private void OnMouseExit()
    {
        // Hide the enemy name when the mouse exits
        nameText.text = "";
    }

    private void OnDestroy()
    {
        // Clean up the name text instance when the enemy is destroyed
        Destroy(nameTextInstance);
    }
}