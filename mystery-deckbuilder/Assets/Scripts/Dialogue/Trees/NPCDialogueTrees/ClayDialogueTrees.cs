using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
     private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public ClayDialogueTrees()
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
        NPCNode intro = new(new string[] {"Ah, so you're the 'fish' I keep hearing about. The so-called water animal?", 
        "The name's Clay. So what can I do for you, detective?"});
        OptionNode options = new(); //set options later
        intro.SetNext(options);

        PlayerNode askWhere = new(new string[] {"Where were you on the night of the berry disappearance?"});
        NPCNode answerWhere = new(new string[] {"Umm I think I was at home?", 
        "Wait a sec, I forgot what I just said. I don't feel like telling you where I was."});
        EncounterNode encounter = new();
        askWhere.SetNext(answerWhere);
        answerWhere.SetNext(encounter);

        PlayerNode askRole = new(new string[] {"What is your role here in Small Pines?"});
        NPCNode answerRole = new(new string[] {"My job is to act as an intermediary between the rat mob and the rest of Small Pines.", 
        "Everyone in town tries to avoid the other rats but they don't seem to mind me for some reason.", "That makes me perfect for the job!"});
        askRole.SetNext(answerRole);
        answerRole.SetNext(options);

        PlayerNode askBerries = new(new string[] {"Do you have any idea as to who may have stolen the berries?"});
        NPCNode answerBerries = new(new string[] {"I have no clue, but I do know two folks who are worth investigating.", 
        "There's this old-timer at our hideout and he's the most knowledgeable rat around. Plus he's very smart, unlike me.", 
        "I always go to him for advice so maybe he can help you too? That's the elk secretary.", 
        "He seems very capable but something about him seems to rub me the wrong way. It might be worth it to check in on him"});
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
        NPCNode root = new(new string[] {"Hah..... So uhhh this is a bit embarassing to say but I slept through the entire night.", 
        "We rats are night animals but for some reason I was very sleepy. This usually doesn't happen.", 
        "All I remember is being at Mike's and enjoying some perogies that day.", 
        "I recall waling out of the restaurant but that's where my memories end.", 
        "Next thing I knew, it was next morning and I was back at our hideout. It was very strange...."});
         DialogueTree tree = new (root);
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        DialogueTree tree = new(new NPCNode(new string[] {"Where I was is not important. If you must know, I was in Small Pines. Ok. net question."}));
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }

}
