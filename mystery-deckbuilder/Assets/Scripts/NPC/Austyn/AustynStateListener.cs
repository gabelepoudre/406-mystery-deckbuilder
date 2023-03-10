using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AustynStateListener : MonoBehaviour
{
     // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
        
        GameState.NPCs.Austyn.encountersCompleted.OnChange += OnEncounterComplete;
        GameState.currentDay.OnChange += OnDayChange;
    }

    private void OnEncounterComplete()
    {
        //if you've completed the first encounter, then we want to initiate the next dialogue tree depending on whether you won or lost
        
        if (GameState.NPCs.Austyn.encountersWon.Value == 1)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "IntroAfterEncounterWin";
        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "IntroAfterEncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        
    }

    //reset the post-loss dialogue to the normal one
    private void OnDayChange()
    {
        if (GameState.NPCs.Austyn.encountersWon.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Intro";
        }
    }
}
