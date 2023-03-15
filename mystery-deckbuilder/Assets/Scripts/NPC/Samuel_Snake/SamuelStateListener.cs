using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SamuelStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
        UpdateDialogue();
    }

    private void ChangeDialogueBasedOnState()
    {
        
        
        GameState.NPCs.Samuel.encountersCompleted.OnChange += OnEncounterComplete;
       
    }

    private void OnEncounterComplete()
    {

        //if you've completed the first encounter, then we want to initiate the next dialogue tree depending on whether you won or lost
        try
        {
            if (GameState.NPCs.Samuel.encountersWon.Value == 1)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
            }
            else
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
            }

            transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

            if (GameState.NPCs.Samuel.encountersWon.Value == 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "Intro";
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Samuel.encountersCompleted.OnChange -= OnEncounterComplete;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Samuel.encountersCompleted.OnChange -= OnEncounterComplete;
        }
    }

    private void UpdateDialogue()
    {
        if (GameState.NPCs.Samuel.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
       
    }

    

  
    

}
