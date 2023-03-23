using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InactiveOnLowCards : MonoBehaviour
{
    public void DeckChange()
    {
        try
        {
            if(GameState.Player.fullDeck.Value.Count < 10)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
            }
        }
        catch(MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.fullDeck.OnChange -= DeckChange;
        }
        catch(NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.fullDeck.OnChange -= DeckChange;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        DeckChange();
        GameState.Player.fullDeck.OnChange += DeckChange;
    }

}
