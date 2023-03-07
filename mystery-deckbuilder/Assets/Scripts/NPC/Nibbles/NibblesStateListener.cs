using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NibblesStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've won an encounter with Nibbles for the day
        GameState.NPCs.Nibbles.encountersCompleted.OnChange += OnEncounterComplete;
        GameState.currentDay.OnChange += OnDayChange;
    }

    private void OnEncounterComplete()
    {
        //if you've completed the first encounter, then we want to initiate the next dialogue tree depending on whether you won or lost
        if (GameState.NPCs.Nibbles.encountersCompleted.Value == 1)
        {
            if (GameState.Meta.lastEncounterWin.Value)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroAfterEncounterWin";
            }
            else
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroAfterEncounterLoss";
            }

            transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        }
    }

    //reset the post-win / post-loss dialogue to the normal one
    private void OnDayChange()
    {
        if (GameState.NPCs.Nibbles.encountersCompleted.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Intro";
        }
    }

   



    

}
