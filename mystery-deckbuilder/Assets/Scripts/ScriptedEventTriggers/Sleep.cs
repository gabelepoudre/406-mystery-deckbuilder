using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{
    public void NextDay()
    {
        GameState.Meta.currentDay.Value += 1;
        SceneManager.LoadScene("DeckBuilding");
    }

    public void Awake()
    {
        if (GameState.Meta.justSlept.Value)
        {
            gameObject.SetActive(false);
        }
    }
}
