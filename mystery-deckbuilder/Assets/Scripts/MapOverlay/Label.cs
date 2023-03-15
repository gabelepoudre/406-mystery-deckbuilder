using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Label : MonoBehaviour
{
    public string locationAsString;

    void Start()
    {
        ShowOrHide();
        GameState.Player.locationsViewable.OnChange += ShowOrHide;
    }


    public void MoveToScene()
    {

        if (DialogueManager.Instance.DialogueActive)
        {
            Debug.Log("cannot switch locations when dialogue in session");
            return;
        }

        Debug.Log("Move to " + locationAsString);

        //update state
        GameState.Player.location.Value = GameState.Player.Locations.Parse<GameState.Player.Locations>(locationAsString);
        //update location related states
        UpdateState(locationAsString);
        Debug.Log("update player location state to " + GameState.Player.location.Value.ToString());

        SceneManager.LoadScene(locationAsString);
    }

    public void ShowOrHide()
    {
        try
        {
            GameState.Player.Locations loc = GameState.Player.Locations.Parse<GameState.Player.Locations>(locationAsString);
            if (GameState.Player.locationsViewable.Value[loc])
            {
                Display();
            }
            else
            {
                Hide();
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.locationsViewable.OnChange -= ShowOrHide;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.locationsViewable.OnChange -= ShowOrHide;
        }
    }
    public void MoveToScene(string scene)
    {
        
        if (DialogueManager.Instance.DialogueActive)
        {
            Debug.Log("cannot switch locations when dialogue in session");
            return;
        }
        
        Debug.Log("Move to " + scene);

        

        SceneManager.LoadScene(scene);
    }

    public void Display()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
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
