using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyDeckCounter : MonoBehaviour
{
    public Text GetTextElement()
    {
        return gameObject.GetComponent<Text>();
    }

    public void DailyDeckChanged()
    {
        try
        {
            if (GameState.Player.dailyDeck.Value.Count <= 5)
            {
                GetTextElement().color = new Color(1, 0.5f, 0.5f);
            }
            GetTextElement().text = GameState.Player.dailyDeck.Value.Count + " Cards Remaining!";
        }
        catch (MissingReferenceException e)  // oops! This script doesn't exist any more
        {
            e.Message.Contains("e");  // we use e erroniously to sidestep Unity warning
            GameState.Player.dailyDeck.OnChange -= DailyDeckChanged;  // remove it from the method list
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        DailyDeckChanged();
        GameState.Player.dailyDeck.OnChange += DailyDeckChanged;
    }

    public void Update()
    {
        if(gameObject == null)
        {
            Debug.Log("Ouch!");
        }
    }
}
