using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_PrinceStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
        UpdateDialogue();
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
            if (GameState.NPCs.Rat_Prince.encountersWon.Value == 1)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
            }
            else
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
            }

            transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

            if (GameState.NPCs.Rat_Prince.encountersWon.Value == 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "Confession";
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Rat_Prince.encountersCompleted.OnChange -= OnEncounterComplete;
        }

    }



    private void UpdateDialogue()
    {
        if (GameState.Player.location.Value == GameState.Player.Locations.Boxcar)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Confession";
        }
        if (GameState.NPCs.Rat_Prince.encountersWon.Value > 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWin";
        }
    }

    

    

}

