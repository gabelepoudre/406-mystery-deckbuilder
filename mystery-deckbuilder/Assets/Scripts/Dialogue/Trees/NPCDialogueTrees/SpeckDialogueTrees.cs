using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeckDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public SpeckDialogueTrees()
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
    }

    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"What do you want? Can't you see I'm busy here?"});
        OptionNode options = new(); //set options later
        intro.SetNext(options);

        PlayerNode askWhere = new(new string[] {"Where were you on the night of the berry disappearance?"});
        NPCNode answerWhere = new(new string[] {"Me? Why do you want to know? Are all detectives so nosy?"});
        EncounterNode encounter = new();
        askWhere.SetNext(answerWhere);
        answerWhere.SetNext(encounter);

        PlayerNode askRole = new(new string[] {"What is your role here in Small Pines?"});
        NPCNode answerRole = new(new string[] {"I'm a member of the rat mob. We do various things around here, including cleaning up the town.", 
        "You know how much garbage the town gets every day?! No you don't, of course, because we do a good job cleaning the place!", 
        "And do we even get a simple thanks for our hard work? No!", "No one appreciates us, but soon they'll learn. Everyone underestimates the power of us rats."});
        askRole.SetNext(answerRole);
        answerRole.SetNext(options);

        PlayerNode askBerries = new(new string[] {"Do you have any idea as to who may have stolen the berries?"});
        NPCNode answerBerries = new(new string[] {"You wanna know what I think? I think the elk might be up to something. I have never trusted him nor the mayor...", 
        "But Crouton doesn't seem bright enough to plot something like this. I would check on the elk if I were you."});
        askBerries.SetNext(answerBerries);
        answerBerries.SetNext(options);

        (string, IDialogueNode) [] OptionsList = {
            ("Ask about Whereabouts", askWhere),
            ("Ask about Role", askRole),
            ("Ask about Berries", askBerries)
        };

        options.SetOptions(OptionsList);


        return new DialogueTree(intro);
    }

    private DialogueTree BuildAfterEncounterWin()
    {
        NPCNode root = new(new string[] {"The night of the incident... Right... I was uhhhh enjoying the night drinking with my buddy Clay.", 
        "Now if you'll excuse me I'm looking for something. (Now where did I drop this stupid thing...)"});
         DialogueTree tree = new (root);
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        DialogueTree tree = new(new NPCNode(new string[] {"Stop bothering me. I got things to do."}));
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}