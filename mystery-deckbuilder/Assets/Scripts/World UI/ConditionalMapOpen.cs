using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionalMapOpen : MonoBehaviour
{
    public GameObject miniMap;

    public void OpenMap()
    {
        UpdateStateAfterBerryFarm();

        // not conditional
        miniMap.SetActive(true);
        GameState.Meta.mapIsOpen.Value = true;
    }

    //the berry farm event is officially over after the player opens the map for the first time during the scene
    private void UpdateStateAfterBerryFarm()
    {
         //NOTE: updating gameplay phase if leaving berry barn during tutorial day 2
        if (SceneManager.GetActiveScene().name == "BerryFarm" &&
        GameState.Meta.currentGameplayPhase.Value == GameState.Meta.GameplayPhases.Tutorial
        && GameState.Meta.currentDay.Value == 2)
        {
            GameState.Meta.currentGameplayPhase.Value = GameState.Meta.GameplayPhases.Phase_1;
            Debug.Log("changed gameplay phase to " + GameState.Meta.currentGameplayPhase.Value.ToString());
        }
    }
}
