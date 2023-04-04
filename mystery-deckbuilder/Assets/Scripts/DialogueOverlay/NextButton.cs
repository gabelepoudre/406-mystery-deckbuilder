/*
 * author(s) Ehsan Soltan
 *
 * This script contains the NextButton class
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is responsible only for calling the DialogueBoxManager to
 * command the dialogue box to display the next sentence
 */
public class NextButton : MonoBehaviour
{
    /* Calls on the DialogueBoxManager singleton to command the dialogue box
     * that it instantiated to display the next sentence in its queue 
     */
    public void GetNextSentence()
    {
        GameState.Meta.dialogueAdvanced.Raise();
        GameState.Player.glubTalkingInDialogue.Value = false;
        DialogueManager.Instance.DisplayNextSentence();
    }
}
