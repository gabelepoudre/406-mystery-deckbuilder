using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDialogue : MonoBehaviour
{

    private DialogueTree _dialogueTree;


    private DialogueTree BuildTree()
    {
        PlayerNode intro = new(new string[] { "Hello!", "...", "Hello??" });
        NPCNode introReply = new(new string[] { "Hi..." });
        intro.SetNext(introReply);

        PlayerNode askName = new(new string[] {  })



    } 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
