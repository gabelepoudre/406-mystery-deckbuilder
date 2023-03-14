using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenCard : MonoBehaviour
{
    [SerializeField] private int _card;

    // Start is called before the first frame update
    void Start()
    {
        //to disallow player from picking it up again
        if (GameState.Player.fullDeck.Value.Contains(_card))
        {
            gameObject.SetActive(false);
        }
    }

    public void PickUpCard()
    {
        //add three copies of the card
        GameState.Player.fullDeck.Value.Add(_card);
        GameState.Player.fullDeck.Value.Add(_card);
        GameState.Player.fullDeck.Value.Add(_card);
        Debug.Log("Picked up card " + _card.ToString());
        gameObject.SetActive(false);
    }


}
