using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikeStateListener : MonoBehaviour
{
    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've won an encounter with Nibbles for the day
        
        try 
        {
            GameState.NPCs.Mike.encountersCompleted.OnChange += OnEncounterComplete;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Mike.encountersCompleted.OnChange -= OnEncounterComplete;
        }
    }

    private void OnEncounterComplete()
    {
       
        //if you've completed the first encounter, then we want to initiate the next dialogue tree depending on whether you won or lost
        
        if (GameState.NPCs.Mike.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        if (GameState.NPCs.Mike.encountersCompleted.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Intro";
        }
    }
}
