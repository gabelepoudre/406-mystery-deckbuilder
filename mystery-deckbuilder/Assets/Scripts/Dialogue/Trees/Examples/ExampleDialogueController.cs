/*
 * author(s): Gabriel LePoudre
 * 
 * This script is an example how one (Ehsan) could implement a parse/walk of a dialogue tree
 * WARNING: Not developed for production use, just an example
 */

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
    private IDialogueNode _curNode;  // the current node is all that matters

    // Start is called before the first frame update
    void Start()
    {
        // find dialogue. Note that this tree is built on awake, because if it
        // was start there would be a race to see if this line was executed before BuildTree in ExampleDialogueTree
        DialogueTree dt = this.gameObject.GetComponent<ExampleDialogue>().dialogueTree;
        _curNode = dt.root;
        OnNext();
    }

    /*
     * Okay, the logic for this one is pretty ugly. Sorry. 
     * - OptionButtons are created as a prefab and set in the optionButtonPrefab
     * - These buttons have public fields where we can set an optionID and a reference to this script
     * - These buttons will call their internal method OnClick, which in turn calls OptionSelection() with \
     *      the proper optionID
     * - A buttons optionID is determined by a counter incremented within a for loop
     * - This counter also determines its y position 
     * - A buttons text is set with the option that is actually incremented in the for loop
     * - The button always callbacks the correct OptionSelection because it is set with "this" on instantiation
     * - The button is visible because it is instantiated as a child of the attached gameObject, which is \
     *      currently found within a canvas
     */
    private void SpawnOptionButtons()
    {
        if (_curNode.NodeType() != "option")
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
                reference.transform.position = new Vector3
                    (
                        reference.transform.position.x,
                        reference.transform.position.y + 100 + (counter * 40),
                        reference.transform.position.z
                    );
                reference.GetComponent<ExampleDialogueOptionButton>().optionID = counter;
                reference.GetComponent<ExampleDialogueOptionButton>().controllerScript = this;
                reference.GetComponentInChildren<Text>().text = option;
                buttons.Add(reference);
                counter += 1;
            }
        }
    }

    /* Called by an OptionButton prefab. See comment on SpawnOptionButtons for a more in-depth explaination*/
    public void OptionSelection(int option)
    {
        if (_curNode.NodeType() != "option")
        {
            Debug.LogError("Called OptionSelection when the current node wasn't an option");
        }
        else
        {
            OptionNode lCurNode = (OptionNode)_curNode;
            _curNode = lCurNode.Next(option);  // this next is overloaded. If no int is passed, we default to option 0

            // destroy all the buttons after an option is picked
            foreach(GameObject optionButton in buttons)
            {
                Destroy(optionButton);
            }

            _textIndex = -1;  // this matters again since we aren't an option anymore
            OnNext();  // make sure to call this to continue normally
        }
    }

    public void OnNext()
    {

        // if null, assume we wish to end the conversation
        if (_curNode == null)
        {
            this.gameObject.SetActive(false);
            return;
        } 
        // if player, cast as player. if we haven't exhausted dialogue, show it. If we have, go to the next node
        if (_curNode.NodeType() == "player")
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
                typeBox.text = lCurNode.NodeType();  // TODO change name from text
            }
        }
        // if an npc, same logic as a player
        else if (_curNode.NodeType() == "npc")
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
                typeBox.text = lCurNode.NodeType();  // TODO change name from text
            }
        }
        // if an option, we call SpawnOptionButtons which has all the logic we need
        else
        {
            SpawnOptionButtons();
        }
    }
}
