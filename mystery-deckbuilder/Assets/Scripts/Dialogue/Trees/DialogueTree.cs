using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree
{
    public IDialogueNode root;
    public DialogueTree(IDialogueNode root)
    {
        this.root = root;
    }
    
}
