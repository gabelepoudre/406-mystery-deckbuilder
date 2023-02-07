/*
 * author(s) Ehsan Soltan
 *
 * This script contains the Dialogue class
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is responsible for storing information pertaining to a dialogue,
 * such as the name of the character you are speaking to and the actual sentences in the dialogue
 *
*/
public class Dialogue
{

    public string Name {get; set; }

    public string[] Sentences{get; private set; }

    public Dialogue(string newName, string[] newSentences)
    {
        Name = newName;
        Sentences = newSentences;
    }

   
}
