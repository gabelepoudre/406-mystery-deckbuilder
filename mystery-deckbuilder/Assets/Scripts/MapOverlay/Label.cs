using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Label : MonoBehaviour
{
    public string locationAsString;

    void Start()
    {
        ShowOrHide();
        GameState.Player.locationsViewable.OnChange += ShowOrHide;
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
        catch(MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.locationsViewable.OnChange -= ShowOrHide;
        }
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
        Debug.Log("update player location state to " + GameState.Player.location.Value.ToString());

        SceneManager.LoadScene(locationAsString);
    }

    public void Display()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


    
}
