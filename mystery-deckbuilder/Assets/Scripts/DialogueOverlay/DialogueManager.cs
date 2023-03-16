/*
 *
 * author(s) Ehsan Soltan
 *
 * This script contains a singleton class that is responsible for managing dialogue.
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
 * -traversing a given dialogue tree
 * -Commanding the instantiated DialogueBox to display the next sentence and show the option box
 * -Ending the dialogue and commanding the DialogueBox to destroy itself
*/
public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance {get; private set; } //a static instance of itself

    public string NPCName {get; set;}

    public bool DialogueActive;

    public GameObject CurrentNPC;

    private DialogueTree _dialogueTree;
    private IDialogueNode _currentNode;

    private Queue<string> _sentences = new();

    [SerializeField] private GameObject _dialogueBoxPrefab;
    private GameObject _dialogueBox;
    

    /* Since this is a singleton class, we destroy all other instances of it that aren't this one */
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
        NPCName = "NPC";
        DialogueActive = false;
        _sentences = new Queue<string>();
    }


    /* Initiates a new dialogue by instantiating a DialogueBox, adding its sentences to the queue
     * setting the name of the DialogueBox, and displaying the next sentence (in this case the first one)
     */
    public void StartDialogue(DialogueTree newDialogueTree, GameObject currentNPC)
    {
        CurrentNPC = currentNPC;
        DialogueActive = true;
        NPCName = CurrentNPC.GetComponent<NPC>().CharacterName;


        //the current dialogue must be ended before starting a new one
        if (_dialogueBox != null)
        {
            Debug.LogError("Error: tried to start a new dialogue when one was already in session");
            return;
        }

        _dialogueTree = newDialogueTree;

        //instantiate the dialogue box prefab
        _dialogueBox = Instantiate(_dialogueBoxPrefab, new Vector3(0, -7, 0), Quaternion.identity);

        GameState.Meta.dialogueActive.Value = true;
        GoToNode(_dialogueTree.root); //we start at the root node of the dialogue tree
    }

    /* Moves to the next node in the dialogue tree
    */
    public void NextNode()
    {
        _currentNode = _currentNode.Next();

        //end dialogue if we've reached the end
        if (_currentNode == null)
        {
            EndDialogue();
            return;
        }

        //TODO: remove this once a proper way of initiating encounters is implemented
        // in which the arguments passed to the encounter like compliance and stuff is dependent
        // on the current NPC you're talking to, rather than Nibbles
        if (_currentNode.NodeType() == "encounter")
        {
            EndDialogue();
            NPC cur = CurrentNPC.GetComponent<NPC>();
            Encounter.StartEncounter(new EncounterConfig(cur, (int)cur.startingCompliance, (int)cur.startingPatience));
        }

        StopAllCoroutines(); //stop displaying text, in case player clicks next while text still writing

        if (_currentNode.NodeType() != "option") //if a normal dialogue node
        {

            if (_currentNode.NodeType() == "npc") 
            {
                
                
                string secondName = ((NPCNode)_currentNode).Name;
                if (secondName is null) {
                    _dialogueBox.GetComponent<DialogueBox>().SetName(NPCName);
                }
                else //for handling multiple different NPCs
                {
                    _dialogueBox.GetComponent<DialogueBox>().SetName(secondName);
                }
               
            }
            else
            {
                _dialogueBox.GetComponent<DialogueBox>().SetName("You");
            }

            EnqueueAllSentences();
            DisplayNextSentence();
        }
        else //if an option node
        {
            _dialogueBox.GetComponent<DialogueBox>().SpawnOptionBox((OptionNode)_currentNode); //create option box
        }
    }

    /* To be used if current node is an option node
     * Goes to the option corresponding to the given index
    */
    public void NextNodeByOptionIndex(int index)
    {
        
        if (_currentNode.NodeType() != "option") //error if current node is not an option node
        {
            Debug.LogError("tried to NextNodeByOptionIndex() when current node wasn't option node");
        }
     
        GoToNode(((OptionNode)_currentNode).Next(index));
    }
    
    /* Dequeues the sentence queue, and commands the previously instantiated DialogueBox to display 
     * the dequeued sentence 
     */
    public void DisplayNextSentence()
    {
        if (_dialogueBox == null) return; // TODO: 
        if (!_dialogueBox.GetComponent<DialogueBox>().FinishedSentence)
        {
            _dialogueBox.GetComponent<DialogueBox>().SpeedUp();
            return;
        }
        
        //if its an option node then the player has to pick an option, rather then clicking next
        if (_currentNode.NodeType() == "option")
        {
            return;
        }
        

        if (_sentences.Count != 0) //if we still have sentences in the queue
        {
            string sentence = _sentences.Dequeue();
            _dialogueBox.GetComponent<DialogueBox>().DisplaySentence(sentence);
        }
        else //if we ran out of sentences in the queue, traverse to next node
        {
            NextNode();
        }
        
    }

    /* Commands the previously instantiated DialogueBox to destroy itself */
    public void EndDialogue()
    {
        _dialogueBox.GetComponent<DialogueBox>().DestroyDialogueBox();
        DialogueActive = false;
        GameState.Meta.dialogueActive.Value = false;
        _sentences.Clear();

    }



    /* Enqueues all sentences contained in the current node */
    private void EnqueueAllSentences()
    {
        if (_currentNode.NodeType() == "encounter")
        {
            return;
        }

        // have to handle PlayerNode and NPCNode unless if IDialogueNode interface had a GetDialogue() or something
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
            Debug.LogError("error. tried to enqueue sentences of node type" + _currentNode.NodeType());
        }
    }

    /* Goes to the specified node via an new empty entrypoint node */
    private void GoToNode(IDialogueNode node)
    {
        _currentNode = new PlayerNode(new string[]{}, node);
        NextNode();
    }

    private void StartEncounter()
    {

        EndDialogue();
    }
}
