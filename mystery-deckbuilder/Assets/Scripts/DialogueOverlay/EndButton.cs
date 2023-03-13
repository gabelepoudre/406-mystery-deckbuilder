using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    public void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue();
    }
}
