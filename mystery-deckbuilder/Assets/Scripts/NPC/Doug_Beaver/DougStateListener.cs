using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DougStateListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeDialogueBasedOnState();
        MetAustynOrMarkOrSamuel();
        UpdateDialogue();
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


        //i don't think there's a need to subscribe to the state, but i'm keeping it just in case
        /*
        try 
        {
            GameState.NPCs.Austyn.met.OnChange += MetAustynOrMarkOrSamuel;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Austyn.met.OnChange -= MetAustynOrMarkOrSamuel;
        }

        try 
        {
            GameState.NPCs.Mark.met.OnChange += MetAustynOrMarkOrSamuel;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Mark.met.OnChange -= MetAustynOrMarkOrSamuel;
        }
        try 
        {
            GameState.NPCs.Samuel.met.OnChange += MetAustynOrMarkOrSamuel;
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Samuel.met.OnChange -= MetAustynOrMarkOrSamuel;
        }
        */

       
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

        if (GameState.NPCs.Doug.encountersWon.Value == 0)
        {
            transform.GetComponent<NPC>().CurrentDialogueKey = "Intro";
        }

    }


    private void MetAustynOrMarkOrSamuel()
    {
        if (GameState.NPCs.Doug.encountersWon.Value == 0)
        {
            if (GameState.NPCs.Austyn.encountersWon.Value > 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetAustynOnly";
            }

            if (GameState.NPCs.Mark.encountersWon.Value > 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetMarkOnly";
            }

            if (GameState.NPCs.Samuel.encountersWon.Value > 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetSamuelOnly";
            }

            if (GameState.NPCs.Austyn.encountersWon.Value > 0 && GameState.NPCs.Mark.encountersWon.Value > 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetAustinAndMark";
            }

            if (GameState.NPCs.Austyn.encountersWon.Value > 0 && GameState.NPCs.Samuel.encountersWon.Value > 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetAustinAndSamuel";
            }

            if (GameState.NPCs.Mark.encountersWon.Value > 0 && GameState.NPCs.Samuel.encountersWon.Value > 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetMarkAndSamuel";
            }

            if (GameState.NPCs.Austyn.encountersWon.Value > 0 && GameState.NPCs.Mark.encountersWon.Value > 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetAustynAndMark";
            }

            if (GameState.NPCs.Austyn.encountersWon.Value > 0 && GameState.NPCs.Mark.encountersWon.Value > 0 && GameState.NPCs.Samuel.encountersWon.Value > 0)
            {
                transform.GetComponent<NPC>().CurrentDialogueKey = "IntroMetAllThree";
            }
        }
       
    }


    private void UpdateDialogue()
    {
        if (GameState.NPCs.Doug.encountersWon.Value == 1)
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
       
    }

}

 

    



