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
        //dialogue based on whether you've won an encounter with Nibbles for the day
        GameState.NPCs.Rat_Prince.encountersCompleted.OnChange += OnEncounterComplete;
    }

    private void OnEncounterComplete()
    {
        //TODO: implement
    }

    

}

