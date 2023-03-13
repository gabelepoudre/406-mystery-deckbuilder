using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AustinStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've completed an encounter (succesfully or not)
        try 
        {
            GameState.NPCs.Austin.encountersCompleted.OnChange += OnEncounterComplete;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Austin.encountersCompleted.OnChange -= OnEncounterComplete;
        }

    }

    private void OnEncounterComplete()
    {
        //if you've completed the encounter, then we want to initiate the dialogue tree that corresponds to the correct dialogue tree
        if (GameState.NPCs.Austin.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "EncounterWin";
        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "EncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        if (GameState.NPCs.Austin.encountersWon.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Intro";
        }
    }

    

}
