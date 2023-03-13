using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarryStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
        OnEncounterComplete();
    }

    //marry has no encounters but..
    //TODO: implement if she has other state based dialogue changes
    private void ChangeDialogueBasedOnState()
    {
        return;
       
    }

    //marry has no encounters
    private void OnEncounterComplete()
    {
       return;
    }

    

}
