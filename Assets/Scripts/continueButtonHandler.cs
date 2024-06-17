using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class continueButtonHandler : MonoBehaviour
{
    public SceneController controller;
    // Start is called before the first frame update
    private void Update()
    {
        if (controller == null)
        {
            controller = GetComponent<SceneController>();
        }
    }
    public void continueOn()
    {
        controller.GoToMapScene();
    }
}