using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elk_SecretaryStateListener : MonoBehaviour
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
        
        try 
        {
            GameState.NPCs.Elk.encountersCompleted.OnChange += OnEncounterComplete;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Elk.encountersCompleted.OnChange -= OnEncounterComplete;
        }

        //we have to subscribe to the value since crouton is in the same scene
        try 
        {
            GameState.NPCs.Crouton.encountersWon.OnChange += UpdateDialogue;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Crouton.encountersWon.OnChange -= UpdateDialogue;
        }
    }

    private void OnEncounterComplete()
    {
         //if you've completed the first encounter, then we want to initiate the next dialogue tree depending on whether you won or lost
        
        _preEncounterDialogueKey = transform.GetComponent<NPC>().CurrentDialogueKey;
        
        if (GameState.NPCs.Elk.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        if (GameState.NPCs.Elk.encountersWon.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = _preEncounterDialogueKey;
        }
    }

    private void UpdateDialogue()
    {
        

        if (GameState.NPCs.Alan.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BuildDialogueWithAlan";
        }

        if (GameState.NPCs.Nina.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BuildDialogueWithNina";
        }

        if (GameState.NPCs.Alan.encountersWon.Value == 1 && GameState.NPCs.Nina.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BuildDialogueWithAlanAndNina";
        }

        if (GameState.NPCs.Crouton.encountersWon.Value == 1 && GameState.NPCs.Nina.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BuildDialogueWithCroutonAndNina";
        }

        if (GameState.NPCs.Alan.encountersWon.Value == 1 && GameState.NPCs.Crouton.encountersWon.Value == 1 && GameState.NPCs.Nina.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BuildDialogueWithAllThree";
        }

        if (GameState.NPCs.Alan.encountersWon.Value == 1 && GameState.NPCs.Crouton.gaveEvidence.Value)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BuildAlanWithEvidence";
        }

        if (GameState.NPCs.Alan.encountersWon.Value == 1 &&
        GameState.NPCs.Nina.encountersWon.Value == 1 && GameState.NPCs.Crouton.gaveEvidence.Value)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BuildAlanWithEvidenceAndNina";
        }

        if (GameState.NPCs.Alan.encountersWon.Value == 1 && GameState.NPCs.Crouton.encountersWon.Value == 1 &&
        GameState.NPCs.Nina.encountersWon.Value == 1 && GameState.NPCs.Crouton.gaveEvidence.Value)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BuildAlanWithEvidenceAndNinaAndCrouton";
        }

        if (GameState.NPCs.Elk.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
       
    }




    

}
