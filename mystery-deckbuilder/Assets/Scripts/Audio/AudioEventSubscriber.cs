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
        GameState.Meta.lastDayPostDream.OnChange += LastDayPostDreamChanged;
        GameState.Meta.inBadEnd.OnChange += WithinBadEndChanged;
        GameState.Meta.inGoodEnd.OnChange += WithinGoodEndChanged;
        GameState.Meta.inPickSuspect.OnChange += InPickASuspectChanged;
        GameState.Meta.nonEncounterCardHoverOver.OnChange += NonEncounterCardHoverChanged;
        GameState.Meta.activeEncounterCardHoverOver.OnChange += EncounterCardHoverChanged;
        GameState.Meta.dbCardMovedToDeck.OnChange += DeckbuilderCardAddedToDeck;
        GameState.Meta.dbCardRemovedFromDeck.OnChange += DeckbuilderCardRemovedFromDeck;
        GameState.Meta.pageDownTrigger.OnChange += PageDown;
        GameState.Meta.pageUpTrigger.OnChange += PageUp;
        GameState.Meta.notepadActive.OnChange += InMenuChanged;
        GameState.Meta.mapIsOpen.OnChange += InMapChanged;
        GameState.Meta.menuNotepadPageSwitch.OnChange += MenuPageFlip;
        GameState.Meta.menuNotepadTabSwitch.OnChange += MenuTabFlip;
        GameState.Meta.mapLocationClicked.OnChange += MapLocationPicked;
        GameState.Meta.activeEncounter.OnChange += EncounterChanged;
        GameState.Meta.activeEncounterInWinScreen.OnChange += InEncounterWin;
        GameState.Meta.activeEncounterInLossScreen.OnChange += InEncounterLoss;
        GameState.Meta.activeEncounterCardHelp.OnChange += EncounterCardHelpClicked;
        GameState.Player.location.OnChange += LocationChanged;
        GameState.Meta.dialogueGoing.OnChange += DialogueGoing;
        GameState.Meta.activeEncounterCardDrawn.OnChange += CardDrawn;
        GameState.Meta.secretFound.OnChange += SecretFound;
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

    public void LocationChanged()
    {
        try
        {
            Debug.Log("Event LocationChanged triggered");
            if (GameState.Player.location.Value == GameState.Player.Locations.Bar)
            {
                // do stuff. Note: if you want to ensure you don't replay base music on 
                //  each location swap but still want pub music, you'll want to keep a variable like
                //  "normal music playing" and check against it for an else if to this conditional
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.location.OnChange -= LocationChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.location.OnChange -= LocationChanged;
        }
    }

    public void CardDrawn()
    {
        try
        {
            Debug.Log("Event CardDrawn triggered");
            // do stuff, value is not used
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterCardDrawn.OnChange -= CardDrawn;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterCardDrawn.OnChange -= CardDrawn;
        }
    }

    public void SecretFound()
    {
        try
        {
            Debug.Log("Event SecretFound triggered");
            // do stuff, value is not used
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.secretFound.OnChange -= SecretFound;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.secretFound.OnChange -= SecretFound;
        }
    }


    public void DialogueStarted()
    {
        // note: this only happens once when dialogue box is opened
        try
        {
            Debug.Log("Event DialogueStarted triggered");
            // do stuff, value is not used
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
        // note: this only happens when the advance button or an option button are clicked
        try
        {
            Debug.Log("Event DialogueAdvanced triggered");
            // do stuff, value is not used
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
        // note: this only happens once when dialogue end (including when encounters start)
        try
        {
            Debug.Log("Event DialogueEnded triggered");
            // do stuff, value is not used
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

    public void DialogueGoing()
    {
        // note: this one is for advanced stuff like NPCs talking. Is true while they are talking and false otherwise
        try
        {
            Debug.Log("Event DialogueGoing triggered with value " + GameState.Meta.dialogueGoing.Value);
            if (GameState.Meta.dialogueGoing.Value == "npc")
            {
                // do stuff
            }
            else if (GameState.Meta.dialogueGoing.Value == "player")
            {
                // do if glub stuff
            }
            else if (GameState.Meta.dialogueGoing.Value == "")
            {
                // do no one talking stuff
            }
            else
            {
                Debug.LogWarning("Audio listener had an invalid dialogue trigger");
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueGoing.OnChange -= DialogueGoing;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueGoing.OnChange -= DialogueGoing;
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
                if(GameState.Meta.currentDay.Value == GameState.Meta.lastDay)
                {
                    GameState.Meta.lastDayPostDream.Raise();
                }
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

    public void LastDayPostDreamChanged()
    {
        try
        {
            Debug.Log("Event lastdaypostdreamchanged triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.lastDayPostDream.OnChange -= LastDayPostDreamChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.lastDayPostDream.OnChange -= LastDayPostDreamChanged;
        }
    }

    public void WithinBadEndChanged()
    {
        try
        {
            Debug.Log("Event WithinBadEndChanged triggered");
            if (GameState.Meta.inBadEnd.Value)
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
            GameState.Meta.inBadEnd.OnChange -= WithinBadEndChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.inBadEnd.OnChange -= WithinBadEndChanged;
        }
    }

    public void WithinGoodEndChanged()
    {
        try
        {
            Debug.Log("Event WithinGoodEndChanged triggered");
            if (GameState.Meta.inGoodEnd.Value)
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
            GameState.Meta.inGoodEnd.OnChange -= WithinGoodEndChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.inGoodEnd.OnChange -= WithinGoodEndChanged;
        }
    }

    public void InPickASuspectChanged()
    {
        try
        {
            Debug.Log("Event InPickASuspectChanged triggered");
            if (GameState.Meta.inPickSuspect.Value)
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
            GameState.Meta.inPickSuspect.OnChange -= InPickASuspectChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.inPickSuspect.OnChange -= InPickASuspectChanged;
        }
    }

    public void NonEncounterCardHoverChanged()
    {
        try
        {
            Debug.Log("Event NonEncounterCardHoverChanged triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.nonEncounterCardHoverOver.OnChange -= NonEncounterCardHoverChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.nonEncounterCardHoverOver.OnChange -= NonEncounterCardHoverChanged;
        }
    }

    public void EncounterCardHoverChanged()
    {
        try
        {
            Debug.Log("Event EncounterCardHoverChanged triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterCardHoverOver.OnChange -= EncounterCardHoverChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterCardHoverOver.OnChange -= EncounterCardHoverChanged;
        }
    }

    public void DeckbuilderCardAddedToDeck()
    {
        try
        {
            Debug.Log("Event DeckbuilderCardAddedToDeck triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dbCardMovedToDeck.OnChange -= DeckbuilderCardAddedToDeck;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dbCardMovedToDeck.OnChange -= DeckbuilderCardAddedToDeck;
        }
    }

    public void DeckbuilderCardRemovedFromDeck()
    {
        try
        {
            Debug.Log("Event DeckbuilderCardRemovedFromDeck triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dbCardRemovedFromDeck.OnChange -= DeckbuilderCardRemovedFromDeck;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dbCardRemovedFromDeck.OnChange -= DeckbuilderCardRemovedFromDeck;
        }
    }

    public void PageDown()
    {
        try
        {
            Debug.Log("Event PageDown triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.pageDownTrigger.OnChange -= PageDown;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.pageDownTrigger.OnChange -= PageDown;
        }
    }

    public void PageUp()
    {
        try
        {
            Debug.Log("Event PageUp triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.pageUpTrigger.OnChange -= PageUp;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.pageUpTrigger.OnChange -= PageUp;
        }
    }

    public void InMenuChanged()
    {
        try
        {
            Debug.Log("Event InMenuChanged triggered");
            if (GameState.Meta.notepadActive.Value)
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
            GameState.Meta.notepadActive.OnChange -= InMenuChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.notepadActive.OnChange -= InMenuChanged;
        }
    }

    public void MenuPageFlip()
    {
        try
        {
            Debug.Log("Event MenuPageFlip triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.menuNotepadPageSwitch.OnChange -= MenuPageFlip;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.menuNotepadPageSwitch.OnChange -= MenuPageFlip;
        }
    }

    public void MenuTabFlip()
    {
        try
        {
            Debug.Log("Event MenuTabFlip triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.menuNotepadTabSwitch.OnChange -= MenuTabFlip;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.menuNotepadTabSwitch.OnChange -= MenuTabFlip;
        }
    }

    public void InMapChanged()
    {
        try
        {
            Debug.Log("Event InMapChanged triggered");
            if (GameState.Meta.mapIsOpen.Value)
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
            GameState.Meta.mapIsOpen.OnChange -= InMapChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.mapIsOpen.OnChange -= InMapChanged;
        }
    }

    public void MapLocationPicked()
    {
        try
        {
            Debug.Log("Event MapLocationPicked triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.mapLocationClicked.OnChange -= MapLocationPicked;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.mapLocationClicked.OnChange -= MapLocationPicked;
        }
    }

    public void EncounterChanged()
    {
        try
        {
            Debug.Log("Event EncounterChanged triggered");
            if (GameState.Meta.activeEncounter.Value != null)
            {
                if(GameState.Meta.activeEncounter.Value.GetOpponent().IsBoss)
                {
                    // do start encounter with boss
                }
                else
                {
                    // do start encounter non boss
                }
            }
            else
            {
                // do stuff on exit (not sure if called before or after win screen)
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounter.OnChange -= EncounterChanged;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounter.OnChange -= EncounterChanged;
        }
    }

    public void InEncounterWin()
    {
        try
        {
            Debug.Log("Event InEncounterWin triggered");
            if (GameState.Meta.activeEncounterInWinScreen.Value)
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
            GameState.Meta.activeEncounterInWinScreen.OnChange -= InEncounterWin;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterInWinScreen.OnChange -= InEncounterWin;
        }
    }

    public void InEncounterLoss()
    {
        try
        {
            Debug.Log("Event InEncounterLoss triggered");
            if (GameState.Meta.activeEncounterInLossScreen.Value)
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
            GameState.Meta.activeEncounterInLossScreen.OnChange -= InEncounterLoss;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterInLossScreen.OnChange -= InEncounterLoss;
        }
    }

    public void EncounterCardHelpClicked()
    {
        try
        {
            Debug.Log("Event EncounterCardHelpClicked triggered");
            // do stuff here, value is meaningless
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterCardHelp.OnChange -= EncounterCardHelpClicked;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterCardHelp.OnChange -= EncounterCardHelpClicked;
        }
    }
}
