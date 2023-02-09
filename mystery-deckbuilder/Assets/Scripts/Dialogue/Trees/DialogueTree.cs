/*
 * author(s): Gabriel LePoudre
 * 
 * This script houses the DialogueTree class, which is a glorified root reference for dialogue nodes
 */

/* This class houses a glorified root reference for dialogue nodes */
public class DialogueTree
{
    public IDialogueNode root;
    public DialogueTree(IDialogueNode root)
    {
        this.root = root;
    }
    
}
