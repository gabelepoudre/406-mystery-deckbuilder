using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samuel_SnakeStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
        GameState.NPCs.Samuel_Snake.encountersCompleted.OnChange += OnEncounterComplete;
    }

    private void OnEncounterComplete()
    {
        //if you've completed the first encounter, then we want to initiate the dialogue tree that corresponds to the 
        //key "IntroAfterEncounter"
        if (GameState.NPCs.Samuel_Snake.encountersCompleted.Value == 1 && GameState.currentDay.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "IntroAfterEncounter";
            transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        }
    }

    

}
