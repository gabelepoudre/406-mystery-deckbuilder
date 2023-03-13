using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryFarmCommotionEventTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        TriggerEvent();
    }

    private void TriggerEvent()
    {   if (GameState.Meta.currentGameplayPhase.Value == GameState.Meta.GameplayPhases.Tutorial && GameState.Meta.currentDay.Value == 2)
        {
            GameObject crouton = GameObject.Find("Crouton");
            DialogueTree tree = crouton.GetComponent<CroutonDialogueTrees>().BuildBerryCommotion();
            DialogueManager.Instance.StartDialogue(tree, crouton);
        }   
        
    }
}
