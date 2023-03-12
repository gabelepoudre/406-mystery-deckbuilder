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
    private List<DeckCardContainerController> _deckContainerControllers = new();

    public void Start()
    {
        foreach(GameObject deckContainer in deckContainers)
        {
            _deckQuantities.Add(deckContainer.GetComponentInChildren<Text>());
            _deckContainerControllers.Add(deckContainer.GetComponent<DeckCardContainerController>());

        }

        Card card = (Card)Cards.CreateCardWithID(1);
        GameObject _cardPrefabInstance = null;

        switch (card.GetElement())
        {
            case "Intimidation":
                _cardPrefabInstance = Instantiate(redCardNoEncounter, _deckContainerControllers[0].spawn.position, _deckContainerControllers[0].spawn.rotation, this.gameObject.transform);
                break;
            case "Sympathy":
                _cardPrefabInstance = Instantiate(blueCardNoEncounter, _deckContainerControllers[0].spawn.position, _deckContainerControllers[0].spawn.rotation, this.gameObject.transform);
                break;
            case "Persuasion":
                _cardPrefabInstance = Instantiate(greenCardNoEncounter, _deckContainerControllers[0].spawn.position, _deckContainerControllers[0].spawn.rotation, this.gameObject.transform);
                break;
            case "Preparation":
                _cardPrefabInstance = Instantiate(greyCardNoEncounter, _deckContainerControllers[0].spawn.position, _deckContainerControllers[0].spawn.rotation, this.gameObject.transform);
                break;
        }

        if (_cardPrefabInstance == null)
        {
            Debug.LogWarning("I cry evertim");
        }

        if (_cardPrefabInstance.GetComponent<NoEncounterCardPrefabController>() == null)
        {
            Debug.LogWarning("I cry evertim2");
        }

        NoEncounterCardPrefabController c = _cardPrefabInstance.GetComponent<NoEncounterCardPrefabController>();
        card.SetAndInitializeNoEncounterFrontendController(c);
        _deckContainerControllers[0].SetCard(card);

    }


}
