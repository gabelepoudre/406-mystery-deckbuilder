/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Rat Prince's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class Rat_PrinceDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public Rat_PrinceDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    

    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("IntroAfterEncounter", BuildIntroAfterEncounter());
    }

 
    private DialogueTree BuildIntro()
    {
        
        return new DialogueTree(null);
    }

 
    private DialogueTree BuildIntroAfterEncounter()
    {
         return new DialogueTree(null);
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
