using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class continueButtonHandler : MonoBehaviour
{
    public SceneController controller;
    // Start is called before the first frame update
    public void continueOn(){
        controller.GoToMapScene();
    }
}
