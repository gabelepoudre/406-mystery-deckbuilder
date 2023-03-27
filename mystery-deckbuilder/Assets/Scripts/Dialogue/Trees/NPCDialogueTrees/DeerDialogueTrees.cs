using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public DeerDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary will Speck's dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
      
    }

    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Oh it's... You're... You're the renowned detective Glub. We are currently undergoing a bit of a crisis.", 
        "All the Saskatoon berries that were for the berry festival have gone missing. The town of Small Pines would really appreciate the help of such a renowned detective.",
        "Will you help us figure out this missing berry mystery?"});
        OptionNode options = new(); //set options later
        intro.SetNext(options);

        NPCNode no = new(new string[] {"Are you sure? The people of Small Pines could really use your help."});
        no.SetNext(options);

        NPCNode yes = new(new string[] {"Thank you. I will pass you over to our local detective Black Bear."});


        (string, IDialogueNode) [] OptionsList = {
            ("Yes", yes),
            ("No", no)
        };

        options.SetOptions(OptionsList);


        return new DialogueTree(intro);
    }

   
    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }
}