/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Crouton's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class CroutonDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public CroutonDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    
    private void BuildTreeDictionary()
    {
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("BuildDialogeWithAlan", BuildDialogeWithAlan());
        _dialogueTreeDict.Add("BuildDialogueWithNina", BuildDialogueWithNina());
        _dialogueTreeDict.Add("BuildDialogueWithBoth", BuildDialogueWithBoth());
        _dialogueTreeDict.Add("IntroAfterEncounter", BuildIntroAfterEncounter());
        _dialogueTreeDict.Add("BuildIntroLoss", BuildIntroLoss());
    }


    private DialogueTree BuildIntro()
    {
        NPCNode greeting = new(new string[] { "Hello Detective I am Crouton, Mayor of Small Pines, what can I help you with today?"});
        OptionNode reply = new();

        PlayerNode askFreeTime = new(new string[] { "Are there places you like to visit on your off time Mayor?" });
        PlayerNode askWork = new(new string[] { "As Mayor of Small Pines, what exactly do you do as your work?",
            "I'm curious to know if you don't mind me asking." });

        NPCNode freeTimeAnswer = new(new string[] { "Well, before I was the Mayor I liked to visit the main street and walk by the flower shop to smell the flowers." ,
            "I also enjoyed exploring the rail yard and the lumberyard! But ever since I've become the town mayor, I haven't really had that much free time." ,
            "When I do get free time I visit the Hay Bale shrine in the Town Hall's basement and roll around on hay!" });

        NPCNode workAnswer = new(new string[] { "Woof?" , "Okay well usually I attend meetings so I can listen to people talk." ,
            "I also greet people and listen to people talk about stuff.", " I also go to the office to listen to him talk and stuff and then I go to eat treats and drink milk!" });

        (string, IDialogueNode)[] replyOptionsList = {
            ("Ask about free time", askFreeTime),
            ("Ask about work", askWork)
        };

        greeting.SetNext(reply);

        askFreeTime.SetNext(freeTimeAnswer);
        freeTimeAnswer.SetNext(reply);

        askWork.SetNext(workAnswer);
        workAnswer.SetNext(reply);

        reply.SetOptions(replyOptionsList);
        

        return new DialogueTree(greeting);
    }

    private DialogueTree BuildDialogeWithAlan()
    {
        NPCNode greeting = new(new string[] { "Hello Detective I am Crouton, Mayor of Small Pines, what can I help you with today?" });
        OptionNode reply = new();

        PlayerNode askFreeTime = new(new string[] { "Are there places you like to visit on your off time Mayor?" });
        PlayerNode askWork = new(new string[] { "As Mayor of Small Pines, what exactly do you do as your work?",
            "I'm curious to know if you don't mind me asking." });

        NPCNode freeTimeAnswer = new(new string[] { "Well, before I was the Mayor I liked to visit the main street and walk by the flower shop to smell the flowers." ,
            "I also enjoyed exploring the rail yard and the lumberyard! But ever since I've become the town mayor, I haven't really had that much free time." ,
            "When I do get free time I visit the Hay Bale shrine in the Town Hall's basement and roll around on hay!" });

        NPCNode workAnswer = new(new string[] { "Woof?" , "Okay well usually I attend meetings so I can listen to people talk." ,
            "I also greet people and listen to people talk about stuff.", " I also go to the office to listen to him talk and stuff and then I go to eat treats and drink milk!" });

        PlayerNode askAlan = new(new string[] { "Mayor Crouton, it's been reported that the political group has been receiving mysterious thick mail for the past week, tell me what you know about it." });

        NPCNode alanAnswer = new(new string[] { "Huh, what mails? OH, the ones Elkie's been getting! Yeah I know thoh, um I mean. NOPE we haven't received any mails, not here Glubby ol pal." });

        PlayerNode askAlan2 = new(new string[] { "Miss Crouton, you're not being honest with me are you?" });

        NPCNode alanAnswer2 = new(new string[] { "Uh-uh-uh-um Detective what are you talking about?" });

        PlayerNode askAlan3 = new(new string[] { "Tell me Ms. Crouton, is the Elk secretary receiving mysterious mails or not."});

        NPCNode alanAnswer3 = new(new string[] { "OKAY fiiiiine I'll tell, Elkie has been receiving mysterious mails that he won't show Crouton." ,
            "But I think they're not bad, they're probs just filled with treats! WAIT, or maybe they're LOVE LETTERS OMIGAHD!" });

        askAlan.SetNext(alanAnswer);
        alanAnswer.SetNext(askAlan2);
        askAlan2.SetNext(alanAnswer2);
        alanAnswer2.SetNext(askAlan3);
        askAlan3.SetNext(alanAnswer3);
        alanAnswer3.SetNext(reply);

        (string, IDialogueNode)[] replyOptionsList = {
            ("Ask about free time", askFreeTime),
            ("Ask about work", askWork),
            ("Ask about mail", askAlan)
        };

        greeting.SetNext(reply);

        askFreeTime.SetNext(freeTimeAnswer);
        freeTimeAnswer.SetNext(reply);

        askWork.SetNext(workAnswer);
        workAnswer.SetNext(reply);

        reply.SetOptions(replyOptionsList);


        return new DialogueTree(greeting);
    }

    private DialogueTree BuildDialogueWithNina()
    {
        NPCNode greeting = new(new string[] { "Hello Detective I am Crouton, Mayor of Small Pines, what can I help you with today?" });
        OptionNode reply = new();

        PlayerNode askFreeTime = new(new string[] { "Are there places you like to visit on your off time Mayor?" });
        PlayerNode askWork = new(new string[] { "As Mayor of Small Pines, what exactly do you do as your work?",
            "I'm curious to know if you don't mind me asking." });

        NPCNode freeTimeAnswer = new(new string[] { "Well, before I was the Mayor I liked to visit the main street and walk by the flower shop to smell the flowers." ,
            "I also enjoyed exploring the rail yard and the lumberyard! But ever since I've become the town mayor, I haven't really had that much free time." ,
            "When I do get free time I visit the Hay Bale shrine in the Town Hall's basement and roll around on hay!" });

        NPCNode workAnswer = new(new string[] { "Woof?" , "Okay well usually I attend meetings so I can listen to people talk." ,
            "I also greet people and listen to people talk about stuff.", " I also go to the office to listen to him talk and stuff and then I go to eat treats and drink milk!" });

        PlayerNode askNina = new(new string[] { "Mayor Crouton I've met with you're younger sister Nina, she told me that none of your plans ever gets approved.", "Also she wants to play" });

        NPCNode ninaAnswer = new(new string[] { "YOU MET NINA!? SHE'S GREAT ISN'T SHE! I WANNA PLA-" ,
            "er, I mean, the rejection of my plans is wholeheartedly reasonable since I failed to take into account everyone's best interest" });

        PlayerNode askNina2 = new(new string[] { "Miss Crouton, is that what the Elk Secretary told you to say?" });

        NPCNode ninaAnswer2 = new(new string[] { "Yup! Nope, not once" });

        PlayerNode askNina3 = new(new string[] { "I can tell that you are only acting the part of a Mayor so be truthful with me, who's really in charge?" });

        EncounterNode encounter = new();

        askNina.SetNext(ninaAnswer);
        ninaAnswer.SetNext(askNina2);
        askNina2.SetNext(ninaAnswer2);
        ninaAnswer2.SetNext(askNina3);
        askNina3.SetNext(encounter);

        (string, IDialogueNode)[] replyOptionsList = {
            ("Ask about free time", askFreeTime),
            ("Ask about work", askWork),
            ("Ask about Nina", askNina)
        };

        

        greeting.SetNext(reply);

        askFreeTime.SetNext(freeTimeAnswer);
        freeTimeAnswer.SetNext(reply);

        askWork.SetNext(workAnswer);
        workAnswer.SetNext(reply);

        reply.SetOptions(replyOptionsList);


        return new DialogueTree(greeting);
    }

    private DialogueTree BuildDialogueWithBoth()
    {
        NPCNode greeting = new(new string[] { "Hello Detective I am Crouton, Mayor of Small Pines, what can I help you with today?" });
        OptionNode reply = new();

        PlayerNode askFreeTime = new(new string[] { "Are there places you like to visit on your off time Mayor?" });
        PlayerNode askWork = new(new string[] { "As Mayor of Small Pines, what exactly do you do as your work?",
            "I'm curious to know if you don't mind me asking." });

        NPCNode freeTimeAnswer = new(new string[] { "Well, before I was the Mayor I liked to visit the main street and walk by the flower shop to smell the flowers." ,
            "I also enjoyed exploring the rail yard and the lumberyard! But ever since I've become the town mayor, I haven't really had that much free time." ,
            "When I do get free time I visit the Hay Bale shrine in the Town Hall's basement and roll around on hay!" });

        NPCNode workAnswer = new(new string[] { "Woof?" , "Okay well usually I attend meetings so I can listen to people talk." ,
            "I also greet people and listen to people talk about stuff.", " I also go to the office to listen to him talk and stuff and then I go to eat treats and drink milk!" });

        PlayerNode askNina = new(new string[] { "Mayor Crouton I've met with you're younger sister Nina, she told me that none of your plans ever gets approved.", "Also she wants to play" });

        NPCNode ninaAnswer = new(new string[] { "YOU MET NINA!? SHE'S GREAT ISN'T SHE! I WANNA PLA-" ,
            "er, I mean, the rejection of my plans is wholeheartedly reasonable since I failed to take into account everyone's best interest" });

        PlayerNode askNina2 = new(new string[] { "Miss Crouton, is that what the Elk Secretary told you to say?" });

        NPCNode ninaAnswer2 = new(new string[] { "Yup! Nope, not once" });

        PlayerNode askNina3 = new(new string[] { "I can tell that you are only acting the part of a Mayor so be truthful with me, who's really in charge?" });

        EncounterNode encounter = new();

        askNina.SetNext(ninaAnswer);
        ninaAnswer.SetNext(askNina2);
        askNina2.SetNext(ninaAnswer2);
        ninaAnswer2.SetNext(askNina3);
        askNina3.SetNext(encounter);

        PlayerNode askAlan = new(new string[] { "Mayor Crouton, it's been reported that the political group has been receiving mysterious thick mail for the past week, tell me what you know about it." });

        NPCNode alanAnswer = new(new string[] { "Huh, what mails? OH, the ones Elkie's been getting! Yeah I know thoh, um I mean. NOPE we haven't received any mails, not here Glubby ol pal." });

        PlayerNode askAlan2 = new(new string[] { "Miss Crouton, you're not being honest with me are you?" });

        NPCNode alanAnswer2 = new(new string[] { "Uh-uh-uh-um Detective what are you talking about?" });

        PlayerNode askAlan3 = new(new string[] { "Tell me Ms. Crouton, is the Elk secretary receiving mysterious mails or not." });

        NPCNode alanAnswer3 = new(new string[] { "OKAY fiiiiine I'll tell, Elkie has been receiving mysterious mails that he won't show Crouton." ,
            "But I think they're not bad, they're probs just filled with treats! WAIT, or maybe they're LOVE LETTERS OMIGAHD!" });

        askAlan.SetNext(alanAnswer);
        alanAnswer.SetNext(askAlan2);
        askAlan2.SetNext(alanAnswer2);
        alanAnswer2.SetNext(askAlan3);
        askAlan3.SetNext(alanAnswer3);
        alanAnswer3.SetNext(reply);

        (string, IDialogueNode)[] replyOptionsList = {
            ("Ask about free time", askFreeTime),
            ("Ask about work", askWork),
            ("Ask about mail", askAlan),
            ("Ask about Nina", askNina)
        };



        greeting.SetNext(reply);

        askFreeTime.SetNext(freeTimeAnswer);
        freeTimeAnswer.SetNext(reply);

        askWork.SetNext(workAnswer);
        workAnswer.SetNext(reply);

        reply.SetOptions(replyOptionsList);


        return new DialogueTree(greeting);
    }

    /** Nibbles' intro after you beat him **/
    private DialogueTree BuildIntroAfterEncounter()
    {
         return new DialogueTree(new NPCNode(new string[] { "OKAY FINE I'll tell, but just it between us 'kay? It's Elkie, he's the one making decisions." ,
             "But please don't hate him detective, Elkie's nice to Crouton! He's got a lotta ideas and is smart! Just bad people skills."}));
    }

    private DialogueTree BuildIntroLoss()
    {
        return new DialogueTree(new NPCNode(new string[] { "Detective the nerve, PREPAWSTEROUS! I am in charge, Mayor Crouton." ,
            "Now please enough with these baseless accusations and return to your detective work!" }));
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
