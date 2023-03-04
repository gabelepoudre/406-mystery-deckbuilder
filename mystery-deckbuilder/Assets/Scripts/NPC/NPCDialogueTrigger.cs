using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* A placeholder dialogue trigger class for the alpha */
public class NPCDialogueTrigger : MonoBehaviour
{

    //will be called when player clicks Nibbles
    public void StartDialogue()
    {
        //since we have just started a dialogue, the last NPC spoken to is this one
        GameState.NPCs.lastNPCSpokenTo = transform.GetComponent<NPC>().CharacterName;

        //start the dialogue based on the NPCs current dialogue key   
        string currentDialogueKey = transform.GetComponent<NPC>().CurrentDialogueKey;
        DialogueTree tree = transform.GetComponent<NPC>().DialogueTreeDictionary[currentDialogueKey];

        //call on the dialogue manager to start the dialogue, passing it the tree corresponding to the current dialogue key
        DialogueManager.Instance.StartDialogue(tree, this.gameObject);
        Debug.Log("triggered dialogue");
    }

   
}
