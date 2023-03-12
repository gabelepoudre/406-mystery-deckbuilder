using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_BearStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've won an encounter with Bear for the day
        try 
        {
            GameState.NPCs.Black_Bear.encountersCompleted.OnChange += OnEncounterComplete;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Black_Bear.encountersCompleted.OnChange -= OnEncounterComplete;
        }

    }

    private void OnEncounterComplete()
    {
        //TODO: implement
    }
}
