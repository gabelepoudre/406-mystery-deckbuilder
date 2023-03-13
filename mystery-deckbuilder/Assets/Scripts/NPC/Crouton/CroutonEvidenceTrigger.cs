using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroutonEvidenceTrigger : MonoBehaviour
{
    //to be triggered on click
    public void TriggerEvidence()
    {
        if (transform.GetComponent<NPC>().CurrentDialogueKey == "DialogueWithAlan")
        {
            GameState.NPCs.Crouton.gaveEvidence.Value = true;
        }
    }
}
