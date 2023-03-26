using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ColorOnDeckChange : MonoBehaviour
{
    private Text _text;

    public void DeckChange()
    {
        try
        {
            if (GameState.Player.fullDeck.Value.Count < 10)
            {
                _text.color = new Color(1, 0, 0);
            }
            else
            {
                _text.color = new Color(0, 0, 0);
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.fullDeck.OnChange -= DeckChange;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.fullDeck.OnChange -= DeckChange;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        _text = this.gameObject.GetComponent<Text>();
        if (_text == null)
        {
            Debug.LogError("Could not find Text component");
        }
        DeckChange();
        GameState.Player.fullDeck.OnChange += DeckChange;
    }

}
