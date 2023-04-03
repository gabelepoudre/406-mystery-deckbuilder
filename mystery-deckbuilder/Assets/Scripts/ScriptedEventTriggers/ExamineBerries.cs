using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExamineBerries : MonoBehaviour
{
    //npc to start dialogue. any random one will do
    public GameObject Glub;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        //for if the player comes back to the boxcar after the event
        SetActiveIfWolverineBeaten();

        //for during the scene after the player beats wolverine and ends dialogue
        GameState.Meta.dialogueActive.OnChange += OnDialogueEndIfWolverineBeaten;
        
    }

    private void SetActiveIfWolverineBeaten()
    {
        if (GameState.NPCs.Wolverine.encountersWon.Value == 1)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnDialogueEndIfWolverineBeaten()
    {
        try {
            if (! GameState.Meta.dialogueActive.Value)
            {
                SetActiveIfWolverineBeaten();
            }
        
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueActive.OnChange -= OnDialogueEndIfWolverineBeaten;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.dialogueActive.OnChange -= OnDialogueEndIfWolverineBeaten;
        }
    }

    public void TriggerExamineBerriesDialogue()
    {
        DialogueTree tree = new(new PlayerNode(new string[] {"It's filled with berries!"}));
        DialogueManager.Instance.StartDialogue(tree, Glub);
    }

}
