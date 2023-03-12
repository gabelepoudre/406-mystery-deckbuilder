using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* All of Black bear's dialogue trees */
public class BlackBearDialogueTrees : MonoBehaviour
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public BlackBearDialogueTrees()
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
        //TODO: implement
        return new DialogueTree(null);
    }

 
    private DialogueTree BuildIntroAfterEncounter()
    {
        //TODO: implement
         return new DialogueTree(null);
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }

}
