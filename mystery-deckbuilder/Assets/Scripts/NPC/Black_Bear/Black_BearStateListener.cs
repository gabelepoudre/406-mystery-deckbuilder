using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Black_BearStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    //bear has no encounters but..
    //TODO: implement if he has other state based dialogue changes
    private void ChangeDialogueBasedOnState()
    {
        
        return;
    }

    //bear has no encounters
    private void OnEncounterComplete()
    {
        return;
    }

    //TODO: implement if he has other state based dialogue changes
    private void UpdateDialogue()
    {

    }
}
