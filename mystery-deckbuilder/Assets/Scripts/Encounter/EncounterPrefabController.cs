/*
 * author(s): Gabriel LePoudre, William Metivier
 * 
 * Created to be attached to the Encounter prefab
 * Handles the "front end" of the Encounter
 * 
 */

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
        // this is in awake because we need the bar scripts to initialize before Start()
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

    /* Initializes the front-end with what it needs from a config */
    public void Initialize(EncounterConfig config)
    {
        _npcHeadshotScript = config.Opponent.encounterSprites;
        _complianceBarScript.SetMax(config.MaximumCompliance);
        _patienceBarScript.SetMax(config.MaximumPatience);
        _patienceBarScript.SetValue(config.MaximumPatience);
    }

    /* Testing function to demo that we can change headshots */
    public void RandomlyChangeHeadshot()
    {
        // Note: this is for test purposes
        Debug.Log("Changing NPC headshot for testing purposes");

        switch(Mathf.RoundToInt(Random.value*4))
        {
            case 0:
                _npcHeadshotScript.GetAngry(npcHeadshot);
                break;
            case 1:
                _npcHeadshotScript.GetNeutral(npcHeadshot);
                break;
            case 2:
                _npcHeadshotScript.GetHappy(npcHeadshot);
                break;
            case 3:
                _npcHeadshotScript.GetStress(npcHeadshot);
                break;
            case 4:
                _npcHeadshotScript.GetWorry(npcHeadshot);
                break;
        }
    }

    /* Place a card on the frontend. This requires us to create the frontend and link it to the Card class (SetAndInitializeFrontendController) */
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

    /* Reaches into placemat to see if it is full (convienience method) */
    public bool PlaceMatFull()
    {
        return _placeMatScript.IsFull();
    }

    /* Remove the Card prefab at that location */
    public void RemoveCard(Card card)
    {
        // TODO: remove call, for testing purposes only
        RandomlyChangeHeadshot();

        _placeMatScript.ClearPosition(card.GetPosition());
        Destroy(card.GetFrontendController().gameObject);  // finds the card prefab
    }

    /* Highlights a card given it's instantiated prefab */
    public void HighlightCard(GameObject cardFrontend)
    {
        _oldHighlightedCardTransformPosition = cardFrontend.transform.position;
        cardFrontend.transform.localScale = new Vector3(cardFrontend.transform.localScale.x * 2, cardFrontend.transform.localScale.y * 2, cardFrontend.transform.localScale.z);
        _highlightedCard = cardFrontend;

        Transform newTransform = cardHighlightTransform;
        _highlightedCard.transform.position = new Vector3(newTransform.position.x, newTransform.position.y, newTransform.position.z);
    }

    /* Unhighlights the currently highlighted card */
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
    
    /* This is only here because the draw button needs a non-static attached Monobehaviour to OnClick()*/
    public void DrawCard()
    {
        if (GameState.Meta.activeEncounter.Value != null)
        {
            GameState.Meta.activeEncounter.Value.DrawCard();
        }
        else
        {
            Debug.LogWarning("Tried to draw card when no encounter was active!");
        }
    }
}
