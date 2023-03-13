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

    public Transform previewCardSpawn;

    public int Page
    {
        get
        {
            return _page;
        }
        set
        {
            
        }
    }
    private int _page = 1;

    private List<Text> _deckQuantities = new();
    private List<DeckCardContainerController> _deckContainerControllers = new();
    private List<GameObject> _currentCardInstantiations = new();

    private List<(int, int, int, int)> GetDeckCards()
    {
        List<(int, int, int, int)> cards = new();

        for (int card_id = 0; card_id <= Cards.totalCardCount-1; card_id++)
        {
            int numberOfCardsWithId = 0;
            int numberOfActiveCardsWithId = 0;
            int maxNumberOfCards = 3;
            foreach(int card in GameState.Player.fullDeck.Value)
            {
                if (card == card_id)
                {
                    numberOfCardsWithId += 1;
                }
            }
            if (numberOfCardsWithId != 0)  // we found some
            {
                foreach (int card in GameState.Player.dailyDeck.Value)
                {
                    if (card == card_id)
                    {
                        numberOfActiveCardsWithId += 1;
                    }
                }
                cards.Add((card_id, numberOfActiveCardsWithId, numberOfCardsWithId, maxNumberOfCards));
            }
        }

        return cards;
    }

    private bool CanMovePageUp()
    {
        if (_page != 1)
        {
            return false;
        }
        return true;
    }

    private bool CanMovePageDown()
    {
        if (_page == 1)
        {
            return false;
        }
        return true;
    }

    public void DisplayCards(int page)
    {
        if (_currentCardInstantiations.Count != 0)
        {
            foreach(GameObject card in _currentCardInstantiations)
            {
                Destroy(card);
            }
        }

        List<(int, int, int, int)> ordered_cards = GetDeckCards();
        for (int card_section = -6 + (page*6); card_section < -6 + ((page+1)*6) && card_section <= GameState.Player.fullDeck.Value.Count-1; card_section++)
        {
            int normalized_idx = card_section - ((page - 1) * 6);
            (int, int, int, int) cardData = ordered_cards[normalized_idx];
            int cardIdx = cardData.Item1;
            (int, int, int) quant = (cardData.Item2, cardData.Item3, cardData.Item4);

            Card card = (Card)Cards.CreateCardWithID(cardIdx, true);
            GameObject _cardPrefabInstance = null;

            switch (card.GetElement())
            {
                case "Intimidation":
                    _cardPrefabInstance = Instantiate(redCardNoEncounter, _deckContainerControllers[normalized_idx].spawn.position, _deckContainerControllers[normalized_idx].spawn.rotation, this.gameObject.transform);
                    break;
                case "Sympathy":
                    _cardPrefabInstance = Instantiate(blueCardNoEncounter, _deckContainerControllers[normalized_idx].spawn.position, _deckContainerControllers[normalized_idx].spawn.rotation, this.gameObject.transform);
                    break;
                case "Persuasion":
                    _cardPrefabInstance = Instantiate(greenCardNoEncounter, _deckContainerControllers[normalized_idx].spawn.position, _deckContainerControllers[normalized_idx].spawn.rotation, this.gameObject.transform);
                    break;
                case "Preparation":
                    _cardPrefabInstance = Instantiate(greyCardNoEncounter, _deckContainerControllers[normalized_idx].spawn.position, _deckContainerControllers[normalized_idx].spawn.rotation, this.gameObject.transform);
                    break;
            }


            NoEncounterCardPrefabController c = _cardPrefabInstance.GetComponent<NoEncounterCardPrefabController>();
            c.makeBiggerTransform = previewCardSpawn;
            _cardPrefabInstance.transform.localScale = new Vector3(_cardPrefabInstance.transform.localScale.x - 0.43f, _cardPrefabInstance.transform.localScale.y - 0.43f, _cardPrefabInstance.transform.localScale.z);
            card.SetAndInitializeNoEncounterFrontendController(c);
            _deckContainerControllers[normalized_idx].SetQuantity(quant);
        }
    }

    public void Start()
    {
        foreach(GameObject deckContainer in deckContainers)
        {
            _deckQuantities.Add(deckContainer.GetComponentInChildren<Text>());
            _deckContainerControllers.Add(deckContainer.GetComponent<DeckCardContainerController>());
        }
        DisplayCards(1);

    }


}
