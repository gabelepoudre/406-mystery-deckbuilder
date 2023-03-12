/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of Austyn's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class AustynDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public AustynDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    

    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildAfterEncounterWin());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());
        
    }

 
    private DialogueTree BuildIntro()
    {

        NPCNode greeting = new(new string[] {"Oh hey detective.", "Is there something we could help with?"});
        OptionNode reply = new(); //set later
        greeting.SetNext(reply);

        PlayerNode askWhere = new(new string[] {"Where were you on the night of the berry disappearance?"});
        PlayerNode askRole = new(new string[] {"What is your role here in Small Pines?"});
        PlayerNode askBerries = new(new string[] {"So as a union worker, do you think the union could have stolen the berries?"});

        NPCNode whereAnswer = new(new string[] {"Oh we were home by that time. The union is really good about not forcing overtime", 
        "It's usually only the hardcore members that stay late."});
        NPCNode roleAnswer = new(new string[] {"I started at the union recenty.", "Honestly, it's been one of the best things to happen to us", 
        "It's so nice to have a stable job and a solid income."});
        EncounterNode encounter = new();


        whereAnswer.SetNext(reply);
        roleAnswer.SetNext(reply);

        askWhere.SetNext(whereAnswer);
        askRole.SetNext(roleAnswer);
        askBerries.SetNext(encounter);
        
        (string, IDialogueNode)[] replyOptionsList = {
            ("Ask about whereabouts", askWhere),
            ("Ask about role", askRole),
            ("Ask about berries", askBerries)
        };

        reply.SetOptions(replyOptionsList);

        
        return new DialogueTree(greeting);
    }

 
    
    private DialogueTree BuildAfterEncounterWin()
    {
        return new DialogueTree(new NPCNode(new string[] {"I mean...", "I guess the union has the equipment and the manpower to steal the berries, but they would never do that.", 
        "The union loves Small Pines. The would never sabotage the festival."}));
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        return new DialogueTree(new NPCNode(new string[] {"I don't know if the union would like me talking about them like this", "Really sorry..."}));
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
