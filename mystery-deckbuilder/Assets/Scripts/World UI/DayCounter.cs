using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DayCounter : MonoBehaviour
{
    public Text GetTextElement()
    {
        return gameObject.GetComponent<Text>();
    }

    public void DayChanged()
    {
        try
        {
            GetTextElement().text = "Day " + GameState.Meta.currentDay.Value;
        }
        catch (MissingReferenceException e)  // oops! This script doesn't exist any more
        {
            e.Message.Contains("e");  // we use e erroniously to sidestep Unity warning
            GameState.Player.dailyDeck.OnChange -= DayChanged;  // remove it from the method list
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.dailyDeck.OnChange -= DayChanged;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        DayChanged();
        GameState.Player.dailyDeck.OnChange += DayChanged;
    }

    public void Update()
    {
        if (gameObject == null)
        {
            Debug.Log("Ouch!");
        }
    }
}
