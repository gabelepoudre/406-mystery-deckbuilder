using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolverineDialogueTrigger : MonoBehaviour
{
    //will be called when player clicks wolverine
    public void StartDialogue()
    {
        if (GameState.NPCs.Wolverine.isInteractableAtBoxCar.Value || GameState.Player.location.Value != GameState.Player.Locations.Boxcar) {
                //since we have just started a dialogue, the last NPC spoken to is this one
            GameState.NPCs.lastNPCSpokenTo.Value = transform.GetComponent<NPC>().CharacterName;

            //update the met value since we've met them
            GameState.NPCs.npcNameToMet[transform.GetComponent<NPC>().CharacterName].Value = true;

            //start the dialogue based on the NPCs current dialogue key   
            string currentDialogueKey = transform.GetComponent<NPC>().CurrentDialogueKey;
            DialogueTree tree = transform.GetComponent<NPC>().DialogueTreeDictionary[currentDialogueKey];

            //call on the dialogue manager to start the dialogue, passing it the tree corresponding to the current dialogue key
            DialogueManager.Instance.StartDialogue(tree, this.gameObject);
            Debug.Log("triggered dialogue with " + transform.GetComponent<NPC>().CharacterName);
        }
        
    }

}
