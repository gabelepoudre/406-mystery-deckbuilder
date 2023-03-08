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
        GameState.NPCs.Austin.encountersCompleted.OnChange += OnEncounterComplete;
        GameState.currentDay.OnChange += OnDayChange;
        GameState.Meta.currentAct.OnChange += OnActChange;
    }

    private void OnEncounterComplete()
    {
        //if you've completed the encounter, then we want to initiate the dialogue tree that corresponds to the correct dialogue tree
        if (GameState.NPCs.Austin.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Act2EncounterWin";
        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Act2EncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();
    }


    //reset the post-loss dialogue to the normal one
    private void OnDayChange()
    {
        if (GameState.NPCs.Austin.encountersWon.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Intro";
        }
    }

    //gets a different dialogue in act 2
    private void OnActChange()
    {
        if (GameState.Meta.currentAct.Value == 2)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Act2";
        }
    }

    

}
