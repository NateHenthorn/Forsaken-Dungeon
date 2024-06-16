using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatBlock : MonoBehaviour
{
    public TextMeshProUGUI initiative;
    public int initi;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI HP;
    //public TextMeshProUGUI BP;
    //public TextMeshProUGUI AP;
    public TextMeshProUGUI DMG;
    public TextMeshProUGUI SE;
    public TextMeshProUGUI coins;

    //canvas and statblock
    public StatBlock statblock;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public StatBlock()
    {

    }

    public void setInitiative(int init)
    {
        this.initi = init;
        initiative.text = ("" + init);
    }
    public void setPlayerBlock(Player player)
    {
       // player.makeStatBlock(280, 270);
    }

    void makeStatBlock()
    {
        if (statblock != null && canvas != null)
        {
            int x = 250;
            int y = 100;
            // Instantiate as a child of the Canvas
            Instantiate(statblock, new Vector3(x, y, -2), Quaternion.identity, canvas.transform);
        }
        else
        {
            if (statblock == null)
                Debug.LogError("StatBlock prefab is not assigned in the Inspector.");
            if (canvas == null)
                Debug.LogError("Canvas is not found in the scene.");
        }
    }
}