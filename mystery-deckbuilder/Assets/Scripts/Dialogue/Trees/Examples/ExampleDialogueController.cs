using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleDialogueController : MonoBehaviour
{
    public Text typeBox;
    public Text dialogueBox;

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
            OptionNode lCurNode = (OptionNode)_curNode;
        }
    }
}
