using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBDeckCardQuantityController : MonoBehaviour
{
    public int CardsContainedInDeck 
    {
        get
        {
            return _cardsContainedInDeck;
        }
        set 
        {
            _cardsContainedInDeck = value;
            UpdateQuantity();
        }
    }
    private int _cardsContainedInDeck = 0;

    public int CardHoldMax
    {
        get
        {
            return _cardHoldMax;
        }
        set
        {
            _cardHoldMax = value;
            UpdateQuantity();
        }
    }
    private int _cardHoldMax = 3;

    // Start is called before the first frame update

    private Text GetLinkedText()
    {
        Text t = gameObject.GetComponent<Text>();
        if (t == null)
        {
            Debug.LogError("CardQuantityController wasn't linked to an object with Text");
        }
        return t;
    }

    private void UpdateQuantity()
    {
        GetLinkedText().text = CardsContainedInDeck + "/" + CardHoldMax;
    }

    void Start()
    {
        UpdateQuantity();
    }

}
