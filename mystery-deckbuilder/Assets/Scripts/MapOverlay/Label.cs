using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Label : MonoBehaviour
{
    public bool LabelDisplay = false;

    public void MoveToScene(string scene)
    {
        Debug.Log("Move to " + scene);
        SceneManager.LoadScene(scene);
    }

    public void display()
    {
        LabelDisplay = true;
    }

    public void hide()
    {
        LabelDisplay = false;
    }


    
}
