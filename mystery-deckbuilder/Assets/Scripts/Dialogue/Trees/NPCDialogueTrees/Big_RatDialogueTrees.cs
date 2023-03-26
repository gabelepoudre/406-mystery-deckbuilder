/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Rat Mob's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class Big_RatDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public Big_RatDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    

    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("AfterHenchmen", BuildAfterHenchmen());
        _dialogueTreeDict.Add("AfterHenchmenAndNote", BuildAfterHenchmenAndNote());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildAfterEncounterWin());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());
    }

 
    private DialogueTree BuildIntro()
    {
        return new DialogueTree(new NPCNode(new string[] { "Eh? Who're you?",
            "Look here, I'm a pretty busy guy, so unless you have a good reason,",
            "stop talking to me and go bother someone else" }));
    }

    private DialogueTree BuildAfterHenchmen()
    {
        NPCNode greeting = new(new string[] { "I hear someone has been snooping around and sticking their nose where it don't belong!",
        "Fine, you have my attention. What do you want from me?" });
        OptionNode reply = new();

        PlayerNode askLocation = new(new string[] { "Where were you on the night of the berry disapearance?", 
            "I've heard it was quiet around here."});

        NPCNode locationAnswer = new(new string[] { "Why do you care?",
            "If you have to know, I invited the gang to a far away hideout to party the night away.",
            "I thought I would reward everybody's hard work and that a night of snacks and drinks would be perfect for that", 
            "And yes, we have multiple hideouts that are connected by underground tunnels.",
            "Since the townsfolk don't like us very much, travelling by tunnel is a good way for us to remain out of sight and not bother anyone." });

        PlayerNode ok = new(new string[] { "Alright. I ave something else to ask, though." });

        PlayerNode askClay = new(new string[] { "What happened to Clay?",
            "Why can't he remember anything about that night?" });

        NPCNode clayAnswer = new(new string[] { "Who knows? Clay has always been an odd one.", 
            "He's too friendly with the townfolk I tell ya.", 
            "He's also not that bright, so he could be a hindrance to us for... certain activities.",
            "Since he's not very smart, I'm guessing he must've done something dumb and knocked himself out for the whole night." });

        (string, IDialogueNode)[] IntroReplyOptionsList = {
            ("Ask about whereabouts", askLocation),
            ("Ask about Clay", askClay)
        };
        greeting.SetNext(reply);
        reply.SetOptions(IntroReplyOptionsList);

        askClay.SetNext(clayAnswer);
        clayAnswer.SetNext(reply);

        askLocation.SetNext(locationAnswer);
        locationAnswer.SetNext(ok);
        ok.SetNext(reply);

        return new DialogueTree(greeting);
    }

    private DialogueTree BuildAfterHenchmenAndNote()
    {
        NPCNode greeting = new(new string[] { "I hear someone has been snooping around and sticking their nose where it don't belong!",
        "Fine, you have my attention. What do you want from me?" });
        OptionNode reply = new();

        PlayerNode askLocation = new(new string[] { "Where were you on the night of the berry disapearance?",
            "I've heard it was quiet around here"});

        NPCNode locationAnswer = new(new string[] { "Why do you care?",
            "If you have to know, I invited the gang to a far away hideout to party the night away.",
            "I though I would reward everybody's hard work and that a night of snacks and drinks would be perfect for that",
            "And yes, we have multiple hideouts that are connected by underground tunnels.",
            "Since the townsfolk don't like us very much, travelling by tunnel is a good way for us to remain out of sigt and not bother anyone." });

        PlayerNode notOk = new(new string[] { "This note here seems to contradict what you just told me.", 
            "What is the meaning of this" });

        NPCNode caught = new(new string[] { "Wait, where did you find that?", 
            "Tsk, I'm gonna need to chat with them all later...", 
            "So what are you going to do if I don't want to explain?" });

        EncounterNode encounter = new();

        PlayerNode askClay = new(new string[] { "What happened to Clay?",
            "Why can't he remember anything about that night?" });

        NPCNode clayAnswer = new(new string[] { "Who knows? Clay has always been an odd one.",
            "He's too friendly with the townsfolk I tell ya.",
            "He's also not that bright, so he could be a hindrance to us for... certain activities.",
            "Since he's not very smart, I'm guessing he must've done something dumb and knocked himself out for the whole night." });

        (string, IDialogueNode)[] IntroReplyOptionsList = {
            ("Ask about whereabouts", askLocation),
            ("Ask about Clay", askClay)
        };
        greeting.SetNext(reply);
        reply.SetOptions(IntroReplyOptionsList);

        askClay.SetNext(clayAnswer);
        clayAnswer.SetNext(reply);

        askLocation.SetNext(locationAnswer);
        locationAnswer.SetNext(notOk);
        notOk.SetNext(caught);
        caught.SetNext(encounter);

        return new DialogueTree(greeting);
    }

    private DialogueTree BuildAfterEncounterWin()
    {
         return new DialogueTree(new NPCNode(new string[] { "Ok, fine you got me there.", 
             "That's right, we decided to meet  up at the berry farm, then travel by tunnel.", 
             "What was that? There are no tunnel entrances at the berry farm?", 
             "Oh, uh... I mean... instead we...",
             "So we met up at the berry farm and swam along the river to get to our other hideout... Yeah that's what happened, for sure.",
             "Ok so we used the river that night, but we aren't exactly overflowing with berries like you might expect.",
             "Why don't you go talK to my son. He's always been the top of his class on the swim team, if you know what I mean",
             "You can probably find him at his bar, or sculking around neer the rail yard." }));
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        return new DialogueTree(new NPCNode(new string[] { "I really don't like nosy detectives. Stop asking me to explain." }));
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
