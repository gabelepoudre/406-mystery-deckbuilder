using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* All of Black bear's dialogue trees */
public class BlackBearDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public BlackBearDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary will Speck's dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
      
    }

    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Hello detective. Let me quickly catch you up to speed on what I've found at the crime scene.", 
        "The disappearance happened last night. There was a large number of footprints which indicates a big group worked together to steal the berries.", 
        "There's evidence that the culprits escaped using the river and travelled North. And uhhh. That's all I got, sorry.", 
        "We have one week left before the Berry Festival, so we better act quickly."});
        return new DialogueTree(intro);
    }

   
    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }
}
