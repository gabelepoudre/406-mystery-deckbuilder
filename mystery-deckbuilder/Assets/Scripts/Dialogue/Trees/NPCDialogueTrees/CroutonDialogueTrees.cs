/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        _dialogueTreeDict.Add("DialogueWithAlan", BuildDialogeWithAlan()); //evidence
        _dialogueTreeDict.Add("DialogueWithNina", BuildDialogueWithNina());
        _dialogueTreeDict.Add("DialogueWithBoth", BuildDialogueWithBoth());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildEncounterWin());

        _dialogueTreeDict.Add("BerryCommotion", BuildBerryCommotion());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildEncounterLoss());
        _dialogueTreeDict.Add("BuildRanOutOfDays", BuildRanOutOfDays());
    }


    private DialogueTree BuildRanOutOfDays()
    {
        NPCNode croutonForce = new(new string[] { "Well detective, I hope your investigation went well.", "The people of small pines demand a suspect be named." });
        ArbitraryCodeNode croutonForcePick = new(() => { this.gameObject.GetComponent<PickASuspectLauncher>().Launch(); return 0; });
        croutonForce.SetNext(croutonForcePick);

        return new DialogueTree(croutonForce);
    }

    private DialogueTree BuildIntro()
    {
        NPCNode greeting = new(new string[] { "Hello Detective I am Crouton, Mayor of Small Pines, what can I help you with today?"});
        OptionNode reply = new();

        PlayerNode askFreeTime = new(new string[] { "Are there places you like to visit on your off time Mayor?" });
        PlayerNode askWork = new(new string[] { "As Mayor of Small Pines, what exactly do you do as your work?",
            "I'm curious to know if you don't mind me asking." });

        NPCNode freeTimeAnswer = new(new string[] { "Well, before I was the Mayor I liked to visit Main Street and walk by the flower shop to smell the flowers." ,
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

        NPCNode freeTimeAnswer = new(new string[] { "Well, before I was the Mayor I liked to visit Main Street and walk by the flower shop to smell the flowers." ,
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

        NPCNode freeTimeAnswer = new(new string[] { "Well, before I was the Mayor I liked to visit Main Street and walk by the flower shop to smell the flowers." ,
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

        NPCNode freeTimeAnswer = new(new string[] { "Well, before I was the Mayor I liked to visit the Main Street and walk by the flower shop to smell the flowers." ,
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

    /** Crouton's dialogue after you beat him **/
    private DialogueTree BuildEncounterWin()
    {
         return new DialogueTree(new NPCNode(new string[] { "OKAY FINE I'll tell, but just it between us 'kay? It's Elkie, he's the one making decisions." ,
             "But please don't hate him detective, Elkie's nice to Crouton! He's got a lotta ideas and is smart! Just bad people skills."}));
    }

    private DialogueTree BuildEncounterLoss()
    {
        return new DialogueTree(new NPCNode(new string[] { "Detective the nerve, PREPAWSTEROUS! I am in charge, Mayor Crouton." ,
            "Now please enough with these baseless accusations and return to your detective work!" }));
    }

    //public because will be called by scripted event trigger
    public DialogueTree BuildBerryCommotion()
    {
        //Note: the DialogueManager wasn't setting the name to Crouton when the optional name parameter in NPCNode was null, so i manually specified Crouton for now
        NPCNode berriesGone = new(new string[] {"Oh great haymaker, please help us!", 
        "what about the festival?! What's gonna happen now?!", 
        "Who would've done something like this?!"}, name:"Crowd");
        PlayerNode excuseMe = new(new string[] {"Excuse me, but what happened here?"});
        NPCNode fishBrains = new(new string[] {"Look around you fish brain, the berries are gone!"}, name:"Crowd");
        PlayerNode woah = new(new string[] {"Woah all the berries are gone just like that, also that was rude."});
        NPCNode festival = new(new string[] {"And just before the festival too!"}, name:"Crowd");
        NPCNode cooperation = new(new string[] {"Everyone please...", 
        "I know how much the Berry Festival means to all of you, but for us to find the berries we need your cooperation and patience!"}, name:"Crouton");
        NPCNode precious = new(new string[] {"Oh Crouton dear is just too precious, I'm so glad she's our mayor!", "Ms. Crouton, are the berries gonna come back?"}, name:"Crowd");
        NPCNode comingBack = new(new string[] {"They're going to come back, don't worry! Everyone please remain calm!", 
        "We are going to require everyone's help if we are to find the berries before the Berry festival!"}, name:"Crouton");
        NPCNode quietDown = new(new string[] {"Everyone let's quiet down for Mayor Crouton!"}, name:"Crowd");
        PlayerNode bad = new(new string[] {"*This is bad, the crowd is nowhere close to calming down*"});
        NPCNode calm = new(new string[] {"Ummm.. Miss Crouton said to remain calm...", "I'M PANICKING CAN YOU TELL!?",  
        "What has happened here will be remembered for all history..."}, name:"Crowd");
        NPCNode surprise = new(new string[] {"*Visible panic and a surprised Pikachu face* Uhhh please?"}, name:"Crouton");
        NPCNode silence = new(new string[] {"SILENCE!!!"}, name:"?");
        PlayerNode loud = new(new string[] {"Woah what a loud voice. where did that come from?"});
        NPCNode bear = new(new string[] {"*Gasps* It's the Black Bear!"}, name:"Crowd");

        berriesGone.SetNext(excuseMe);
        excuseMe.SetNext(fishBrains);
        fishBrains.SetNext(woah);
        woah.SetNext(festival);
        festival.SetNext(cooperation);
        cooperation.SetNext(precious);
        precious.SetNext(comingBack);
        comingBack.SetNext(quietDown);
        quietDown.SetNext(bad);
        bad.SetNext(calm);
        calm.SetNext(surprise);
        surprise.SetNext(silence);
        silence.SetNext(loud);
        loud.SetNext(bear);


        /* I'm just gonna add this stuff here for now lol. time is of the essence */
        NPCNode elkIntro = new(new string[] {"Oh it's... you're... You're the renowned detective Glub. We are currently undergoing a bit of a crisis.", 
        "All the Saskatoon berries that were for the Berry Festival have gone missing. The town of Small Pines would really appreciate the help of such a renowned detective.",
        "Will you help us figure out this missing berry mystery?"}, name:"Elk Secretary");
        OptionNode elkOptions = new(); //set options later
        bear.SetNext(elkIntro);
        elkIntro.SetNext(elkOptions);

        NPCNode no = new(new string[] {"Are you sure? The people of Small Pines could really use your help."}, name:"Elk Secretary");
        no.SetNext(elkOptions);

        NPCNode yes = new(new string[] {"Thank you. I will pass you over to our local detective Black Bear."}, name:"Elk Secretary");


        (string, IDialogueNode) [] OptionsList = {
            ("Yes", yes),
            ("No", no)
        };

        elkOptions.SetOptions(OptionsList);

        NPCNode bearIntro = new(new string[] {"Hello detective. Let me quickly catch you up to speed on what I've found at the crime scene.", 
        "The disappearance happened last night. There was a large number of footprints which indicates a big group worked together to steal the berries.", 
        "There's evidence that the culprits escaped using the river and travelled North. And uhhh. That's all I got, sorry.", 
        "We have one week left before the berry festival, so we better act quickly"}, name:"Black Bear");
        yes.SetNext(bearIntro);

        return new DialogueTree(berriesGone);
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
