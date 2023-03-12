using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinaDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public NinaDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary will Nibbles' dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildAfterEncounterWin());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());
    }

    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Hey Detective Glub, I'm Nina the Great Pyrenees!", 
        "I heard that you and the folks at the Town Hall are going to be working together to find the missing berries!", 
        "I just know the mystery's gonna be solved!"});
        PlayerNode reply = new(new string[] {"The Town Hall, huh? Say, what are your toughts on Mayor Crouton?"});
        NPCNode bigSis = new(new string[] {"Big... sister? We're basically inseparable!"});

        OptionNode options = new();

        PlayerNode askConnection = new(new string[] {"Since you have a pretty good connection with the political group, how do they plan to run the city?"});
        EncounterNode encounter = new();

        PlayerNode askBerries = new(new string[] {"Do you possibly know of someone or a group that would be capable of stealing all the berries?"});
        NPCNode explainBerries = new(new string[] {"Probs Elkie. He always has crazy ideas so he probs can pull it off if he sets his mind to it!", 
        "But he keeps making big sister do BIG work! Hey if you see big sis tell her Nina said 'Let's play!'"});


        intro.SetNext(reply);
        reply.SetNext(bigSis);
        bigSis.SetNext(options);
        askBerries.SetNext(explainBerries);
        askConnection.SetNext(encounter);
        explainBerries.SetNext(options);

        (string, IDialogueNode) [] optionsList = {
            ("Ask about city", askConnection),
            ("Ask about berries", askBerries)
        };
        
        options.SetOptions(optionsList);

        return new DialogueTree(intro);
    }

    
    private DialogueTree BuildAfterEncounterWin()
    {
        NPCNode root = new(new string[] {"Big sis has the best plans!", 
        "Like bigger parks for walks, treat dispensers all over the town and an extra day in the week for more play time!", 
        "But none of her plans get approved. I don't know why! They're literally the best plans!", "Tell her I WANNA PLAY!!!"});
         DialogueTree tree = new (root);
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        NPCNode root = new(new string[] {"They definitely run it with the best plan ever! Big sis is the boss woman YEAHHH!"});
        DialogueTree tree = new(root);
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }
}
