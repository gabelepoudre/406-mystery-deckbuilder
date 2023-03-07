/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Elk Secretary's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class Elk_SecretaryDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public Elk_SecretaryDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary will Nibbles' dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("IntroAfterEncounter", BuildIntroAfterEncounter());
    }

    /** Nibbles' intro **/
    private DialogueTree BuildIntro()
    {
        
        return new DialogueTree(null);
    }

    /** Nibbles' intro after you beat him **/
    private DialogueTree BuildIntroAfterEncounter()
    {
         return new DialogueTree(null);
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
