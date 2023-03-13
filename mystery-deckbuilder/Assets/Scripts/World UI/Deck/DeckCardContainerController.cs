using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCardContainerController : MonoBehaviour
{
    public Transform spawn;
    public DeckCardQuantityController quantityController;

    private Card _currentCard;
    private GameObject _cardPrefabInstance;
    private DeckUIController _parentController;

    public void Start()
    {
        _parentController = GetComponentInParent<DeckUIController>();
    }

    public void SetQuantity((int, int, int) quant)
    {
        quantityController.CardsActiveInDeck = quant.Item1;
        quantityController.CardsContainedInDeck = quant.Item2;
        quantityController.CardHoldMax = quant.Item3;
    }
}
