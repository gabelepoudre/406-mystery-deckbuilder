/*
 * author(s): Gabriel LePoudre
 * 
 * This script stores all things dialogue nodes
 */

using System.Collections.Generic;


/* This dialogue interface is used to treat all dialogue nodes as equals (for instance, as a return) */
public interface IDialogueNode
{
    public string NodeType();
    public IDialogueNode Next();  // this function will return the reference to the next node
}

/* A somewhat unused interface that overloads Next() for option nodes to contain an option parameter */
public interface IOptionNode: IDialogueNode
{
    public IDialogueNode Next(int option); // this function will return the reference to the node corresponding to the option
}

/* Class for storing things that the player says! (or- what we say that the player says)*/
public class PlayerNode: IDialogueNode
{
    private IDialogueNode _next;
    public string[] dialogue;

    /* The type of node (player, npc, option) we are dealing with */
    public string NodeType() { return "player"; }

    /* Returns the reference to the next node in the tree (the next set of dialogue) */
    public IDialogueNode Next() { return _next; }

    public void SetNext(IDialogueNode next) { this._next = next; }

    /* Note: next default to null, which will terminate the conversation */
    public PlayerNode(string[] dialogue, IDialogueNode next = null)
    {
        this.dialogue = dialogue;
        this._next = next;
    }
}

/* Class for storing things that the npc says! */
public class NPCNode : IDialogueNode
{
    private IDialogueNode _next;
    public string[] dialogue;
    public string Name { get; set; }

    /* The type of node (player, npc, option) we are dealing with */
    public string NodeType() { return "npc"; }

    /* Returns the reference to the next node in the tree (the next set of dialogue) */
    public IDialogueNode Next() { return _next; }

    public void SetNext(IDialogueNode next) { this._next = next; }

    /* Note: next default to null, which will terminate the conversation */
    public NPCNode(string[] dialogue, IDialogueNode next = null, string name=null)
    {
        this.dialogue = dialogue;
        this.Name = name;
        this._next = next;
    }
}

public class OptionNode: IOptionNode
{
    private List<IDialogueNode> _nextOptions = new(); // list of options (Next(option) choices)
    public string[] options;  // list of strings indicating the options. In the same order as options

    /* The type of node (player, npc, option) we are dealing with */
    public string NodeType() { return "option"; }

    /* When not overloaded, selects the first option*/
    public IDialogueNode Next() { return _nextOptions[0]; }

    /* Returns the reference to the next node in the tree (the next set of dialogue) */
    public IDialogueNode Next(int option) { return _nextOptions[option]; }

    /* Given an array of (string, node) tuples, sets the options*/
    public void SetOptions((string, IDialogueNode)[] options)
    {
        List<string> opts_list = new();
        _nextOptions.Clear();
        foreach ((string, IDialogueNode) option in options)
        {
            opts_list.Add(option.Item1);
            _nextOptions.Add(option.Item2);
        }
        this.options = opts_list.ToArray();
    }

    /* Note: if you fail to SetOptions or set them via constructor, you will run into errors as they are null */
    public OptionNode((string, IDialogueNode)[] options = null)
    {
        if (options != null)
        {
            List<string> opts_list = new();
            foreach ((string, IDialogueNode) option in options)
            {
                opts_list.Add(option.Item1);
                _nextOptions.Add(option.Item2);
            }
            this.options = opts_list.ToArray();
        }
    }

}

/* A class for representing a leaf node that indicates when an encounter should start */
public class EncounterNode: IDialogueNode
{
    private IDialogueNode _next;

    public string NodeType() { return "encounter"; }

    public IDialogueNode Next() { return _next; }

    public EncounterNode()
    {
        _next = null; //since it must always be a leaf node
    }

}
