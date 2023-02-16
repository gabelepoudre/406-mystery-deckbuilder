using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A placeholder dialogue trigger class for the alpha */
public class NibblesDialogueTrigger : MonoBehaviour
{

    //will be called when player clicks Nibbles
    public void StartDialogue1()
    {
        Debug.Log("triggered dialogue");
        DialogueTree tree = transform.GetComponent<NibblesIntro>().dialogueTree;
        string name = transform.GetComponent<NPC>().name;
        DialogueBoxManager.Instance.StartDialogue(tree, name);
    }
}
