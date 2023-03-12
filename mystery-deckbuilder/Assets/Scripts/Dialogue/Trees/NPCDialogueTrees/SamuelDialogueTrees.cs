/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of Samuel Snake's dialogue trees in a dictionary, the point being
 * that all of Samuel Snake's dialogue will be built here and passed to the NPC class 
 */
public class SamuelDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public SamuelDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary will Nibbles' dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildAfterEncounterWin());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());
    }

    /** intro **/
    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Hi detective.", "You've caught me between calls, so let me know if you need anything."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] {"Where were you the night of the berry disappearance?"});
        NPCNode explainWhere = new(new string[] {"Me personally, I was at home fast asleep.", "I prescribe to that work hard, sleep hard mentality.", 
        "As for our workers in general, there certainly weren't any on the clock at that hour."});
        
        PlayerNode askEvidence = new(new string[] {"There's some solid evidence stacking up against the union.", "I need you to work with me here."});
        EncounterNode encounter = new();
        askEvidence.SetNext(encounter);

        askWhere.SetNext(explainWhere);
        explainWhere.SetNext(introReply);

        (string, IDialogueNode)[] optionsList = {
            ("Ask whereabouts", askWhere),
            ("Ask about evidence", askEvidence)
        };

        introReply.SetOptions(optionsList);

        return new DialogueTree(intro);
    }

    /** intro after you beat him **/
    private DialogueTree BuildAfterEncounterWin()
    {
         DialogueTree tree = new (new NPCNode(new string[] {""}));
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        DialogueTree tree = new(new NPCNode(new string[] {"I don't really know if I should be talking about my clients behind their back like this.", 
        "It's not very professional"}));
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
