using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EncounterPrefabController : MonoBehaviour
{
    public Image npcHeadshot;
    public Image glubHeadshot;
    public GameObject complianceBar;
    public GameObject patienceBar;
    public GameObject cardPlaceMat;

    public GameObject redCard;
    public GameObject blueCard;
    public GameObject greenCard;
    public GameObject greyCard;
    public Transform cardHighlightTransform;

    private BarScript _complianceBarScript;
    private BarScript _patienceBarScript;
    private PlaceMatPrefabController _placeMatScript;
    private NPCEncounterSpriteController _npcHeadshotScript;

    private Vector3 _oldHighlightedCardTransformPosition;
    private GameObject _highlightedCard;

    void Awake()
    {
        _complianceBarScript = complianceBar.GetComponent<BarScript>();
        if (_complianceBarScript == null)
        {
            Debug.LogError("Could not find BarScript on compliance bar");
        }
        _patienceBarScript = patienceBar.GetComponent<BarScript>();
        if (_patienceBarScript == null)
        {
            Debug.LogError("Could not find BarScript on compliance bar");
        }
        _placeMatScript = cardPlaceMat.GetComponent<PlaceMatPrefabController>();
    }

    public void Initialize(EncounterConfig config)
    {
        //_npcHeadshotScript = config.Opponent.gameObject.GetComponent<NPCEncounterSpriteController>();
        _complianceBarScript.SetMax(config.MaximumCompliance);
        _patienceBarScript.SetMax(config.MaximumPatience);
        _patienceBarScript.SetValue(config.MaximumPatience);
    }

    public void PlaceCard(Card card)
    {
        // find an open index
        int idx = _placeMatScript.GetEmptyTransformIndex();

        // set card position to that index
        card.SetPosition(idx);

        // get transform
        Transform empty = _placeMatScript.GetTransformFromIndex(idx);

        // actually instantiate the card
        GameObject cardFrontend = null;
        switch (card.GetElement())
        {
            case "Intimidation":
                cardFrontend = Instantiate(redCard, empty.position, empty.rotation, _placeMatScript.gameObject.transform);  
                break;
            case "Sympathy":
                cardFrontend = Instantiate(blueCard, empty.position, empty.rotation, _placeMatScript.gameObject.transform);
                break;
            case "Persuasion":
                cardFrontend = Instantiate(greenCard, empty.position, empty.rotation, _placeMatScript.gameObject.transform);
                break;
            case "Preparation":
                cardFrontend = Instantiate(greyCard, empty.position, empty.rotation, _placeMatScript.gameObject.transform);
                break;
        }

        // Initialize with the proper info
        CardPrefabController frontendController = cardFrontend.GetComponent<CardPrefabController>();
        card.SetAndInitializeFrontendController(frontendController);
    }

    public bool PlaceMatFull()
    {
        return _placeMatScript.IsFull();
    }

    public void RemoveCard(Card card)
    {
        _placeMatScript.ClearPosition(card.GetPosition());
        Destroy(card.GetFrontendController().gameObject);  // finds the card prefab
    }

    public void HighlightCard(GameObject cardFrontend)
    {
        _oldHighlightedCardTransformPosition = cardFrontend.transform.position;
        cardFrontend.transform.localScale = new Vector3(cardFrontend.transform.localScale.x * 2, cardFrontend.transform.localScale.y * 2, cardFrontend.transform.localScale.z);
        _highlightedCard = cardFrontend;

        Transform newTransform = cardHighlightTransform;
        _highlightedCard.transform.position = new Vector3(newTransform.position.x, newTransform.position.y, newTransform.position.z);
    }

    public void UnHighlightCard()
    {
        if (_highlightedCard == null)
        {
            Debug.LogWarning("There is no card to un-highlight");
        }
        else
        {
            _highlightedCard.transform.position = _oldHighlightedCardTransformPosition;
            _highlightedCard.transform.localScale = new Vector3(_highlightedCard.transform.localScale.x / 2, _highlightedCard.transform.localScale.y / 2, _highlightedCard.transform.localScale.z);
            _highlightedCard = null;
        }
    }

    public int GetPatience()
    {
        return _patienceBarScript.GetValue();
    }

    public int GetCompliance()
    {
        return _complianceBarScript.GetValue();
    }

    public void SetPatience(int value)
    {
        Debug.Log("Patience was set to " + value.ToString() + ", was " + _patienceBarScript.GetValue());
        _patienceBarScript.SetValue(value);
    }

    public void SetCompliance(int value)
    {
        Debug.Log("Compliance was set to " + value.ToString() + ", was " + _complianceBarScript.GetValue());
        _complianceBarScript.SetValue(value);
    }
}
