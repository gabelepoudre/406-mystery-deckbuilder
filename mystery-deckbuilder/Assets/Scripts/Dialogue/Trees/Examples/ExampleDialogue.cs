/*
 * author(s): Gabriel LePoudre
 * 
 * This script houses an example dialogue tree build as a demo to show how to build your own dialogue trees 
 */


using UnityEngine;

public class ExampleDialogue : MonoBehaviour
{

    public DialogueTree dialogueTree;


    /* Build the tree and return its reference */
    private DialogueTree BuildTree()
    {

        // intro
        PlayerNode intro = new(new string[] { "Hello!", "...", "Hello??" });
        NPCNode introReply = new(new string[] { "Hi..." });
        intro.SetNext(introReply);

        PlayerNode hardOfHearing = new(new string[] { "Are you hard of hearing?" });
        introReply.SetNext(hardOfHearing);
        NPCNode hardOfHearingReply = new(new string[] { "No..." });
        hardOfHearing.SetNext(hardOfHearingReply);

        // preparing the base options node. Empty for now, will be filled
        OptionNode baseOptions = new();
        hardOfHearingReply.SetNext(baseOptions);

        //* ask name logical path. returns to base options after completion
        PlayerNode name = new(new string[] { "What is your name?" });
        NPCNode nameReply = new(new string[] { "George..." });
        name.SetNext(nameReply);
        nameReply.SetNext(baseOptions);

        //* ask why George is here
        PlayerNode whyHere = new(new string[] { "Why are you here?", "Are you lost?" });
        NPCNode whyHereReply = new(new string[] { "There is no here...", "We aren't real...", "We are an example..." });
        whyHere.SetNext(whyHereReply);

        //** create options after george tells us we are an example.
        OptionNode weAreExampleOptions = new();
        whyHereReply.SetNext(weAreExampleOptions);

        PlayerNode example = new(new string[] { "Wait. What do you mean by \"Example\"?" });
        NPCNode exampleReply = new(new string[] { "IDK" });
        example.SetNext(exampleReply);
        exampleReply.SetNext(baseOptions);

        (string, IDialogueNode)[] exampleOptionList = { 
            ("Leave conversation", null),
            ("Ask what he means", example)
        };
        weAreExampleOptions.SetOptions(exampleOptionList);

        // fill base options
        (string, IDialogueNode)[] baseOptionList = {
            ("Leave conversation", null),
            ("Ask name", name),
            ("Ask why he is here", whyHere)
        };
        baseOptions.SetOptions(baseOptionList);

        Debug.Log("Built Tree");

        return new DialogueTree(intro);
    } 

    // Awake is called before the start
    void Awake()
    {
        this.dialogueTree = BuildTree();
    }

}
