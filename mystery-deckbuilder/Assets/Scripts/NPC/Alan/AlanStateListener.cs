using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlanStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
        UpdateDialogue();
    }

    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've won an encounter for the day

        try 
        {
            GameState.NPCs.Alan.encountersCompleted.OnChange += OnEncounterComplete;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Alan.encountersCompleted.OnChange -= OnEncounterComplete;
        }

    }

    private void OnEncounterComplete()
    {
       
        //if you've completed the first encounter, then we want to initiate the next dialogue tree depending on whether you won or lost
        
        if (GameState.NPCs.Alan.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        if (GameState.NPCs.Alan.encountersWon.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Intro";
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
