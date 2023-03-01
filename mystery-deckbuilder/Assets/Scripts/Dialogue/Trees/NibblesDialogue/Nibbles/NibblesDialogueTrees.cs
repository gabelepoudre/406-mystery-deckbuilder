/**
  * Author(s): Ehsan Soltan
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** The Class which holds all of Nibble's dialogue trees in a dictionary **/
public class NibblesDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict;

    public NibblesDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("IntroAfterEncounter", BuildIntroAfterEncounter());
    }

    /** Nibbles' intro **/
    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Cheesed to meet you detective! Are you finding enough to eat around here?", 
        "Hopefully our small town food can compete with your big city cuisine."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        NPCNode nibblesGourmand = new(new string[] {"I am Nibbles the mouse, I am a locally renowned gourmand.", 
        "A gourmouse, if you will. I have travelled far, training my palate on all of the amazing creations made of the flatland's boundaries."});
        NPCNode loveOfCuisine = new(new string[] {"Not particularly, however wherever you go there will be people with love of cuisine.", 
        "People for who cooking is an art and people for who eating connects to that wonderful art!"});
        NPCNode confidential = new(new string[] {"Hmmmmm........",
        "I don't know if I want to keep that information confidential or not..."});
        nibblesGourmand.SetNext(introReply);
        loveOfCuisine.SetNext(introReply);


        PlayerNode askAboutNibbles = new(new string[] {"You seem very familiar with food, little mouse. What's your history?"});
        PlayerNode askAboutSmallPines = new(new string[] {"I'm in town for the festival. Is Small Pines known for its culinary traditions?"});
        PlayerNode askAboutRestaurants = new(new string[] {"What's the best restaurant in town? one only pros would know about?"});
        askAboutNibbles.SetNext(nibblesGourmand);
        askAboutSmallPines.SetNext(loveOfCuisine);
        askAboutRestaurants.SetNext(confidential);

        (string, IDialogueNode) [] IntroReplyOptionsList = {
            ("Ask about Nibbles", askAboutNibbles),
            ("Ask about Small Pines", askAboutSmallPines),
            ("Ask about restaurants", askAboutRestaurants)

        };

        introReply.SetOptions(IntroReplyOptionsList);

        //this encounter node indicates that an encounter will start when the dialogue reaches it
        EncounterNode startEncounter = new();
        confidential.SetNext(startEncounter);


        return new DialogueTree(intro);
    }

    /** Nibbles' intro after you beat him **/
    private DialogueTree BuildIntroAfterEncounter()
    {
         DialogueTree tree = new (new NPCNode(new string[] {"You're very persuasive!",
        "Near the rail yard, operating out of an old sea can, there is a small perogy place known as Mike's Perogies", "That is where you should start."}));

        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }

    void Awake()
    {

    }


}
