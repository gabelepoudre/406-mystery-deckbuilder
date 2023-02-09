using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleDialogueController : MonoBehaviour
{
    public Text typeBox;
    public Text dialogueBox;
    public GameObject optionButtonPrefab;

    private List<GameObject> buttons = new();
    private int _textIndex = -1;
    private IDialogueNode _curNode;

    // Start is called before the first frame update
    void Start()
    {
        // find dialogue
        DialogueTree dt = this.gameObject.GetComponent<ExampleDialogue>().dialogueTree;
        _curNode = dt.root;
        OnNext();
    }

    private void SpawnOptionButtons()
    {
        if (_curNode.Type() != "option")
        {
            Debug.LogError("Called SpawnOptionButtons when the current node wasn't an option");
        }
        else
        {
            OptionNode lCurNode = (OptionNode)_curNode;
            int counter = 0;
            foreach (string option in lCurNode.options)
            {
                GameObject reference = Instantiate(optionButtonPrefab, this.transform);
                reference.transform.position = new Vector3(reference.transform.position.x, reference.transform.position.y + 100 + (counter * 40), reference.transform.position.z);
                reference.GetComponent<ExampleDialogueOptionButton>().optionID = counter;
                reference.GetComponent<ExampleDialogueOptionButton>().controllerScript = this;
                reference.GetComponentInChildren<Text>().text = option;
                buttons.Add(reference);
                counter += 1;
            }
        }
    }

    /* Called by an OptionButton prefab*/
    public void OptionSelection(int option)
    {
        if (_curNode.Type() != "option")
        {
            Debug.LogError("Called OptionSelection when the current node wasn't an option");
        }
        else
        {
            OptionNode lCurNode = (OptionNode)_curNode;
            _curNode = lCurNode.Next(option);  // this next is overloaded. If no int is passed, we default to option 0
            _textIndex = -1;

            foreach(GameObject optionButton in buttons)
            {
                Destroy(optionButton);
            }

            OnNext();
        }
    }

    public void OnNext()
    {
        if (_curNode == null)
        {
            this.gameObject.SetActive(false);
            return;
        } 
        if (_curNode.Type() == "player")
        {
            PlayerNode lCurNode = (PlayerNode)_curNode;
            if (_textIndex >= lCurNode.dialogue.Length-1)
            {
                _curNode = _curNode.Next();
                _textIndex = -1;
                OnNext();
            }
            else
            {
                _textIndex += 1;
                dialogueBox.text = lCurNode.dialogue[_textIndex];
                typeBox.text = lCurNode.Type();  // TODO change name from text
            }
        }
        else if (_curNode.Type() == "npc")
        {
            NPCNode lCurNode = (NPCNode)_curNode;
            if (_textIndex >= lCurNode.dialogue.Length - 1)
            {
                _curNode = _curNode.Next();
                _textIndex = -1;
                OnNext();
            }
            else
            {
                _textIndex += 1;
                dialogueBox.text = lCurNode.dialogue[_textIndex];
                typeBox.text = lCurNode.Type();  // TODO change name from text
            }
        }
        else
        {
            SpawnOptionButtons();
        }
    }
}
