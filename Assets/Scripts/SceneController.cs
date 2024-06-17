using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure MapCreator is not destroyed when loading a new scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public SceneLoadTracker SceneLoadTracker;
    public bool sceneLoaded = false;
    public int numSceneLoaded = 0;
    public MapCreator mapCreator;
    public MapManager mapManager;
    public GameManager gameManager;
    public int roomDif = 0;
    public void Start()
    {
        SceneLoadTracker = GameObject.FindGameObjectWithTag("SceneLoadTracker").GetComponent<SceneLoadTracker>();
    }
    public void Update()
    {
        if (SceneLoadTracker.sceneJustLoaded)
        {
            sceneLoaded = true;
            numSceneLoaded++;
        }
    }

    public void reloadMap()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        mapManager.loadMap();
    }
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void GoToMapScene()
    {
        if (MapCreator.Instance != null)
        {
            MapCreator.Instance.SaveMapData(); // Save map data before leaving the scene

        }
        MapCreator.Instance.isCurrentScene = true;
       SceneManager.LoadScene("Map", LoadSceneMode.Single); // Replace with your map scene name
        MapCreator.Instance.loadMap();

    }

    public void GoToShopScene()
    {
        if (MapCreator.Instance != null)
        {
            MapCreator.Instance.SaveMapData(); // Save map data before leaving the scene
        }
        SceneManager.LoadScene("PrestigeShop", LoadSceneMode.Single); // Replace with your shop scene name
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void GoToRoomScene()
    {
        // if (MapCreator.Instance != null)
        // {
        MapCreator.Instance.isCurrentScene = false;
            MapCreator.Instance.DestroyMap();

       // }
        SceneManager.LoadScene("Room", LoadSceneMode.Single);
        gameManager = FindObjectOfType<GameManager>();
       //GameManager.Instance.difLevel = roomDif;
    }
}