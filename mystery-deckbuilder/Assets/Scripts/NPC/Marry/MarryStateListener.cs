using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarryStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void ChangeDialogueBasedOnState()
    {
        
        try 
        {
            GameState.NPCs.Marry.encountersCompleted.OnChange += OnEncounterComplete;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Marry.encountersCompleted.OnChange -= OnEncounterComplete;
        }
    }

    private void OnEncounterComplete()
    {
       //TODO: implement
    }

    

}
