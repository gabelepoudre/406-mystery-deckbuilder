using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolverineDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public WolverineDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary will Speck's dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildAfterEncounterWin());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());
        _dialogueTreeDict.Add("Bar", BuildBar());
    }

    private DialogueTree BuildIntro()
    {
        
        PlayerNode root = new(new string[] {"....."});
        OptionNode options = new(); //set options later
        root.SetNext(options);

        PlayerNode askWhere = new(new string[] {"Where were you on the night of the berry disappearance?"});
        PlayerNode answerWhere = new(new string[] {"......"});
        askWhere.SetNext(answerWhere);
        answerWhere.SetNext(options);
       

        PlayerNode askRole = new(new string[] {"What is your role here in Small Pines?"});
        NPCNode answerRole = new(new string[] {"......"});
        askRole.SetNext(answerRole);
        answerRole.SetNext(options);

        PlayerNode convince = new(new string[] {"What is it going to take to convince you?"});
        EncounterNode encounter = new();
        convince.SetNext(encounter);


        (string, IDialogueNode) [] OptionsList = {
            ("Ask about Whereabouts", askWhere),
            ("Ask about Role", askRole),
            ("Ask what convince", convince)
        };

        options.SetOptions(OptionsList);


        return new DialogueTree(root);
    }

    private DialogueTree BuildAfterEncounterWin()
    {
        NPCNode root = new(new string[] {"Fine, do what you will."});
         DialogueTree tree = new (root);
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        DialogueTree tree = new(new NPCNode(new string[] {"I am unconvinced."}));
        return tree;
    }

    private DialogueTree BuildBar()
    {
        DialogueTree tree = new(new NPCNode(new string[] {"...."}));
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }
}