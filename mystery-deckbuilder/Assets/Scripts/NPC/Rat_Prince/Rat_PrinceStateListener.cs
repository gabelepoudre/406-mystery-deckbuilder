using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rat_PrinceStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateDialogue();
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've won an encounter
        
       
        GameState.NPCs.Rat_Prince.encountersCompleted.OnChange += OnEncounterComplete;
        
       
    }

    private void OnEncounterComplete()
    {
        try
        {
            string prevDialogue = transform.GetComponent<NPC>().CurrentDialogueKey;
            if (GameState.NPCs.Rat_Prince.encountersWon.Value == 1)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
                transform.GetComponent<NPCDialogueTrigger>().StartDialogue();
            }
            else
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
                transform.GetComponent<NPCDialogueTrigger>().StartDialogue();
                transform.GetComponent<NPC>().CurrentDialogueKey = prevDialogue;
            }
            
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Rat_Prince.encountersCompleted.OnChange -= OnEncounterComplete;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Rat_Prince.encountersCompleted.OnChange -= OnEncounterComplete;
        }

    }



    private void UpdateDialogue()
    {
        if (GameState.Player.location.Value == GameState.Player.Locations.Boxcar)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "BoxCar";

            //he doesn't hang around the boxcar after the player finishes whole event
            if (GameState.NPCs.Wolverine.isInteractableAtBoxCar.Value) {
                gameObject.SetActive(false);
            }
        }
        if (GameState.Player.location.Value == GameState.Player.Locations.Bar
        && GameState.NPCs.Wolverine.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Confession";
        }
        if (GameState.NPCs.Rat_Prince.encountersWon.Value > 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
    }

    

    

}

