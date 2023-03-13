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

        //update state
        GameState.Player.location.Value = GameState.Player.Locations.Parse<GameState.Player.Locations>(scene);
        Debug.Log("update player location state to " + GameState.Player.location.Value.ToString());

        SceneManager.LoadScene(scene);
    }

    public void Display()
    {
        LabelDisplay = true;
    }

    public void Hide()
    {
        LabelDisplay = false;
    }


    
}
