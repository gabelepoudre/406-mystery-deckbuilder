using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OslowDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
     private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public OslowDialogueTrees()
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
        NPCNode intro = new(new string[] {"Greetings detective. What can this old one do for you?"});
        OptionNode options = new(); //set options later
        intro.SetNext(options);

        PlayerNode askWhere = new(new string[] {"Where were you on the night of the berry disappearance?"});
        PlayerNode answerWhere = new(new string[] {"I was at home enjoying a good novel.", 
        "I know, it sounds pretty boring but I'm at that age where I just don't have the energy for high spirited activities. Plus, I do like reading"});
        askWhere.SetNext(answerWhere);
        answerWhere.SetNext(options);

        PlayerNode askRole = new(new string[] {"What is your role here in Small Pines?"});
        NPCNode answerRole = new(new string[] {"I'm just the village elder now but I guess I'm like an information keeper of sorts?", 
        "I'm old and physically not helpful, however I am knowledgeable so the townsfolk like to come and see me when they want to know something.", 
        "I also like to keep records of the town history"});
        askRole.SetNext(answerRole);
        answerRole.SetNext(options);

        PlayerNode askBerries = new(new string[] {"Clay said you might be able to help me. Do you have an idea as to who stole the berries?"});
        NPCNode answerBerries = new(new string[] {"As a keeper, I can't just give you information for free. Let's see if you can persuade me."});
        EncounterNode encounter = new();
        answerBerries.SetNext(encounter);

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
        NPCNode root = new(new string[] {"Hmmm I have a couple ideas but nothing definitive.", 
        "That night, it seems usually quiet around here so the mob may have been up to something. I did see a young  member head towards Mike's though.", 
        "Perhaps you can go there and ask him about that night. There's also something odd about Elk.", 
        "On the surface he seems like a stand-up fellow but he does some shady things behind the mayor's back.", 
        "I have overheard some of his conversations when no one else was around and I can't help but be suspicious of him."});
         DialogueTree tree = new (root);
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        DialogueTree tree = new(new NPCNode(new string[] {"Sorry, it's nothing personal. I'm sure someone else would like to help though."}));
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }

}
