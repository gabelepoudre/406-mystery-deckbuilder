using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCardContainerController : MonoBehaviour
{
    public Transform spawn;
    public CardCollectionQuantityController quantityController;

    private Card _currentCard;
    private GameObject _cardPrefabInstance;
    private DeckUIController _parentController;

    public void Start()
    {
        _parentController = GetComponentInParent<DeckUIController>();
    }

    public void SetQuantity((int, int) quant)
    {
        quantityController.CardsContainedInCollection = quant.Item1;
        quantityController.CardHoldMax = quant.Item2;
    }
}
