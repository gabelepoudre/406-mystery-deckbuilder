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
        NPCNode explainWhere = new(new string[] {"I was definitely home by the time that took place. I don't have an alibi if that's what you're asking.",
            "I live alone and I don't particularly care to be held in account by anyone if I don't have to."});

        PlayerNode askRole = new(new string[] { "What is your role here in Small Pines?" });
        NPCNode roleAnswer = new(new string[] { "I'm the owner operator of a local vehicle and equipment rental company.",
        "Things like trucks, vans, lawnmowers, snowblowers, and generators.",
        "I feel like I mostly rent to those beaver thugs, but one of these days, I'm going to smarten up and stop renting to them." });
        
        PlayerNode askEvidence = new(new string[] {"Do you have any clients you think might be involved with the berry disappearance?"});
        EncounterNode encounter = new();
        askEvidence.SetNext(encounter);

        askWhere.SetNext(explainWhere);
        explainWhere.SetNext(introReply);

        askRole.SetNext(roleAnswer);
        roleAnswer.SetNext(introReply);

        (string, IDialogueNode)[] optionsList = {
            ("Ask whereabouts", askWhere),
            ("Ask about role", askRole),
            ("Ask about theft", askEvidence)
        };

        introReply.SetOptions(optionsList);

        return new DialogueTree(intro);
    }

    /** intro after you beat him **/
    private DialogueTree BuildAfterEncounterWin()
    {
         DialogueTree tree = new (new NPCNode(new string[] {"If anyone, it would be those beavers. My equipment always comes back from them completely trashed.",
         "Not enough to void their deposit mind you, but way more worn out than it really should be.", 
         "Those beavers are really of the mindset that they can just throw their weight around and get away with anything."}));
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        DialogueTree tree = new(new NPCNode(new string[] {"I don't really know if I should be talking about my clients behind their back like this.", 
        "It's not very professional."}));
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
