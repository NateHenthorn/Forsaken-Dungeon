using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class continueButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void continueOn(){
        SceneManager.LoadScene("Map");
    }
}
