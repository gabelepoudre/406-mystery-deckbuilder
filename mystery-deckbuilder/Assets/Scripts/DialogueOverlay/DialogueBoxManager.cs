/*
 *
 * author(s) Ehsan Soltan
 *
 * This script contains a singleton class that is responsible for managing the dialogue box.
 * It contains public methods for starting a dialogue, displaying the next sentence, and ending the dialogue.
 *
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The singleton class responsible for the following:
 * Instantiating DialogueBox GameObjects
 * -Starting dialogues
 * -Commanding the instantiated DialogueBox to display the next sentence
 * -Ending the dialogue and commanding the DialogueBox to destroy itself
*/
public class DialogueBoxManager : MonoBehaviour
{

    public static DialogueBoxManager Instance {get; private set; }

    private DialogueTree _dialogueTree;
    private IDialogueNode _currentNode;

    private Queue<string> _sentences = new();

    [SerializeField] private GameObject _dialogueBoxPrefab;
    
    private GameObject _dialogueBox;

    /*Since this is a singleton class, we destroy all other instances of it that aren't this one*/
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
        
    }

    /* A built-in dialogue to be called by the button in the DialogueTest scene 
     * for the purpose of demonstration.
    */
    public void TestDialogueStart() 
    {
        /*
        PlayerNode p1 = new(new string[]{"yoyoyo this is a test", "still testing now we go next node"});
        NPCNode n1 = new(new string[]{"hey this is npc talking", "im still talking"});
        OptionNode o1 = new(new (string, IDialogueNode)[]{("go player", p1), ("go npc", n1)});
        p1.SetNext(o1);
        DialogueTree test = new(p1);
        StartDialogue(test);
        */

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

        StartDialogue(new DialogueTree(intro));
        
    }

    /* Initiates a new dialogue by instantiating a DialogueBox, adding its sentences to the queue
     setting the name of the DialogueBox, and displaying the next sentence (in this case the first one)
     * Parameters:
     *      dialogue - A Dialogue object whose sentences will be enqueued
    */
    public void StartDialogue(DialogueTree newDialogueTree)
    {
        _dialogueTree = newDialogueTree;

        _dialogueBox = Instantiate(_dialogueBoxPrefab, new Vector3(0, -7, 0), Quaternion.identity);

        //an empty entrypoint node
        _currentNode = new PlayerNode(new string[]{}, _dialogueTree.root);
        NextNode();
        
    }



    public void NextNode()
    {
        _currentNode = _currentNode.Next();
        if (_currentNode == null)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();

        if (_currentNode.NodeType() != "option") {
            _dialogueBox.GetComponent<DialogueBox>().SetName(_currentNode.NodeType()); //change this to name stored in tree
            
            EnqueueAllSentences();
            DisplayNextSentence();
        }
        else {
            _dialogueBox.GetComponent<DialogueBox>().SpawnOptionBox((OptionNode)_currentNode); //create option box
        }
    }

    public void NextNodeByOptionIndex(int index)
    {
        
        if (_currentNode.NodeType() != "option")
        {
            Debug.LogError("tried to NextNodeByOptionIndex() when current node wasn't option node");
        }
        _currentNode = new PlayerNode(new string[]{}, ((OptionNode)_currentNode).Next(index));
        NextNode();
    }

    private void EnqueueAllSentences()
    {
        if (_currentNode.NodeType() == "player")
        {
            foreach(string sentence in ((PlayerNode)_currentNode).dialogue)
            {
                _sentences.Enqueue(sentence);
            }
        }
        else if (_currentNode.NodeType() == "npc")
        {
            foreach(string sentence in ((NPCNode)_currentNode).dialogue)
            {
                _sentences.Enqueue(sentence);
            }
        }
        else
        {
            Debug.LogError("error. tried to enqueue sentences of option node");
        }
    }

    
    /* Dequeues the sentence queue, and commands the previously instantiated DialogueBox to display 
     * the dequeued sentence
     */
    public void DisplayNextSentence()
    {
        
        if (_currentNode.NodeType() == "option")
        {
            return;
        }
        
        if (_sentences.Count != 0) {
            _dialogueBox.GetComponent<DialogueBox>().DisplaySentence(_sentences.Dequeue());
        }
        else {
            NextNode();
        }
        
    }

    

    /* Commands the previously instantiated DialogueBox to destroy itself */
    public void EndDialogue()
    {
        _dialogueBox.GetComponent<DialogueBox>().DestroyDialogueBox();
    }
}
