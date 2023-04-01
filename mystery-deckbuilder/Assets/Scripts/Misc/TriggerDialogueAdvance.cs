using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueAdvance : MonoBehaviour
{
    public void DialogueAdvance()
    {
        GameState.Meta.dialogueAdvanced.Raise();
    }
}
