using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_MobStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've won an encounter with Nibbles for the day
        
        try 
        {
            GameState.NPCs.Rat_Mob.encountersCompleted.OnChange += OnEncounterComplete;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Rat_Mob.encountersCompleted.OnChange -= OnEncounterComplete;
        }
    }

    private void OnEncounterComplete()
    {
        //TODO: implement
    }

    

}
