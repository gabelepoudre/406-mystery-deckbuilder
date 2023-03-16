using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CroutonStateListener : MonoBehaviour
{
    private string _preEncounterDialogueKey = "";
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
        UpdateDialogue();
    }

    private void ChangeDialogueBasedOnState()
    {
       
        GameState.NPCs.Crouton.encountersCompleted.OnChange += OnEncounterComplete;
        
    }

    private void OnEncounterComplete()
    {
         //if you've completed the encounter, then we want to initiate the next dialogue tree depending on whether you won or lost
        try
        {
        _preEncounterDialogueKey = transform.GetComponent<NPC>().CurrentDialogueKey;
        if (GameState.NPCs.Crouton.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        if (GameState.NPCs.Crouton.encountersWon.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = _preEncounterDialogueKey;
        }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Crouton.encountersCompleted.OnChange -= OnEncounterComplete;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Crouton.encountersCompleted.OnChange -= OnEncounterComplete;
        }
    }

    private void UpdateDialogue()
    {
        if (GameState.Player.location.Value == GameState.Player.Locations.BerryFarm && !GameState.NPCs.Crouton.finishedBerryCommotion.Value)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BerryCommotion";
            GameState.NPCs.Crouton.finishedBerryCommotion.Value = true;
        }

        if (GameState.NPCs.Alan.encountersWon.Value > 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "DialogueWithAlan";
        }
        if (GameState.NPCs.Nina.encountersWon.Value > 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "DialogueWithNina";
        }
        if (GameState.NPCs.Alan.encountersWon.Value > 0 && GameState.NPCs.Nina.encountersWon.Value > 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "DialogueWithBoth";
        }

        if (GameState.NPCs.Crouton.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
        if (GameState.Meta.currentDay.Value == 7)
        {
            Debug.Log("Changed crouton dialogue");
            transform.GetComponent<NPC>().CurrentDialogueKey = "BuildRanOutOfDays";
        }
       
    }

    

}

