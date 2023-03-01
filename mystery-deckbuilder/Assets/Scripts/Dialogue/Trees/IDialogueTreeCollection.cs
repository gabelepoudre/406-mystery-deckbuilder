/**
  * Author(s): Ehsan Soltan
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
  * an interface representing a collection of dialogue trees. The point is to build all the trees
  * here, and return the dictionary to a DialogueTreeDictionary variable in an NPC object to reference
**/
public interface IDialogueTreeCollection
{

    //a Dictionary with string keys and tuples of DialogueTrees as values
    public Dictionary<string, DialogueTree> GetDialogueTrees();
}
