using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DougStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
    }

    private void ChangeDialogueBasedOnState()
    {
        //dialogue based on whether you've won an encounter with him for the day
        try 
        {
            GameState.NPCs.Doug.encountersCompleted.OnChange += OnEncounterComplete;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Doug.encountersCompleted.OnChange -= OnEncounterComplete;
        }


        //dialogue based on whether you've met austyn or mark or both
        try 
        {
            GameState.NPCs.Austyn.met.OnChange += MetAustynOrMark;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Austyn.met.OnChange -= MetAustynOrMark;
        }

         try 
        {
            GameState.NPCs.Mark.met.OnChange += MetAustynOrMark;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Mark.met.OnChange -= MetAustynOrMark;
        }

       
    }

    private void OnEncounterComplete()
    {
        //if you've completed the first encounter, then we want to initiate the encounter win tree
        if (GameState.NPCs.Doug.encountersCompleted.Value == 1)
        {
            //if Doug was the first main suspect we won an encounter with
            if (GameState.NPCs.Elk.encountersCompleted.Value == 0 && GameState.NPCs.Rat_Leader.encountersCompleted.Value == 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWinFirstMainSuspect";
            }
            else //if he was the second or third
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterWinSecondOrThirdMainSuspect";
            }
            

        }
        else
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "AfterEncounterLoss";
        }

        transform.GetComponent<NPCDialogueTrigger>().StartDialogue();

        if (GameState.NPCs.Doug.encountersCompleted.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Intro";
        }

        
    }

    private void MetAustynOrMark()
    {
        if (GameState.NPCs.Austyn.met.Value)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetAustynOnly";
        }
        if (GameState.NPCs.Mark.met.Value)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetMarkOnly";
        }
        if (GameState.NPCs.Austyn.met.Value && GameState.NPCs.Mark.met.Value)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetBoth";
        }
    }

 

    

}

