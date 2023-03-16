using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{
    public void NextDay()
    {
        GameState.Meta.currentDay.Value += 1;
        GameState.Player.napsRemainingToday.Value = 1;
        SceneManager.LoadScene("DeckBuilding");

    }

    public void Awake()
    {
        if (GameState.Meta.justSlept.Value || (GameState.Meta.currentDay.Value == 2 && GameState.Meta.currentGameplayPhase.Value == GameState.Meta.GameplayPhases.Tutorial) || GameState.Meta.currentDay.Value == 7)
        {
            gameObject.SetActive(false);
        }
    }
}
