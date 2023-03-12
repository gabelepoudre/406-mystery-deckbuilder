using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public GlubDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary will Speck's dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("MapDialogue", BuildMapDialogue());
        _dialogueTreeDict.Add("Day2Intro", BuildDay2Intro());
      
    }

    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Phew... Finally arrived. My back hurts from the long bus ride.", 
        "Can't wait to try the Saskatoon berries at the festival though.", 
        "It's been a while since I had a vacation so I better make it count.", 
        "Ok enough wasting time here, let's go check out the berries!", 
        ".......", "I just realized I don't know where the Berry Farm is....", 
        "Maybe she can help me."});
        return new DialogueTree(intro);
    }

    private DialogueTree BuildDay2Intro()
    {
        PlayerNode root = new(new string[] {"Why is there yelling in the morning? I thought Small Pines was supposed to be as quiet as the deep sea.", 
        "This is supposed to be my special vacation, my luscious scales might get dirty if I get involved.", 
        "But if this is a big deal, then maybe I should check it out"});

        return new DialogueTree(root);
    }

    private DialogueTree BuildMapDialogue()
    {
        NPCNode intro = new(new string[] {"Thank goodness they give out a map for visitors. Without this map I'm like a fish out of water.", 
        "Oh wait...", "Anyways, I better head down to the farm"});
        return new DialogueTree(intro);
    }

   
    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }
}

