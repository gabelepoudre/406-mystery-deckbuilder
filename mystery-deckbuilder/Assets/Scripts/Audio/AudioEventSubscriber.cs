using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

// ADUIO MANAGER USAGE:
//      Make sure this is placed in the first scene the game loads.
// TO PLAY A SOUND:
//      In any script:
//          FindObjectOfType<AudioManager>().Play("filename-of-sound");
// TO STOP A SOUND:
//      In any script:
//          FindObjectOfType<AudioManager>().Stop("filename-of-sound");

public class AudioEventSubscriber : MonoBehaviour
{
    // Previous song; song played before encounter
    String previousSong = "music-town-new";

    // Set value for berry farm leaving after commotion.
    bool leftCommotion = false;

    // Previous babble; used for stopping babble when advancing dialogue
    String previousBabble = "ac_voice_b1";

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
            // if (GameState.Meta.activeEncounterComplianceRaisedByAmount.Value > 40)
            // else if (GameState.Meta.activeEncounterComplianceRaisedByAmount.Value > 20)
            // else if (GameState.Meta.activeEncounterComplianceRaisedByAmount.Value > 0)
            switch (lastCardPlayedElement)
            {
                case "Intimidation":
                    // Play light hit sound
                    FindObjectOfType<AudioManager>().Play("effect-hit-light");
                    break;
                case "Persuasion":
                    // Play light hit sound
                    FindObjectOfType<AudioManager>().Play("effect-hit-light");
                    break;
                case "Sympathy":
                    // Play light hit sound
                    FindObjectOfType<AudioManager>().Play("effect-hit-light");
                    break;
                case "Preparation":
                    // Play light hit sound
                    FindObjectOfType<AudioManager>().Play("effect-hit-light");
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

            // For berry commotion:

            // On Berry Commotion exit:

            // (if the player has not left the commotion before,
            // and the day is greater than 1,
            // and the berry commotion has happened)
            if (leftCommotion == true && GameState.Meta.currentDay.Value > 1 && GameState.NPCs.Crouton.finishedBerryCommotion.Value && GameState.Player.location.Value != GameState.Player.Locations.BerryFarm)
            {
                Debug.Log("Left berry commotion!");
                //     Stop playing all sounds
                FindObjectOfType<AudioManager>().StopAll();
                //     Then, 
                //     Play town theme
                FindObjectOfType<AudioManager>().Play("music-town-new");
            }

            // play berry commotion music
            else if (GameState.Meta.currentDay.Value == 2 && leftCommotion == false && GameState.Player.location.Value == GameState.Player.Locations.BerryFarm)
            {
                FindObjectOfType<AudioManager>().StopAll();
                FindObjectOfType<AudioManager>().Play("music-encounter-danger");
                leftCommotion = true;
            }

            



            // For rat pub:
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
            // play menu sound
            FindObjectOfType<AudioManager>().Play("effect-menu-sound-4");
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
            // play menu sound
            FindObjectOfType<AudioManager>().Play("effect-menu-sound-4");
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
            if (GameState.Meta.dialogueGoing.Value == "player")
            {
                // do if glub stuff
                FindObjectOfType<AudioManager>().Play("effect-glub");
            }
            else if (GameState.Meta.dialogueGoing.Value == "")
            {
                // do no one talking stuff
            }
            else
            {
                Debug.Log(GameState.Meta.dialogueGoing.Value);

                // Stop previous animal noise sound
                FindObjectOfType<AudioManager>().Stop(previousBabble);

                int randomNum = Random.Range(1, 8);
                switch(GameState.Meta.dialogueGoing.Value)
                {
                    case "Nibbles":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_b" + randomNum;
                        break;
                    case "Austin":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_c" + randomNum;
                        break;
                    case "Austyn":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_c" + randomNum;
                        break;
                    case "Alan":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_a" + randomNum;
                        break;
                    case "Mark":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_a" + randomNum;
                        break;
                    case "Samuel":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_b" + randomNum;
                        break;
                    case "Doug":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_a" + randomNum;
                        break;
                    case "Elk Secretary":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_c" + randomNum;
                        break;
                    case "Rat Leader":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_a" + randomNum;
                        break;
                    case "Rat Prince":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_c" + randomNum;
                        break;
                    case "Big Rat":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_a" + randomNum;
                        break;
                    case "Bee":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_b" + randomNum;
                        break;
                    case "Marry":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_b" + randomNum;
                        break;
                    case "Wolverine":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_a" + randomNum;
                        break;
                    case "Black Bear":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_b" + randomNum;
                        break;
                    case "Crouton":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_b" + randomNum;
                        break;
                    case "Nina":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_b" + randomNum;
                        break;
                    case "Mike":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_b" + randomNum;
                        break;
                    case "Speck":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_c" + randomNum;
                        break;
                    case "Oslow":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_c" + randomNum;
                        break;
                    case "Clay":
                        // Generate animal noise sound
                        previousBabble = "ac_voice_c" + randomNum;
                        break;
                }
                // Play animal noise sound
                FindObjectOfType<AudioManager>().Play(previousBabble);
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
                // play bad end theme
                FindObjectOfType<AudioManager>().Play("music-bad-end");
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
                // play good end theme
                FindObjectOfType<AudioManager>().Play("music-good-end");
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
            // play card hover sound
            FindObjectOfType<AudioManager>().Play("effect-card-mouse-over-1");
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
            // play card hover sound
            FindObjectOfType<AudioManager>().Play("effect-card-mouse-over-2");
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
            // play card hover sound
            FindObjectOfType<AudioManager>().Play("effect-card-mouse-over-3");
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
            // play card hover sound
            FindObjectOfType<AudioManager>().Play("effect-card-mouse-over-3");
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
            // Play sound
            FindObjectOfType<AudioManager>().Play("effect-menu-tab");
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
            // Play sound
            FindObjectOfType<AudioManager>().Play("effect-menu-tab");
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
            // Play page flip sound
            FindObjectOfType<AudioManager>().Play("effect-page-flip");
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
            // Play sound
            FindObjectOfType<AudioManager>().Play("effect-menu-tab");
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
                // Play map sound
                FindObjectOfType<AudioManager>().Play("effect-map");
            }
            else
            {
                // do stuff on exit
                // Play map sound
                FindObjectOfType<AudioManager>().Play("effect-map");
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
            // stop map sound
            FindObjectOfType<AudioManager>().Stop("effect-map");
            // Play menu sound
            FindObjectOfType<AudioManager>().Play("effect-menu-sound-4");
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
                //     Stop playing all sounds
                FindObjectOfType<AudioManager>().StopAll();

                if (GameState.Meta.activeEncounter.Value.GetOpponent().IsBoss)
                {
                    // do start encounter with boss
                    //     Play boss encounter theme
                    FindObjectOfType<AudioManager>().Play("music-encounter-danger");
                }
                else
                {
                    // do start encounter non boss

                    //     Play encounter theme
                    FindObjectOfType<AudioManager>().Play("music-encounter-normal");
                }
            }
            else
            {
                // do stuff on exit;
                // this is triggered AFTER the win/lose screen
                FindObjectOfType<AudioManager>().StopAll();
                FindObjectOfType<AudioManager>().Play(previousSong);
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
                // stop playing all sounds
                FindObjectOfType<AudioManager>().StopAll();

                // play victory theme
                FindObjectOfType<AudioManager>().Play("music-encounter-victory");
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
                // stop playing all sounds
                FindObjectOfType<AudioManager>().StopAll();

                // play defeat theme
                FindObjectOfType<AudioManager>().Play("music-encounter-defeat");
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
