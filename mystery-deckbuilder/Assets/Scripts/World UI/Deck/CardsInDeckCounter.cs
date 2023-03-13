using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsInDeckCounter : MonoBehaviour
{
    public Text GetTextElement()
    {
        return gameObject.GetComponent<Text>();
    }

    public void FullDeckChanged()
    {
        try
        {
            GetTextElement().text = GameState.Player.fullDeck.Value.Count + "/20";
        }
        catch (MissingReferenceException e)  // oops! This script doesn't exist any more
        {
            e.Message.Contains("e");  // we use e erroniously to sidestep Unity warning
            GameState.Player.dailyDeck.OnChange -= FullDeckChanged;  // remove it from the method list
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        FullDeckChanged();
        GameState.Player.dailyDeck.OnChange += FullDeckChanged;
    }

    public void Update()
    {
        if (gameObject == null)
        {
            Debug.Log("Ouch!");
        }
    }
}
