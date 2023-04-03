using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshDailyDeck : MonoBehaviour
{
    public void RefreshAllCardsOnClick()
    {
        GameState.Player.dailyDeck.Value = new List<int>(GameState.Player.fullDeck.Value.ToArray());
    }
}
