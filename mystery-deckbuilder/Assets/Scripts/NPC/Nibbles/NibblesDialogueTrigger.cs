using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A placeholder dialogue trigger class for the alpha */
public class NibblesDialogueTrigger : MonoBehaviour
{

    //will be called when player clicks Nibbles
    public void StartDialogue()
    {
        GameState.NPCs.latestNPCEncountersCompleted.Value = GameState.NPCs.Nibbles.encountersCompleted;
        string currentDialogueKey = transform.GetComponent<NPC>().CurrentDialogueKey;
        DialogueTree tree = transform.GetComponent<NPC>().DialogueTreeDictionary[currentDialogueKey];
        DialogueManager.Instance.StartDialogue(tree, this.gameObject);
        Debug.Log("triggered dialogue");
    }

   
}
