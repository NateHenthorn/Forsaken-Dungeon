using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTracker : MonoBehaviour
{

    public static SceneLoadTracker Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool layerIncreased = false;
    public string targetSceneName = "Map";
    public static bool sceneJustLoaded = false;
    public MapCreator creator;
    public int roomDif = 0;
    private void Start()
    {
        layerIncreased = false;
        creator = GameObject.FindGameObjectWithTag("MapCreator").GetComponent<MapCreator>();
        creator.numTimesLoaded++;
        creator.roomSetter();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetSceneName)
        {
            sceneJustLoaded = true;
            StartCoroutine(ResetSceneJustLoadedFlag());
        }
    }

    private IEnumerator ResetSceneJustLoadedFlag()
    {
        yield return new WaitForEndOfFrame(); // Wait for one frame
        sceneJustLoaded = false;
    }
}
