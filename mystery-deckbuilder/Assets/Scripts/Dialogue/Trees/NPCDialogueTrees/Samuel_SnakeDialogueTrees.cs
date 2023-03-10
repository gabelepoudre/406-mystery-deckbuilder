/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of Samuel Snake's dialogue trees in a dictionary, the point being
 * that all of Samuel Snake's dialogue will be built here and passed to the NPC class 
 */
public class Samuel_SnakeDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public Samuel_SnakeDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary will Nibbles' dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("IntroAfterEncounterWin", BuildIntroAfterEncounterWin());
        _dialogueTreeDict.Add("IntroAfterEncounterLoss", BuildIntroAfterEncounterLoss());
    }

    /** intro **/
    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Hi detective.", "You've caught me between calls, so let me know if you need anything."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] {"Where were you on the night of the berry disappearance?"});
        PlayerNode askRole = new(new string[] {"What is your role here in Small Pines?"});
        PlayerNode askTheft = new(new string[] {"Do you have any client you think might be involved with the berry disappearance?"});
        
        NPCNode explainWhere = new(new string[] {"I was definitely home by the time that was taking place.", 
        "I don't have an alibi if that's what you're asking.", "I live alone and don't particularly care to be held in account by anyone if I don't want to."});
        NPCNode explainRole = new(new string[] {"I'm the owner and operator of a local vehicle and equipment rental company.", 
        "Things like trucks, vans, lawnmowers, snowblowers, and generators.", "I feel like I mostly rent to those beaver thugs.", 
        "One of these days I'm going to smarten up and stop renting to them."});
        EncounterNode encounter = new();

        askWhere.SetNext(explainWhere);
        askRole.SetNext(explainRole);
        askTheft.SetNext(encounter);

        explainWhere.SetNext(introReply);
        explainRole.SetNext(introReply);

        (string, IDialogueNode) [] IntroReplyOptionsList = {
            ("Ask Whereabouts", askWhere),
            ("Ask about role", askRole),
            ("Ask about berries", askTheft)

        };

        introReply.SetOptions(IntroReplyOptionsList);

    
        return new DialogueTree(intro);
    }

    /** intro after you beat him **/
    private DialogueTree BuildIntroAfterEncounterWin()
    {
         DialogueTree tree = new (new NPCNode(new string[] {"If anyone it would be those beavers.", "My equipment always comes back from them completely trashed.", 
         "Not to void their deposit, mind you, but way more worn out than it should really be.", 
         "Those beavers are really of the mindset that they can just throw their weight around and get away with anything."}));
        return tree;
    }

    private DialogueTree BuildIntroAfterEncounterLoss()
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
