using UnityEngine;

public class EnemyHover : MonoBehaviour
{
    private EnemyHUDController hudController;
    private Enemies enemy;

    private void Start()
    {
        hudController = FindObjectOfType<EnemyHUDController>();
        enemy = GetComponent<Enemies>(); // Assuming each enemy has an Enemy component with its stats
    }

    private void OnMouseEnter()
    {
        hudController.ShowHUD(enemy);
    }

    private void OnMouseExit()
    {
        hudController.HideHUD();
    }
}