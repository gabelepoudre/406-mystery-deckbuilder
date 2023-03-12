using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUIController : MonoBehaviour
{
    public GameObject[] deckContainers;
    public GameObject[] collectionContainers;

    public GameObject redCardNoEncounter;
    public GameObject blueCardNoEncounter;
    public GameObject greenCardNoEncounter;
    public GameObject greyCardNoEncounter;

    private List<Text> _deckQuantities = new();
    private List<GameObject> _deckSpawns = new();

    public void Start()
    {
        foreach(GameObject deckContainer in deckContainers)
        {
            _deckQuantities.Add(deckContainer.GetComponentInChildren<Text>());

        }
    }


}
