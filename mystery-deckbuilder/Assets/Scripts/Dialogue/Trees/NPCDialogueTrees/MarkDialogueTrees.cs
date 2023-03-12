/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of Mark's dialogue trees in a dictionary, the point being
 * that all of Mark's dialogue will be built here and passed to the NPC class 
 */
public class MarkDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public MarkDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary with dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildAfterEncounterWin());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());
    }

    /** intro **/
    private DialogueTree BuildIntro()
    {
        NPCNode intro = new(new string[] {"Um hi detective...", "I'm usually pretty busy, but I could spare some time right now if you needed something."});
        OptionNode introReply = new(); //create option list later
        intro.SetNext(introReply);

        PlayerNode askWhere = new(new string[] {"Where were you on the night of the berry disappearance?"});
        PlayerNode askRole = new(new string[] {"What is your role here in Small Pines?"});
        PlayerNode askTheft = new(new string[] {"So do you have any idea as to who might be involved in the berry theft?"});
        
        NPCNode explainWhere = new(new string[] {"I was probably up working on something in my shop.", "I'm always grinding to get ahead, you know.", 
        "Only the strongest survive in this economy."});
        NPCNode explainRole = new(new string[] {"I'm a bit of a general handyman around here. I'm experienced in just about every trade.", 
        "All of the real important ones anyway.", "I don't actually have any kind of ticket or whatever, but those schools are a bunch of scammers and gatekeepers anyway."});
        EncounterNode encounter = new();
        explainWhere.SetNext(introReply);
        explainRole.SetNext(introReply);

        askWhere.SetNext(explainWhere);
        askRole.SetNext(explainRole);
        askTheft.SetNext(encounter);

        (string, IDialogueNode) [] IntroReplyOptionsList = {
            ("Ask Whereabouts", askWhere),
            ("Ask about role", askRole),
            ("Ask about berries", askTheft)

        };

        introReply.SetOptions(IntroReplyOptionsList);

    
        return new DialogueTree(intro);
    }

    /** intro after you beat him **/
    private DialogueTree BuildAfterEncounterWin()
    {
         DialogueTree tree = new (new NPCNode(new string[] {"No one ever asks me,", "but in my opinion it was those beavers that stole the berries", 
         "After all of their disastrous union policies, they must be desperate for some good pres.", "Every year they host a harvest festival, and every year it's outshined by the berry festival.", 
         "At this point I bet they were coping and seething hard enough to try something crazy."}));
        return tree;
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        DialogueTree tree = new(new NPCNode(new string[] {"Oh you know, I've been too busy working on myself to keep up with the gossip."}));
        return tree;
    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
