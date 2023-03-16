/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Elk Secretary's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class DougDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public DougDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    

    private void BuildTreeDictionary()
    {
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("IntroMetAustynOnly", BuildIntroMetAustyn());
        _dialogueTreeDict.Add("IntroMetMarkOnly", BuildIntroMetMark());
        _dialogueTreeDict.Add("BuildIntroMetSamuelOnly", BuildIntroMetSamuel());
        _dialogueTreeDict.Add("BuildIntroMetAustynAndSamuel", BuildIntroMetAustynAndSamuel());
        _dialogueTreeDict.Add("BuildIntroMetMarkAndSamuel", BuildIntroMetMarkAndSamuel());
        _dialogueTreeDict.Add("IntroMetAustynAndMark", BuildIntroMetAustynAndMark());
        _dialogueTreeDict.Add("BuildIntroMetAllThree", BuildIntroMetAllThree());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());
        _dialogueTreeDict.Add("AfterEncounterWinFirstMainSuspect", BuildAfterEncounterWinFirstMainSuspect());
        _dialogueTreeDict.Add("AfterEncounterWinSecondOrThirdSuspect", BuildAfterEncounterWinSecondOrThirdMainSuspect());
    }

 
    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Hello detective. I'm sure you have many questions, but I have some things to tell you first.", 
        "The Small Pines Beaver Lumber Union reserves the right to have a single person take on the role of representing the union and its interests in the public sphere.", 
        "I'm Doug Beaver the Beaver Union union rep, and I'm that person.", "So unless you have a warrant to arrest a specific person, I'm the guy you're stuck with."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] {"Where were you on the night of the berry disappearance?"});
        PlayerNode askTheft = new(new string[] {"Do you have any client you think might be involved with the berry disappearance?"});
        
        NPCNode explainWhere = new(new string[] {"Me personally, I was at home fast asleep.", "I prescribe to that work hard, sleep hard mentality.", 
        "As for our workers in general, there certainly weren't any on the clock at that hour."});

        EncounterNode encounter = new();

        askWhere.SetNext(explainWhere);
        askTheft.SetNext(encounter);

        explainWhere.SetNext(introReply);

        (string, IDialogueNode) [] IntroReplyOptionsList = {
            ("Ask Whereabouts", askWhere),
            ("Ask about berries", askTheft)

        };

        introReply.SetOptions(IntroReplyOptionsList);
        
        return new DialogueTree(intro);
    }

     private DialogueTree BuildIntroMetAustyn()
    {
        NPCNode intro = new(new string[] {"Hello detective. I'm sure you have many questions, but I have some things to tell you first.", 
        "The Small Pines Beaver Lumber Union reserves the right to have a single person take on the role of representing the union and its interests in the public sphere.", 
        "I'm Doug Beaver the Beaver Union union rep, and I'm that person.", "So unless you have a warrant to arrest a specific person, I'm the guy you're stuck with."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] {"Where were you the night of the berry disappearance?"});
        PlayerNode askUnion = new(new string[] {"I have a reliable source that the beaver union has the kind of equipment and manpower needed to carry out this kind of berry stealing operation."});

        NPCNode explainWhere = new(new string[] {"Me personally, I was at home fast asleep.", "I prescribe to that work hard, sleep hard mentality.", 
        "As for our workers in general, there certainly weren't any on the clock at that hour."});
        NPCNode explainUnion = new(new string[] {"Sure detective, we aren't exactly hiding it.", "Anyone with eyes could tell you we have a number of boats and a decent sized workforce.", 
        "But, it's not exactly like we're the only group with those kind of resources in the region."});

        PlayerNode askEvidence = new(new string[] {"There's some solid evidence stacking up against the union.", "I need you to work with me here."});
        EncounterNode encounter = new();
        askEvidence.SetNext(encounter);

        askWhere.SetNext(explainWhere);
        explainWhere.SetNext(introReply);

        askUnion.SetNext(explainUnion);
        explainUnion.SetNext(introReply);

        (string, IDialogueNode)[] optionsList = {
            ("Ask whereabouts", askWhere),
            ("Ask about union", askUnion),
            ("Ask about evidence", askEvidence)
        };

        introReply.SetOptions(optionsList);
        
        return new DialogueTree(intro);
    }

     /** intro **/
    private DialogueTree BuildIntroMetMark()
    {
        NPCNode intro = new(new string[] {"Hello detective. I'm sure you have many questions, but I have some things to tell you first.", 
        "The Small Pines Beaver Lumber Union reserves the right to have a single person take on the role of representing the union and its interests in the public sphere.", 
        "I'm Doug Beaver the Beaver Union union rep, and I'm that person.", "So unless you have a warrant to arrest a specific person, I'm the guy you're stuck with."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] {"Where were you the night of the berry disappearance?"});
        PlayerNode askFestival = new(new string[] {"There are some people in town who think that you guys are upset at the berry festival for undercutting the union's harvest festival.", 
        "Do you have anything to say on that matter?"});

        NPCNode explainWhere = new(new string[] {"Me personally, I was at home fast asleep.", "I prescribe to that work hard, sleep hard mentality.", 
        "As for our workers in general, there certainly weren't any on the clock at that hour."});
        NPCNode explainFestival = new(new string[] {"Ah so this is the motive angle you're working, huh?", "The harvest festiavl isn't a competition.", 
        "It's a way to give back to the community at the end of a good year's work.", "It's primarily for union members and their families, but we open it up to the public as an act of good will."});

        PlayerNode askEvidence = new(new string[] {"There's some solid evidence stacking up against the union.", "I need you to work with me here."});
        EncounterNode encounter = new();
        askEvidence.SetNext(encounter);

        askWhere.SetNext(explainWhere);
        explainWhere.SetNext(introReply);

        askFestival.SetNext(explainFestival);
        explainFestival.SetNext(introReply);

        (string, IDialogueNode)[] optionsList = {
            ("Ask whereabouts", askWhere),
            ("Ask about festival", askFestival),
            ("Ask about evidence", askEvidence)
        };

        introReply.SetOptions(optionsList);
        
        return new DialogueTree(intro);
    }

    private DialogueTree BuildIntroMetSamuel()
    {
        NPCNode intro = new(new string[] {"Hello detective. I'm sure you have many questions, but I have some things to tell you first.",
        "The Small Pines Beaver Lumber Union reserves the right to have a single person take on the role of representing the union and its interests in the public sphere.",
        "I'm Doug Beaver the Beaver Union union rep, and I'm that person.", "So unless you have a warrant to arrest a specific person, I'm the guy you're stuck with."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] { "Where were you on the night of the berry disappearance?" });
        PlayerNode askTheft = new(new string[] { "Do you have any client you think might be involved with the berry disappearance?" });

        NPCNode explainWhere = new(new string[] {"Me personally, I was at home fast asleep.", "I prescribe to that work hard, sleep hard mentality.",
        "As for our workers in general, there certainly weren't any on the clock at that hour."});

        EncounterNode encounter = new();

        askWhere.SetNext(explainWhere);
        askTheft.SetNext(encounter);

        explainWhere.SetNext(introReply);

        PlayerNode askSam = new(new string[] { "What do you make of the claim that the beaver union throws it�s weight around is disrespectful to rental equipment?" });
        NPCNode samAnswer = new(new string[] { "That�s just business." });

        askSam.SetNext(samAnswer);
        samAnswer.SetNext(introReply);

        (string, IDialogueNode)[] IntroReplyOptionsList = {
            ("Ask Whereabouts", askWhere),
            ("Ask about mentality", askSam),
            ("Ask about berries", askTheft)
        };

        introReply.SetOptions(IntroReplyOptionsList);

        return new DialogueTree(intro);
    }

    private DialogueTree BuildIntroMetMarkAndSamuel()
    {
        NPCNode intro = new(new string[] {"Hello detective. I'm sure you have many questions, but I have some things to tell you first.",
        "The Small Pines Beaver Lumber Union reserves the right to have a single person take on the role of representing the union and its interests in the public sphere.",
        "I'm Doug Beaver the Beaver Union union rep, and I'm that person.", "So unless you have a warrant to arrest a specific person, I'm the guy you're stuck with."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] { "Where were you the night of the berry disappearance?" });
        PlayerNode askFestival = new(new string[] {"There are some people in town who think that you guys are upset at the berry festival for undercutting the union's harvest festival.",
        "Do you have anything to say on that matter?"});

        NPCNode explainWhere = new(new string[] {"Me personally, I was at home fast asleep.", "I prescribe to that work hard, sleep hard mentality.",
        "As for our workers in general, there certainly weren't any on the clock at that hour."});
        NPCNode explainFestival = new(new string[] {"Ah so this is the motive angle you're working, huh?", "The harvest festiavl isn't a competition.",
        "It's a way to give back to the community at the end of a good year's work.", "It's primarily for union members and their families, but we open it up to the public as an act of good will."});

        PlayerNode askEvidence = new(new string[] { "There's some solid evidence stacking up against the union.", "I need you to work with me here." });
        EncounterNode encounter = new();
        askEvidence.SetNext(encounter);

        askWhere.SetNext(explainWhere);
        explainWhere.SetNext(introReply);

        askFestival.SetNext(explainFestival);
        explainFestival.SetNext(introReply);

        PlayerNode askSam = new(new string[] { "What do you make of the claim that the beaver union throws it�s weight around is disrespectful to rental equipment?" });
        NPCNode samAnswer = new(new string[] { "That�s just business." });

        askSam.SetNext(samAnswer);
        samAnswer.SetNext(introReply);

        (string, IDialogueNode)[] optionsList = {
            ("Ask whereabouts", askWhere),
            ("Ask about festival", askFestival),
            ("Ask about mentality", askSam),
            ("Ask about evidence", askEvidence)
        };

        introReply.SetOptions(optionsList);

        return new DialogueTree(intro);
    }

    private DialogueTree BuildIntroMetAustynAndSamuel()
    {
        NPCNode intro = new(new string[] {"Hello detective. I'm sure you have many questions, but I have some things to tell you first.",
        "The Small Pines Beaver Lumber Union reserves the right to have a single person take on the role of representing the union and its interests in the public sphere.",
        "I'm Doug Beaver the Beaver Union union rep, and I'm that person.", "So unless you have a warrant to arrest a specific person, I'm the guy you're stuck with."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] { "Where were you the night of the berry disappearance?" });
        PlayerNode askUnion = new(new string[] { "I have a reliable source that the beaver union has the kind of equipment and manpower needed to carry out this kind of berry stealing operation." });

        NPCNode explainWhere = new(new string[] {"Me personally, I was at home fast asleep.", "I prescribe to that work hard, sleep hard mentality.",
        "As for our workers in general, there certainly weren't any on the clock at that hour."});
        NPCNode explainUnion = new(new string[] {"Sure detective, we aren't exactly hiding it.", "Anyone with eyes could tell you we have a number of boats and a decent sized workforce.",
        "But, it's not exactly like we're the only group with those kind of resources in the region."});

        PlayerNode askEvidence = new(new string[] { "There's some solid evidence stacking up against the union.", "I need you to work with me here." });
        EncounterNode encounter = new();
        askEvidence.SetNext(encounter);

        askWhere.SetNext(explainWhere);
        explainWhere.SetNext(introReply);

        askUnion.SetNext(explainUnion);
        explainUnion.SetNext(introReply);

        PlayerNode askSam = new(new string[] { "What do you make of the claim that the beaver union throws it�s weight around is disrespectful to rental equipment?" });
        NPCNode samAnswer = new(new string[] { "That�s just business." });

        askSam.SetNext(samAnswer);
        samAnswer.SetNext(introReply);

        (string, IDialogueNode)[] optionsList = {
            ("Ask whereabouts", askWhere),
            ("Ask about union", askUnion),
            ("Ask about mentality", askSam),
            ("Ask about evidence", askEvidence)
        };

        introReply.SetOptions(optionsList);

        return new DialogueTree(intro);
    }

    //if player has met both Austyn and Mark
    private DialogueTree BuildIntroMetAustynAndMark()
    {
        NPCNode intro = new(new string[] {"Hello detective. I'm sure you have many questions, but I have some things to tell you first.", 
        "The Small Pines Beaver Lumber Union reserves the right to have a single person take on the role of representing the union and its interests in the public sphere.", 
        "I'm Doug Beaver the Beaver Union union rep, and I'm that person.", "So unless you have a warrant to arrest a specific person, I'm the guy you're stuck with."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] {"Where were you the night of the berry disappearance?"});
        PlayerNode askUnion = new(new string[] {"I have a reliable source that the beaver union has the kind of equipment and manpower needed to carry out this kind of berry stealing operation."});
         PlayerNode askFestival = new(new string[] {"There are some people in town who think that you guys are upset at the berry festival for undercutting the union's harvest festival.", 
        "Do you have anything to say on that matter?"});

        NPCNode explainWhere = new(new string[] {"Me personally, I was at home fast asleep.", "I prescribe to that work hard, sleep hard mentality.", 
        "As for our workers in general, there certainly weren't any on the clock at that hour."});
        NPCNode explainUnion = new(new string[] {"Sure detective, we aren't exactly hiding it.", "Anyone with eyes could tell you we have a number of boats and a decent sized workforce.", 
        "But, it's not exactly like we're the only group with those kind of resources in the region."});
         NPCNode explainFestival = new(new string[] {"Ah so this is the motive angle you're working, huh?", "The harvest festiavl isn't a competition.", 
        "It's a way to give back to the community at the end of a good year's work.", "It's primarily for union members and their families, but we open it up to the public as an act of good will."});

        PlayerNode askEvidence = new(new string[] {"There's some solid evidence stacking up against the union.", "I need you to work with me here."});
        EncounterNode encounter = new();
        askEvidence.SetNext(encounter);

        askWhere.SetNext(explainWhere);
        explainWhere.SetNext(introReply);

        askUnion.SetNext(explainUnion);
        explainUnion.SetNext(introReply);

        askFestival.SetNext(explainFestival);
        explainFestival.SetNext(introReply);

        (string, IDialogueNode)[] optionsList = {
            ("Ask whereabouts", askWhere),
            ("Ask about union", askUnion),
            ("Ask about festival", askFestival),
            ("Ask about evidence", askEvidence)
        };

        introReply.SetOptions(optionsList);
        
        return new DialogueTree(intro);
    }

    private DialogueTree BuildIntroMetAllThree()
    {
        NPCNode intro = new(new string[] {"Hello detective. I'm sure you have many questions, but I have some things to tell you first.",
        "The Small Pines Beaver Lumber Union reserves the right to have a single person take on the role of representing the union and its interests in the public sphere.",
        "I'm Doug Beaver the Beaver Union union rep, and I'm that person.", "So unless you have a warrant to arrest a specific person, I'm the guy you're stuck with."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] { "Where were you the night of the berry disappearance?" });
        PlayerNode askUnion = new(new string[] { "I have a reliable source that the beaver union has the kind of equipment and manpower needed to carry out this kind of berry stealing operation." });
        PlayerNode askFestival = new(new string[] {"There are some people in town who think that you guys are upset at the berry festival for undercutting the union's harvest festival.",
        "Do you have anything to say on that matter?"});

        NPCNode explainWhere = new(new string[] {"Me personally, I was at home fast asleep.", "I prescribe to that work hard, sleep hard mentality.",
        "As for our workers in general, there certainly weren't any on the clock at that hour."});
        NPCNode explainUnion = new(new string[] {"Sure detective, we aren't exactly hiding it.", "Anyone with eyes could tell you we have a number of boats and a decent sized workforce.",
        "But, it's not exactly like we're the only group with those kind of resources in the region."});
        NPCNode explainFestival = new(new string[] {"Ah so this is the motive angle you're working, huh?", "The harvest festiavl isn't a competition.",
        "It's a way to give back to the community at the end of a good year's work.", "It's primarily for union members and their families, but we open it up to the public as an act of good will."});

        PlayerNode askEvidence = new(new string[] { "There's some solid evidence stacking up against the union.", "I need you to work with me here." });
        EncounterNode encounter = new();
        askEvidence.SetNext(encounter);

        askWhere.SetNext(explainWhere);
        explainWhere.SetNext(introReply);

        askUnion.SetNext(explainUnion);
        explainUnion.SetNext(introReply);

        askFestival.SetNext(explainFestival);
        explainFestival.SetNext(introReply);

        PlayerNode askSam = new(new string[] { "What do you make of the claim that the beaver union throws it�s weight around is disrespectful to rental equipment?" });
        NPCNode samAnswer = new(new string[] { "That's just business." });

        askSam.SetNext(samAnswer);
        samAnswer.SetNext(introReply);

        (string, IDialogueNode)[] optionsList = {
            ("Ask whereabouts", askWhere),
            ("Ask about union", askUnion),
            ("Ask about festival", askFestival),
            ("Ask about mentality", askSam),
            ("Ask about evidence", askEvidence)
        };

        introReply.SetOptions(optionsList);

        return new DialogueTree(intro);
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
         return new DialogueTree(new NPCNode(new string[] {"You've got nothing.", "You're just looking to pin it on us hardworking animals.", "Typical cop..."}));
    }

    //if Doug was the first main suspect that we won an encounter with
    private DialogueTree BuildAfterEncounterWinFirstMainSuspect()
    {
        NPCNode dialogue = new(new string[] {"Okay, sure. All our equipment, which includes rentals, follows a rigorous sign-out, sign-in process.", 
        "Non of our equipment could have been used for the theft.", 
        "As for our workers, they just want to work a good job, they aren't cultists.", 
        "Here, let me give you some advice.", "There's this bar called [Rat Prince's Bar] on the North side.", 
        "It's frequented by some rough types, but the kind of rough types that have some power behind them."});


        return new DialogueTree(dialogue);
    }

    //if Doug was the second or third main suspect that we won an encounter with. Yes I know it's grotesquely long
    private DialogueTree BuildAfterEncounterWinSecondOrThirdMainSuspect()
    {
        NPCNode dialogue = new(new string[] {"Okay, sure. All our equipment, which includes rentals, follows a rigorous sign-out, sign-in process.", 
        "Non of our equipment could have been used for the theft.", 
        "As for our workers, they just want to work a good job, they aren't cultists.", 
        "You know, if I was masterminding that kind of thing, I might hide the berries in some sort of rail car.", 
        "There's enough of them coming in, shipping out, and sitting on standby that the couldn't all be accounted for, right?"});

        return new DialogueTree(dialogue);
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
