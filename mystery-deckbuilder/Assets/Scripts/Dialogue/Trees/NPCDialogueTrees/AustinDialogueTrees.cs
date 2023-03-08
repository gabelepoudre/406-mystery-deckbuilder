/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of Austin's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class AustinDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public AustinDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    

    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("Act2", BuildAct2Dialogue());
        _dialogueTreeDict.Add("Act2EncounterWin", BuildAct2EncounterWin());
        _dialogueTreeDict.Add("Act2EncounterLoss", BuildAct2EncounterLoss());
        
    }

 
    private DialogueTree BuildIntro()
    {

        NPCNode greeting = new(new string[] {"Hi there detective. Is there anything you need a hand with?"});
        OptionNode reply = new(); //set later
        greeting.SetNext(reply);

        PlayerNode askHarvest = new(new string[] {"How's the berry harvest this year?"});
        PlayerNode askSunset = new(new string[] {"I've heard the sunset is really nice from here."});

        NPCNode harvestAnswer = new(new string[] {"We've had a really good turnout this year actually.", "The berry festival should be amazing."});
        NPCNode sunsetAnswer = new(new string[] {"Oh yeah it is.", "My boyfriend works for the beaver union and we always meet up here to watch the sunset before we go home.", 
        "Also, it turns out you don't have to be a beaver to join the union.", "Guess that must have been a rumour, ha ha."});
        harvestAnswer.SetNext(reply);
        sunsetAnswer.SetNext(reply);

        askHarvest.SetNext(harvestAnswer);
        askSunset.SetNext(sunsetAnswer);
        
        (string, IDialogueNode)[] replyOptionsList = {
            ("Ask about harvest", askHarvest),
            ("Ask about sunset", askSunset)
        };

        reply.SetOptions(replyOptionsList);

        
        return new DialogueTree(greeting);
    }

 
    private DialogueTree BuildAct2Dialogue()
    {

        NPCNode intro = new(new string[] {"Oh hey detective. Is there something we could help with?"});
        OptionNode reply = new(); //set later
        intro.SetNext(reply);

        PlayerNode askDisappearance = new(new string[] {"Where were you on the night of the berry disappearance?"});
        PlayerNode askRole = new(new string[] {"What is your role here in Small Pines?"});
        PlayerNode askBerries = new(new string[] {"So as a union worker, do you think the union could have stolen the berries?"});

        NPCNode explainDisappearance = new(new string[] {"Oh we were home by that time.", "The union is really good about not forcing overtime.", 
        "It's usually only the hardcore members that stay up late."});
        NPCNode explainRole = new(new string[] {"I started at the union recently.", "Honestly it's been one of the best things to happen to us.", 
        "It's so nice to have a stable job and a solid income"});
        explainDisappearance.SetNext(reply);
        explainRole.SetNext(reply);

        EncounterNode encounter = new();

        askDisappearance.SetNext(explainDisappearance);
        askRole.SetNext(explainRole);
        askBerries.SetNext(encounter);

        (string, IDialogueNode)[] optionsList = {
            ("Ask where he was", askDisappearance),
            ("Ask role", askRole)
        };

        reply.SetOptions(optionsList);

         return new DialogueTree(intro);
    }

    private DialogueTree BuildAct2EncounterWin()
    {
        return new DialogueTree(new NPCNode(new string[] {"I mean, I guess the union has the equipment and manpower to steal the berries,", 
        "but they would never do that.", "The union loves Small Pines. They would never sabotage the festival"}));
    }

    private DialogueTree BuildAct2EncounterLoss()
    {
        return new DialogueTree(new NPCNode(new string[] {"I don't know that the union would like me talking about them like this.....", "Really sorry...."}));
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
