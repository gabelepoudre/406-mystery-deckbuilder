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
    public Text cardTypeOnHighlight;
    public GameObject complianceBar;
    public GameObject patienceBar;
    public GameObject cardPlaceMat;

    public GameObject cardPrefab;

    public Transform cardHighlightTransform;
    public Image FrustrationImage;
    public Image MadImage;
    public Image SweatImage;
    public GameObject YouWonPage;
    public GameObject YouLostPage;

    private BarScript _complianceBarScript;
    private BarScript _patienceBarScript;
    private PlaceMatPrefabController _placeMatScript;
    private NPCEncounterSpriteController _npcHeadshotScript;
    private RewardDisplayController _rewardController;
    private int _cardOnWin;

    private Vector3 _oldHighlightedCardTransformPosition;
    private GameObject _highlightedCard;

    public bool HighlightLock { get; set; } = false;

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
        _rewardController = YouWonPage.GetComponent<RewardDisplayController>();
    }

    /* Initializes the front-end with what it needs from a config */
    public void Initialize(EncounterConfig config)
    {
        _npcHeadshotScript = config.Opponent.encounterSprites;
        ChangeHeadshotBasedOnPatience();
        _complianceBarScript.SetMax(config.MaximumCompliance);
        _patienceBarScript.SetMax(config.MaximumPatience);
        _patienceBarScript.SetValue(config.MaximumPatience);
        _cardOnWin = config.Opponent.cardIDUnlockFromWinEncounter;
    }

    public GameObject GetHighlightedCard()
    {
        return _highlightedCard;
    }

    public void ChangeHeadshotBasedOnPatience()
    {
        float remainingPatienceRatio = _patienceBarScript.GetValue() / (float)_patienceBarScript.GetMax();
        Debug.Log("AAAAAAAAAA " + remainingPatienceRatio);
        Debug.Log(_patienceBarScript.GetValue());
        Debug.Log(_patienceBarScript.GetMax());

        switch (remainingPatienceRatio)
        {
            case 0:
                _npcHeadshotScript.GetPhaseOne(npcHeadshot);
                break;
            case <= 0.2f:
                _npcHeadshotScript.GetPhaseFour(npcHeadshot);
                break;
            case <= 0.5f:
                _npcHeadshotScript.GetPhaseThree(npcHeadshot);
                break;
            case <= 0.75f:
                _npcHeadshotScript.GetPhaseTwo(npcHeadshot);
                break;
            default:
                _npcHeadshotScript.GetPhaseOne(npcHeadshot);
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
        cardFrontend = Instantiate(cardPrefab, empty.position, empty.rotation, _placeMatScript.gameObject.transform);

        GameState.Meta.activeEncounterCardDrawn.Raise();

        // Initialize with the proper info
        CardPrefabController frontendController = cardFrontend.GetComponent<CardPrefabController>();
        card.SetAndInitializeFrontendController(frontendController);
    }

    /* Reaches into placemat to see if it is full (convienience method) */
    public bool PlaceMatFull()
    {
        return _placeMatScript.IsFull();
    }

    /* Reaches into placemat to see if it is empty (convienience method) */
    public bool PlaceMatEnpty()
    {
        return _placeMatScript.IsEmpty();
    }

    /* Remove the Card prefab at that location */
    public void RemoveCard(Card card)
    {
        _placeMatScript.ClearPosition(card.GetPosition());
        Destroy(card.GetFrontendController().gameObject);  // finds the card prefab
    }

    /* Highlights a card given it's instantiated prefab */
    public void HighlightCard(GameObject cardFrontend)
    {
        if (!HighlightLock && _highlightedCard != null)
        {
            CardPrefabController c = _highlightedCard.GetComponent<CardPrefabController>();
            if (c.GetHighlighted())  // if card thinks it should still be highlighted, force unhighlight again
            {
                c.UnHighlightCard();
            }
        }
        if (!HighlightLock || _highlightedCard == null)
        {
            _oldHighlightedCardTransformPosition = cardFrontend.transform.position;
            cardFrontend.transform.localScale = new Vector3(cardFrontend.transform.localScale.x * 2, cardFrontend.transform.localScale.y * 2, cardFrontend.transform.localScale.z);
            this.cardTypeOnHighlight.text = cardFrontend.GetComponent<CardPrefabController>().GetElement();
            _highlightedCard = cardFrontend;

            Transform newTransform = cardHighlightTransform;
            _highlightedCard.transform.position = new Vector3(newTransform.position.x, newTransform.position.y, newTransform.position.z);
        }
    }

    /* Unhighlights the currently highlighted card */
    public void UnHighlightCard()
    {
        if (!HighlightLock)
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
                this.cardTypeOnHighlight.text = "";
            }
        }
    }

    public int GetPatience()
    {
        return _patienceBarScript.GetValue();
    }

    public int GetMaxPatience()
    {
        return _patienceBarScript.GetMax();
    }

    public int GetCompliance()
    {
        return _complianceBarScript.GetValue();
    }

    public int GetMaxCompliance()
    {
        return _complianceBarScript.GetMax();
    }

    public bool SetAndCheckPatience(int value)
    {
        Debug.Log("Patience was set to " + value.ToString() + ", was " + _patienceBarScript.GetValue());
        _patienceBarScript.SetValue(value);
        if (_patienceBarScript.IsEmpty())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool SetAndCheckCompliance(int value)
    {
        Debug.Log("Compliance was set to " + value.ToString() + ", was " + _complianceBarScript.GetValue());
        _complianceBarScript.SetValue(value);
        if (_complianceBarScript.IsFull())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /* This is only here because the draw button needs a non-static attached Monobehaviour to OnClick()*/
    public void DrawCard()
    {
        if (GameState.Meta.activeEncounter.Value != null)
        {
            GameState.Meta.activeEncounter.Value.DrawCard(1);
        }
        else
        {
            Debug.LogWarning("Tried to draw card when no encounter was active!");
        }
    }

    public void DisplayYouLostScreen()
    {
        YouLostPage.SetActive(true);
    }
    public void DisplayYouWonScreen()
    {
        YouWonPage.SetActive(true);
        _rewardController.DisplayCardAsReward(_cardOnWin);
    }

    public void DestroyEncounterWithLoss()
    {
        GameState.Meta.activeEncounter.Value.DestroyEncounter(false);
    }

    public void DestroyEncounterWithWin()
    {
        GameState.Meta.activeEncounter.Value.DestroyEncounter(true);
    }
}
