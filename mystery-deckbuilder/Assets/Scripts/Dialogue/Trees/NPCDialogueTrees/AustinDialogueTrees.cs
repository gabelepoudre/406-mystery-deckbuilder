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
        _dialogueTreeDict.Add("EncounterWin", BuildEncounterWin());
        _dialogueTreeDict.Add("EncounterLoss", BuildEncounterLoss());
        
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


    private DialogueTree BuildEncounterWin()
    {
        return new DialogueTree(new NPCNode(new string[] {"I mean, I guess the union has the equipment and manpower to steal the berries,", 
        "but they would never do that.", "The union loves Small Pines. They would never sabotage the festival"}));
    }

    private DialogueTree BuildEncounterLoss()
    {
        return new DialogueTree(new NPCNode(new string[] {"I don't know that the union would like me talking about them like this.....", "Really sorry...."}));
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
