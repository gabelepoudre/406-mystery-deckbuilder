using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDialogueNode
{
    public string Type();
    public IDialogueNode Next();
}

public interface IOptionNode: IDialogueNode
{
    public IDialogueNode Next(int option);
}

public class PlayerNode: IDialogueNode
{
    private IDialogueNode _next;
    public string[] dialogue;

    public string Type() { return "player"; }

    public IDialogueNode Next() { return _next; }

    public PlayerNode(string[] dialogue, IDialogueNode next)
    {
        this.dialogue = dialogue;
        this._next = next;
    }
}

public class NPCNode : IDialogueNode
{
    private IDialogueNode _next;
    public string[] dialogue;

    public string Type() { return "npc"; }

    public IDialogueNode Next() { return _next; }

    public NPCNode(string[] dialogue, IDialogueNode next)
    {
        this.dialogue = dialogue;
        this._next = next;
    }
}

public class OptionNode: IOptionNode
{
    private List<IDialogueNode> _nextOptions = new();
    public string[] options;

    public string Type() { return "option"; }

    public IDialogueNode Next() { return _nextOptions[0]; }

    public IDialogueNode Next(int option) { return _nextOptions[option]; }

    public OptionNode((string, IDialogueNode)[] options)
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
