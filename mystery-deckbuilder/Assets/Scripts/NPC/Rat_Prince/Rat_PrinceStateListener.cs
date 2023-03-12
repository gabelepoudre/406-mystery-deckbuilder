using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_PrinceStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've won an encounter
        GameState.NPCs.Rat_Prince.encountersCompleted.OnChange += OnEncounterComplete;

        //location-dependent
        GameState.Player.location.OnChange += OnLocationChange;
    }

    private void OnEncounterComplete()
    {
         if (GameState.NPCs.Rat_Prince.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        if (GameState.NPCs.Rat_Prince.encountersCompleted.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Confession";
        }
    }

    private void OnLocationChange()
    {
        if (GameState.Player.location.Value == GameState.Player.Locations.Bar)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Bar";
        }
        else if (GameState.Player.location.Value == GameState.Player.Locations.Boxcar)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Boxcar";
        }
    }

    

}

