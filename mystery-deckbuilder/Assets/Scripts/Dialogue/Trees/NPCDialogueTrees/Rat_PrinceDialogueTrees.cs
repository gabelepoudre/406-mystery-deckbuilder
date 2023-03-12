/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Rat Prince's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class Rat_PrinceDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public Rat_PrinceDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    

    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Confession", BuildConfession());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildAfterEncounterWin());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());

        _dialogueTreeDict.Add("Bar", BuildBar());
        _dialogueTreeDict.Add("BoxCar", BuildBoxCar());
    }

    private DialogueTree BuildBar()
    {
        NPCNode root = new(new string[] {"Detective, don't you know who I am? I am the Rat Prince. The Heir of the true rulers of this town.", 
        "Don't bother me again if you know what's good for you"});
        return new DialogueTree(root);
    }

    private DialogueTree BuildBoxCar()
    {
        NPCNode root = new(new string[] {"Well well well... Fancy meeting you here, detective. Unfortunately we can't let you look in that boxcar.", 
        "We are um... doing our own private investigate right now and we wouldn't want any of the evidence to be compromised, now would we?", 
        "If you have a problem with that, why don't you take that up with my associate here. Heh. Good luck..."});
        return new DialogueTree(root);
    }

    

 
    private DialogueTree BuildConfession()
    {
        NPCNode root = new(new string[] {"Oh, detective. It would seem you have an unexpectedly powerful way with words.", 
        "My associate is typically not terribly compliant with law enforcement professionals such as yourself.", 
        "I suppose I can chat with you now, if you really need something."});
        PlayerNode evidence = new(new string[] {"The evidence is really not looking good for you.", 
        "If you want to tell your side of the story, now would be the time to do so."});
        EncounterNode encounter = new();

        root.SetNext(evidence);
        evidence.SetNext(encounter);

        return new DialogueTree(root);
    }

 
    private DialogueTree BuildAfterEncounterWin()
    {
        NPCNode root = new(new string[] {"Fine, ask me what you want to know and I'll answer honestly."});
        OptionNode options = new(); //set options later
        root.SetNext(options);

        PlayerNode askPullItOff = new(new string[] {"How did you pull it off?"});
        NPCNode explainPullItOff = new(new string[] {"It wasn't so hard, really.", 
        "Plant the idea in my father's brain, convince him to go through with it, and then stack the team with rats that I knew were loyal to me over my father.", 
        "It's crazy what people will do when they think it's their own original idea."});
        askPullItOff.SetNext(explainPullItOff);
        explainPullItOff.SetNext(options);

        PlayerNode askWhy = new(new string[] {"Why do something like this?"});
        NPCNode explainWhy = new(new string[] {"I was raised with the knowledge that one day I would succeed my old man and become the leader of the rat mob.", 
        "My father knew this and got me all the training I needed, but at some point I think his age started catching up to him.", 
        "In his mind I became the symbol of his aging. His time as leader coming to an end", 
        "He became distant, and, dare I say, disrespectful.", 
        "And the one thing my father always said was 'never let anyone disrespect you'.", 
        "I suppose I did it to get back at him, but also to show him that he has a competent heir.", 
        "You know, a living legacy and all that. Thinking about it now, I suppose I did it because I love him"});
        askWhy.SetNext(explainWhy);
        explainWhy.SetNext(options);

        PlayerNode askWhatHappens = new(new string[] {"So what happens now?"});
        NPCNode explainWhatHappens = new(new string[] {"Well I suppose I'll have to try agian in 12 months when I get out on a suspiciously early parole.", 
        "Ha ha ha ha..."});
        askWhatHappens.SetNext(explainWhatHappens);
        explainWhatHappens.SetNext(options);

        (string, IDialogueNode) [] optionsList = {
            ("Ask how", askPullItOff),
            ("Ask why", askWhy),
            ("Ask what next", askWhatHappens)
        };

        options.SetOptions(optionsList);

        return new DialogueTree(root);
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
         return new DialogueTree(new NPCNode(new string[] {"Nice try, but no thanks."}));
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
