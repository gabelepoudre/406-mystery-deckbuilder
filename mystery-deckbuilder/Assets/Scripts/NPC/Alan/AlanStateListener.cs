using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlanStateListener : MonoBehaviour
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
    }

    private void OnEncounterComplete()
    {
        //if you've completed the first encounter, then we want to initiate the dialogue tree that corresponds to the 
        //key "IntroAfterEncounter"
        if (GameState.NPCs.Nibbles.encountersCompleted.Value == 1 && GameState.currentDay.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "IntroAfterEncounter";
            transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        }
    }

    

}
