/*
 *
 * author(s) Ehsan Soltan
 *
 * This script contains the DialogueBox class, responsible for visually displaying dialogue
 * 
 *
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


/*
 * The class responsible for expressing the behavior of the DialogueBox game object
 * Contains public methods for setting the nametag, displaying a sentence, and destroying itself
*/
public class DialogueBox : MonoBehaviour
{
    private string _text;

    //the speed with which it will move to the middle of the screen when instantiated
    private float _speed = 25.0f;
    private bool _waiting = true;

    private bool _inPosition = false; //whether it has finished moving to its position
    private bool _finished = false; //whether the dialogue is over
    public bool FinishedSentence = true;


    public Image npcHeadshot;

    private string lastTalker = "";

    public GameObject evilCanvasOptionBox;

    private Transform _childMurdererTransform;
    private ChildMurderer _childMurderer;

    [SerializeField] private GameObject _optionBoxPrefab;
    [SerializeField] private GameObject _optionButtonPrefab;
    private GameObject _optionBox;

    // Update is called once per frame
    void Update()
    {
        if (!_inPosition && !_finished) //if not in position then move to position
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -1.8f, 0), _speed * Time.deltaTime);
        }
        if (!_finished && transform.position == new Vector3(0, 0, 0)) //if its in position
        {
            _inPosition = true;
        }
        if (_finished) //if finished then start moving down
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -7f, 0), _speed * Time.deltaTime);
        }

    }

    /* Sets the text for the nametag portion of the dialogue box */
    public void SetName(string name)
    {
        transform.Find("Canvas").Find("Name").GetComponent<TextMeshProUGUI>().SetText(name);
    }

    /* Displays a given sentence string */
    public void DisplaySentence(string sentence)
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /* Sets finished to true, which will begin moving the box down, and will Destroy after a delay */
    public void DestroyDialogueBox() 
    {
        _finished = true;
        DestroyOptionBox();
        this.npcHeadshot.gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }

    /* instantiate a new option box, as well as its buttons */
    public void SpawnOptionBox(OptionNode optionNode)
    {
        evilCanvasOptionBox.SetActive(true);
        _optionBox = Instantiate(_optionBoxPrefab, new Vector3(0f, -1.5f, 0f), Quaternion.identity, transform);
        SpawnOptionButtons(optionNode);
    }

    /* destroy the spawned option box and its buttons */
    public void DestroyOptionBox()
    {
        evilCanvasOptionBox.SetActive(false);
        Destroy(_optionBox);
        foreach(Transform child in transform.Find("Canvas").transform.Find("Canvas").transform)
        {
            Destroy(child.gameObject);
        }
        _childMurderer.KillChildren();
    }

    /* spawn option buttons corresponding to optionss in optionNode 
     * NOTE: yes I should probably relegate this to an option box class instead but 
     * the Canvas was being weird so I'm doing it like this for now since the alpha is soon
     */
    private void SpawnOptionButtons(OptionNode optionNode)
    {
        int counter = 0;
        for (int index = 0; index < optionNode.options.Length; index++)
        {
            string option = optionNode.options[index];
            //instantiate option buttons
            GameObject optionButton = Instantiate(_optionButtonPrefab, new Vector3(5.0f, 1.3f - counter * 0.5f, 0f), 
            Quaternion.identity, _childMurdererTransform);
            optionButton.GetComponentInChildren<Text>().text = option; //set correct text of button

            //change text to red if it leads to encounter
            
            IDialogueNode node = optionNode.Next(index);
           
            //traverse tree until we hit null or back to options. if we hit an encounter node we set the text to red
            while (node != null && node.NodeType() != "option")
            {
                if (node.NodeType() == "encounter") optionButton.GetComponentInChildren<Text>().color = Color.red;
                node = node.Next();
            }
            

            int i = counter; //have to do this or else every button will call option 2 because thats what the counter ends at
            optionButton.GetComponentInChildren<Button>().onClick.AddListener(() => {
                DialogueManager.Instance.NextNodeByOptionIndex(i);
                DestroyOptionBox();
            });

            counter += 1;
        }
    }

    public void SpeedUp()
    {
        _waiting = false;
    }

    /* Will be invoked as a coroutine to display the characters of the sentence one-by-one */
    IEnumerator TypeSentence (string sentence)
    {
        FinishedSentence = false;
        _text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            _text += letter;
            transform.Find("Canvas").Find("Message").GetComponent<TextMeshProUGUI>().SetText(_text);
            if (_waiting) yield return new WaitForSeconds(0.03f);
        }
        FinishedSentence = true;
        GameState.Meta.dialogueGoing.Value = "";
        _waiting = true;
    }

    public void DarkenNpc()
    {
        Debug.Log(GameState.Meta.dialogueGoing.Value);
        try
        {
            if (GameState.Meta.dialogueGoing.Value == "player")
            {
                lastTalker = "player";
                npcHeadshot.color = new Color(0.7f, 0.7f, 0.7f, 1);
            }
            else if (GameState.Meta.dialogueGoing.Value == "" && lastTalker == "player")
            {
                npcHeadshot.color = new Color(0.7f, 0.7f, 0.7f, 1);
            }
               
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.dialogueGoing.OnChange -= DarkenNpc;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.dialogueGoing.OnChange -= DarkenNpc;
        }
    }

    public void NormalNpc()
    {
        Debug.Log(GameState.Meta.dialogueGoing.Value);
        try
        {
            if (GameState.Meta.dialogueGoing.Value != "player" && GameState.Meta.dialogueGoing.Value != "")
            {
                lastTalker = "npc";
                npcHeadshot.color = new Color(1, 1, 1, 1);
            }
            else if (GameState.Meta.dialogueGoing.Value == "" && lastTalker != "player" && lastTalker != "")
            {
                npcHeadshot.color = new Color(1, 1, 1, 1);
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.dialogueGoing.OnChange -= NormalNpc;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.dialogueGoing.OnChange -= NormalNpc;
        }
    }


    private void Start()
    {
        GameState.Meta.dialogueGoing.OnChange += NormalNpc;
        GameState.Meta.dialogueGoing.OnChange += DarkenNpc;
        _childMurderer = gameObject.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<ChildMurderer>();
        _childMurdererTransform = _childMurderer.gameObject.GetComponent<Transform>();
    }
}
