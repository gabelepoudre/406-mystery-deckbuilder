using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardArtHolder : MonoBehaviour
{
    public Sprite[] cardArts;

    public Sprite GetArtByCardID(int card_id)
    {
        Debug.Log("Getting card background " + card_id + " at index " + (card_id - 1));
        Sprite art = cardArts[card_id - 1];
        if (art == null)
        {
            Debug.LogWarning("There was no art at index");
        }
        return art;
    }
}
