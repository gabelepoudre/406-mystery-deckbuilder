using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolverineStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
     
        GameState.NPCs.Wolverine.encountersCompleted.OnChange += OnEncounterComplete;
    }

    private void OnEncounterComplete()
    {
        //TODO: implement
    }

    

}

