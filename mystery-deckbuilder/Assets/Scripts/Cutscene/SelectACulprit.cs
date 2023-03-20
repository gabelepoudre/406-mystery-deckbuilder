using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is not the most elegant script by any means lol
public class SelectACulprit : MonoBehaviour
{
    private List<(bool, Transform)> targets = new List<(bool, Transform)>();
    private List<GameObject> culprits = new List<GameObject>(); //TODO what data type?
    public GameObject nibblesFrame;
    public GameObject austinFrame;
    public GameObject austynFrame;
    public GameObject alanFrame;
    public GameObject markFrame;
    public GameObject samuelFrame;
    public GameObject dougFrame;
    public GameObject elkFrame;
    public GameObject ratBossFrame;
    public GameObject ratPrinceFrame;
    public GameObject wolverineFrame;
    public GameObject croutonFrame;
    public GameObject ninaFrame;
    public GameObject speckFrame;
    public GameObject oslowFrame;
    public GameObject clayFrame;
    public GameObject marryFrame;
    //store a reference to each culprit object here?


    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform t in this.GetComponentsInChildren<Transform>())
        {
            if (t != gameObject.transform)  // we have to check if it is our Transform, for some reason
            {
                targets.Add((false, t));
            }
        }

        if (GameState.NPCs.npcNameToMet["Nibbles"].Value) { culprits.Add(nibblesFrame); }
        if (GameState.NPCs.npcNameToMet["Austin"].Value) { culprits.Add(austinFrame); }
        if (GameState.NPCs.npcNameToMet["Austyn"].Value) { culprits.Add(austynFrame); }
        if (GameState.NPCs.npcNameToMet["Alan"].Value) { culprits.Add(alanFrame); }
        if (GameState.NPCs.npcNameToMet["Mark"].Value) { culprits.Add(markFrame); }
        if (GameState.NPCs.npcNameToMet["Samuel"].Value) { culprits.Add(samuelFrame); }
        if (GameState.NPCs.npcNameToMet["Doug"].Value) { culprits.Add(dougFrame); }
        if (GameState.NPCs.npcNameToMet["Elk Secretary"].Value) { culprits.Add(elkFrame); }
        if (GameState.NPCs.npcNameToMet["Rat Leader"].Value) { culprits.Add(ratBossFrame); }
        if (GameState.NPCs.npcNameToMet["Rat Prince"].Value) { culprits.Add(ratPrinceFrame); }
        if (GameState.NPCs.npcNameToMet["Wolverine"].Value) { culprits.Add(wolverineFrame); }
        if (GameState.NPCs.npcNameToMet["Crouton"].Value) { culprits.Add(croutonFrame); }
        if (GameState.NPCs.npcNameToMet["Nina"].Value) { culprits.Add(ninaFrame); }
        if (GameState.NPCs.npcNameToMet["Speck"].Value) { culprits.Add(speckFrame); }
        if (GameState.NPCs.npcNameToMet["Oslow"].Value) { culprits.Add(oslowFrame); }
        if (GameState.NPCs.npcNameToMet["Clay"].Value) { culprits.Add(clayFrame); }
        if (GameState.NPCs.npcNameToMet["Big Rat"].Value) { culprits.Add(ratBossFrame); }
        if (GameState.NPCs.npcNameToMet["Marry"].Value) { culprits.Add(marryFrame); }

        foreach (GameObject g in culprits)
        {
            int i = GetEmptyIndex();
            Instantiate(g, targets[i].Item2.position, targets[i].Item2.rotation, this.gameObject.transform);
        }

    }

    private int GetEmptyIndex()
    {
        for (int index = 0; index <= targets.Count - 1; index++)
        {
            (bool, Transform) location = targets[index];
            if (!location.Item1)
            {
                targets[index] = (true, location.Item2);
                return index;
            }
        }
        return -1;
    }

    //check has met gamestates and add the npcs to a list of mugshots to display
}
