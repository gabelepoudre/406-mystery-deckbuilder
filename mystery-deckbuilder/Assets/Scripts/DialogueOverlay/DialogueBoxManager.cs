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

    //the queue which stores the sentences in a given dialogue.
    private Queue<string> sentences;

    [SerializeField] private GameObject dialogueBoxPrefab;
    
    private GameObject dialogueBox;

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
        sentences = new Queue<string>();
        
    }

    /* A built-in dialogue to be called by the button in the DialogueTest scene 
     * for the purpose of demonstration.
    */
    public void TestDialogueStart() 
    {
        string[] d = {"Hi! I'm Ehsan, and this is a demo of the dialogue overlay.",
        "It's not much, but it's honest work.", 
        "Feel free to notify me if you have any suggestions!", 
        "Well, I guess that's all from me. Cya later!"};
        StartDialogue(new Dialogue("Ehsan", d));
    }

    /* Initiates a new dialogue by instantiating a DialogueBox, adding its sentences to the queue
     setting the name of the DialogueBox, and displaying the next sentence (in this case the first one)
     * Parameters:
     *      dialogue - A Dialogue object whose sentences will be enqueued
    */
    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach(string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        dialogueBox = Instantiate(dialogueBoxPrefab, new Vector3(0, -7, 0), Quaternion.identity);
        dialogueBox.GetComponent<DialogueBox>().SetName(dialogue.Name);
        DisplayNextSentence();

    }

    
    /* Dequeues the sentence queue, and commands the previously instantiated DialogueBox to display 
     * the dequeued sentence
     */
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            dialogueBox.GetComponent<DialogueBox>().DisplaySentence(sentences.Dequeue());
        }
    }

    /* Commands the previously instantiated DialogueBox to destroy itself */
    public void EndDialogue()
    {
        dialogueBox.GetComponent<DialogueBox>().DestroyDialogueBox();
    }
}
