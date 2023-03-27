/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Mike's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class MikeDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public MikeDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    private DialogueTree BuildIntro()
    {

        NPCNode intro = new(new string[] {"Greetings detective, welcome to Mike's Perogies.", 
        "I'm Mike and this here is my humble perogy place."});
        PlayerNode askQuestions = new(new string[] {"Greetings sir, my name is Glub and I'm here to investigate the missing berries. Would it be alright if I asked you a couple of questions?"});
        NPCNode thankYou = new(new string[] {"Well thank you kindly detective. The Berry Festival means a good deal to us. What can I help with?"});
        OptionNode options = new();

        intro.SetNext(askQuestions);
        askQuestions.SetNext(thankYou);
        thankYou.SetNext(options);

        PlayerNode askSides = new(new string[] {"Well as far as I can remember, the Northern side of town has always been the rodent's side of town while the rest of the animals live on the Southern side", 
        "However, to be honest with you detective, the relationship between the North and South has been geting worse and worse."});
        PlayerNode askBerries = new(new string[] {"Do you have any possible ideas as to who might've taken the berries?"});
        EncounterNode encounter = new();
        askBerries.SetNext(encounter);

        askSides.SetNext(options);

        (string, IDialogueNode) [] optionsList = {
            ("Ask about city", askSides),
            ("Ask about berries", askBerries)
        };

        options.SetOptions(optionsList);

        return new DialogueTree(intro);
    }

    

    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildAfterEncounterWin());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());
    }

 
     private DialogueTree BuildAfterEncounterWin()
    {
        NPCNode root = new(new string[] {"Well, I don't have any suspects at the moment...", 
        "But as far as I know Mrs. Crouton will be trying her darndest job to find them messing berries, she sure do love them berry festivals!", 
        "The townsfolk sure did do a great job pickin' em folks to lead 'em folks.", 
        "Next time you do see Mrs. Crouton, make sure to tell her that her next meal here is on the house!"});
         DialogueTree tree = new (root);
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        NPCNode root = new(new string[] {"Well I do apologize sir, but that there isn't really my area of expertise. I can't say for certain who might've taken the berries."});
        DialogueTree tree = new(root);
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}

