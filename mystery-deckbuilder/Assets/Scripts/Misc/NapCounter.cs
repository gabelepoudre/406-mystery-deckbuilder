using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NapCounter : MonoBehaviour
{

    public Text GetTextElement()
    {
        return gameObject.GetComponent<Text>();
    }

    public void NapsChanged()
    {
        try
        {
            GetTextElement().text = "Take a nap to refresh your deck! (" + GameState.Player.napsRemainingToday.Value + " left today)";
        }
        catch (MissingReferenceException e)  // oops! This script doesn't exist any more
        {
            e.Message.Contains("e");  // we use e erroniously to sidestep Unity warning
            GameState.Player.dailyDeck.OnChange -= NapsChanged;  // remove it from the method list
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        NapsChanged();
        GameState.Player.dailyDeck.OnChange += NapsChanged;
    }

    public void Update()
    {
        if (gameObject == null)
        {
            Debug.Log("Ouch!");
        }
    }
}
