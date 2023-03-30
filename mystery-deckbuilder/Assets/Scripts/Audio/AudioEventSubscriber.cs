using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioEventSubscriber : MonoBehaviour
{
    public void Start()
    {
        GameState.Meta.activeEncounterComplianceRaisedByAmount.OnChange += CardPlayed;
        GameState.Meta.dialogueStarted.OnChange += DialogueStarted;
        GameState.Meta.dialogueAdvanced.OnChange += DialogueAdvanced;
        GameState.Meta.dialogueEnded.OnChange += DialogueEnded;
        GameState.Meta.inOpeningCutscene.OnChange += WithinIntroCutsceneChanged;
        GameState.Meta.withinDream.OnChange += WithinDreamStateChanged;
    }

    public void CardPlayed()
    {
        try
        {
            Debug.Log("Event LastCardPlayedChanged triggered");
            string lastCardPlayedElement = GameState.Meta.activeEncounterLastCardPlayedElement.Value;
            int patienceDealt = GameState.Meta.activeEncounterPatienceDroppedByAmount.Value;
            int complianceGained = GameState.Meta.activeEncounterComplianceRaisedByAmount.Value;
            switch (lastCardPlayedElement)
            {
                case "Intimidation":
                    // do stuff
                    break;
                case "Persuasion":
                    // do stuff
                    break;
                case "Sympathy":
                    // do stuff
                    break;
                case "Preparation":
                    // do stuff
                    break;
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterComplianceRaisedByAmount.OnChange -= CardPlayed;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterComplianceRaisedByAmount.OnChange -= CardPlayed;
        }
    }

    public void DialogueStarted()
    {
        try
        {
            Debug.Log("Event DialogueStarted triggered");
            if (GameState.Meta.dialogueStarted.Value == "npc")
            {
                // do stuff
            }
            else if (GameState.Meta.dialogueStarted.Value == "player")
            {
                // do if glub stuff
            }
            else
            {
                Debug.LogWarning("Audio listener had an invalid dialogue trigger");
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueStarted.OnChange -= DialogueStarted;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueStarted.OnChange -= DialogueStarted;
        }
    }

    public void DialogueAdvanced()
    {
        try
        {
            Debug.Log("Event DialogueStarted triggered");
            if (GameState.Meta.dialogueAdvanced.Value == "npc")
            {
                // do stuff
            }
            else if (GameState.Meta.dialogueAdvanced.Value == "player")
            {
                // do if glub stuff
            }
            else
            {
                Debug.LogWarning("Audio listener had an invalid dialogue trigger");
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueAdvanced.OnChange -= DialogueAdvanced;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueAdvanced.OnChange -= DialogueAdvanced;
        }
    }

    public void DialogueEnded()
    {
        try
        {
            Debug.Log("Event DialogueEnded triggered");
            if (GameState.Meta.dialogueEnded.Value == "npc")
            {
                // do stuff
            }
            else if (GameState.Meta.dialogueEnded.Value == "player")
            {
                // do if glub stuff
            }
            else
            {
                Debug.LogWarning("Audio listener had an invalid dialogue trigger");
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueEnded.OnChange -= DialogueEnded;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueEnded.OnChange -= DialogueEnded;
        }
    }

    public void WithinIntroCutsceneChanged()
    {
        try
        {
            Debug.Log("Event opening cutscene triggered");
            if (GameState.Meta.inOpeningCutscene.Value)
            {
                // do stuff on enter
            }
            else
            {
                // do other stuff on exit
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.inOpeningCutscene.OnChange -= WithinIntroCutsceneChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.inOpeningCutscene.OnChange -= WithinIntroCutsceneChanged;
        }
    }

    public void WithinDreamStateChanged()
    {
        try
        {
            Debug.Log("Event WithinDreamState triggered");
            if (GameState.Meta.withinDream.Value)
            {
                // do stuff on enter
            }
            else
            {
                // do stuff on exit
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.withinDream.OnChange -= WithinDreamStateChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.withinDream.OnChange -= WithinDreamStateChanged;
        }
    }

    public void Changed()
    {
        try
        {
            Debug.Log("Event  triggered");
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
        }
    }
}
