using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonHandler : MonoBehaviour
{
    // Method to be called when the button is pressed
    public void StartGame()
    {
        // Change the scene to "Room"
        SceneManager.LoadScene("Room");
    }
}
