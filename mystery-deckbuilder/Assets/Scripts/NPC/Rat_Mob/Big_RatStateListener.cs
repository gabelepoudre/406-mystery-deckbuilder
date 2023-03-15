using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Big_RatStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've won an encounter with Nibbles for the day
        GameState.NPCs.Big_Rat.encountersCompleted.OnChange += OnEncounterComplete;
        
    }

    private void OnEncounterComplete()
    {
         //if you've completed the first encounter, then we want to initiate the next dialogue tree depending on whether you won or lost
        try
        {
        if (GameState.NPCs.Big_Rat.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        if (GameState.NPCs.Big_Rat.encountersWon.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterHenchmenAndNote";
        }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Big_Rat.encountersCompleted.OnChange -= OnEncounterComplete;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Big_Rat.encountersCompleted.OnChange -= OnEncounterComplete;
        }


    }

    private void UpdateDialogue()
    {
        if (GameState.NPCs.Alan.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
       
    }

    

}
