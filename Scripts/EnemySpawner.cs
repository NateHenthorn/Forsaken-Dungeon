using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemySpawner : MonoBehaviour
{
    public GameObject commonEnemyPrefab;
    public GameObject rareEnemyPrefab;
    public GameObject legendaryEnemyPrefab;
    public GameObject forsakenEnemyPrefab;
    public Button nextButton;
    public TurnManager turnManager;
    // Start is called before the first frame update
    public void spawnEnemy(int x, int y, int enemyNum)
    {
        if (enemyNum == 0)
        {
            GameObject enemy = Instantiate(commonEnemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
            enemy.transform.position = new Vector3(x, y, 0);

            Enemies playerScript = enemy.GetComponent<commonEnemy>();
            turnManager.addToQueue(enemy, 1);
            if (playerScript != null)
            {
                playerScript.Next = nextButton;
            }
            else
            {
            }
        }
        if (enemyNum == 1)
        {
            GameObject enemy = Instantiate(rareEnemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
            enemy.transform.position = new Vector3(x, y, 0);

            Enemies enemyScript = enemy.GetComponent<rareEnemy>();
            turnManager.addToQueue(enemy, 2);
            if (enemyScript != null)
            {
                enemyScript.Next = nextButton;
            }
            else
            {
            }
        }
        if (enemyNum == 2)
        {
            GameObject enemy = Instantiate(legendaryEnemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
            enemy.transform.position = new Vector3(x, y, 0);

            Enemies enemyScript = enemy.GetComponent<legendaryEnemy>();
            turnManager.addToQueue(enemy, 3);
            if (enemyScript != null)
            {
                enemyScript.Next = nextButton;
            }
            else
            {
            }
        }
        if (enemyNum == 3)
        {
            GameObject enemy = Instantiate(forsakenEnemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
            enemy.transform.position = new Vector3(x, y, 0);

            Enemies enemyScript = enemy.GetComponent<forsakenEnemy>();
            turnManager.addToQueue(enemy, 4);
            if (enemyScript != null)
            {
                enemyScript.Next = nextButton;
            }
            else
            {
            }
        }

    }
}