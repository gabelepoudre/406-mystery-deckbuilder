/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Alan's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class AlanDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
   private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public AlanDialogueTrees()
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

    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Greetings Detective Glub, welcome to the post office sir. You can call me Alan.", 
        "What can I help you with today, sir?"});
        PlayerNode reply = new(new string[] {"Hello, Alan. It's a pleasure to meet you.", 
        "I'm asking around town to find out more about the missing berries."});
        NPCNode thanks = new(new string[] {"Well thank you very much for your hard work, detective!", 
        "Here in Small Pines the Berry festival is very important to us so we very much appreciate the help, sir!", 
        "We are a tight-knit community here and I'd love to help out"});
        OptionNode options = new();
        PlayerNode askBerries = new(new string[] {"Alright well I hope you don't mind me asking...", 
        "But where were you on the day that the berries disappeared?"});
        NPCNode answerBerries = new(new string[] {"Just here doing my job, sir,", 
        "delivering everyone's hard earned mail and making sure the mail is properly delivered, sir!"});
        PlayerNode askSuspicious = new(new string[] {"Anything suspicious happened in the past couple of weeks and possibly even a suspect in your mind?"});
        EncounterNode encounter = new();

        (string, IDialogueNode)[] OptionsList = {
            ("Ask about berries", askBerries),
            ("Ask about suspects", askSuspicious)
        };
        options.SetOptions(OptionsList);

        intro.SetNext(reply);
        reply.SetNext(thanks);
        thanks.SetNext(options);
        askBerries.SetNext(answerBerries);
        answerBerries.SetNext(options);
        askSuspicious.SetNext(encounter);    

        return new DialogueTree(intro);
    }

    
    private DialogueTree BuildAfterEncounterWin()
    {
        NPCNode root = new(new string[] {"Well...", "The Town Hall has been receiving a large amount of mysterious mail that are pretty thick if I do say so myself sir.", 
        "I'm pretty sure they were mailed directly for the Town Hall.", "If I was to pick a suspect for the missing berries, then I'd say it might be the Elk Secretary since everyone loves Mayor Crouton...", 
        "But to be honest with you sir, the Elk Secretary isn't really liked by most people.", 
        "However, that is all that I can think of at the moment sir I do apologize. I hope I could've been of more service."});
         DialogueTree tree = new (root);
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        DialogueTree tree = new(new NPCNode(new string[] {"Um, nothing really out of the ordinary that I can think of right now sir, but I do apologize for not being helpful to your cause, sir."}));
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
