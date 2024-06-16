using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Button nextButton;

    public void spawnPlayer(int x, int y)
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(x, y, -12), Quaternion.identity);

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.Next = nextButton;
        }
        else
        {
        }
    }
}