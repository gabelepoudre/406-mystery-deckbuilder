using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Label : MonoBehaviour
{
    public bool LabelDisplay = false;

    public void MoveToScene(string scene)
    {
        
        if (DialogueManager.Instance.DialogueActive)
        {
            Debug.Log("cannot switch locations when dialogue in session");
            return;
        }
        
        Debug.Log("Move to " + scene);

        //update location related states
        UpdateState(scene);

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

    //updates all location-related and gameplay phase states
    private void UpdateState(string scene)
    {
        //update state
        GameState.Player.location.Value = GameState.Player.Locations.Parse<GameState.Player.Locations>(scene);
        Debug.Log("update player location state to " + GameState.Player.location.Value.ToString());

        //NOTE: updating gameplay phase if leaving berry barn during tutorial day 2
        if (SceneManager.GetActiveScene().name == "BerryFarm" &&
        GameState.Meta.currentGameplayPhase.Value == GameState.Meta.GameplayPhases.Tutorial
        && GameState.Meta.currentDay.Value == 2)
        {
            GameState.Meta.currentGameplayPhase.Value = GameState.Meta.GameplayPhases.Phase_1;
            Debug.Log("changed gameplay phase to " + GameState.Meta.currentGameplayPhase.Value.ToString());
        }
    }


    
}
