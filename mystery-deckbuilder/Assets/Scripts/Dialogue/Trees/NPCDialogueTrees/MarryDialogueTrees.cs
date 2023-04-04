/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of Marry's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class MarryDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public MarryDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    

    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
    }

 
    private DialogueTree BuildIntro()
    {
        
        NPCNode greeting = new(new string[] {"Hello detective, how are you enjoying Small Pines? Would you care for a brochure?"});
        OptionNode reply = new(); //set later
        greeting.SetNext(reply);


        PlayerNode askAboutMarry = new(new string[] {"Nice to meet you, miss. Would you care to tell me about yourself?"});
        PlayerNode askAboutSights = new(new string[] {"Is there anything you might recommend as a must-see?"});
        
        NPCNode marryIntroduction = new(new string[] {"I'm Marry. I work for the Small Pines tourism board.", 
        "Normally I work in more of a planning and management position,", "but during our popular events, like the berry festival,"
        , "it's super important to have someone on the streets welcoming people to our wonderful little town.", "We don't really have the budget for a dedicated welcomer position, so I get to cover it."});
        NPCNode marryRecommendation = new(new string[] {"The berry farm is pretty this time of year.", "If you're free in the evening, the view of the sunset over the fields of Saskatoon berries is just gorgeous.", 
        "Just don't stay out too long.", "You wouldn't want to have a run-in with those unsavoury rat mobster types."});

        askAboutMarry.SetNext(marryIntroduction);
        askAboutSights.SetNext(marryRecommendation);
        marryIntroduction.SetNext(reply);
        marryRecommendation.SetNext(reply);

        (string, IDialogueNode)[] introReplyOptionsList = {
            ("Ask about Marry", askAboutMarry),
            ("Ask about sights", askAboutSights)
        };

        reply.SetOptions(introReplyOptionsList);



        return new DialogueTree(greeting);
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
