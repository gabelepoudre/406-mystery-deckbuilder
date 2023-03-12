/*
 * author(s): Gabriel LePoudre, William Metivier
 * 
 * The class that controls a card frontend
 * Implements Click and Deselection handlers
 * 
 */

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class CardPrefabController : MonoBehaviour, IPointerClickHandler, IDeselectHandler
{
    public Image highlight;
    public Image cardRect;
    public Image cardPicture;
    public Image type;
    public Image descriptionHolder;
    public Text cardName;
    public Text cardDescription;
    public Text visiblePatience;
    public Text visibleCompliance;
    public GameObject effectCirclePrefab;
    public GameObject options;
    public GameObject effectSlots;
    [SerializeField] Color selectionTint; 

    private List<GameObject> _effectCircles;  // TODO, we will eventually initialize circles which hover-over to display currently applied effects
    private int _defaultCompliance;
    private int _defaultPatience;
    private int _position;
    private EffectSlotController _effectSlotController;

    private bool _highlighted = false;
    private bool _showingOptions = false;

    /*
     * The following fields and FixedUpdate solve the following problem:
     *  - The flow of select/deselect is that when you click away from a gameObject, deselect is called BEFORE select
     *  - since deselect is called before select, we don't know if the user is selecting us a second time
     *  - to click post-selection buttons "play" and "?" the user MUST select the card twice
     *  - without this fix, attempting to select "play" or "?" causes the buttons to be hid and reappear, with no clickEvent for the button to register
     *  - with this fix, it takes 6 frames to deselect an object, which does go through if the object we reselect in that time is us, again
     */
    protected bool __evilToldToDeselect = false;
    protected int __evilDeselectionDelay = -1;


    public void FixedUpdate()
    {
        if (!(__evilDeselectionDelay == -1))
        {
            __evilDeselectionDelay -= 1;
            if (__evilDeselectionDelay <= 0 || !__evilToldToDeselect)
            {
                if(__evilToldToDeselect == true)
                {
                    HideOptions();
                    __evilDeselectionDelay = -1;
                }
            }
        }
    }

    void Awake()
    {
        _effectSlotController = effectSlots.GetComponent<EffectSlotController>();
    }

    /* Plays the current card */
    public void PlayCard()
    {
        if (GameState.Meta.activeEncounter.Value == null)
        {
            Debug.LogError("Played card when no encounter was active");
        }
        else
        {
            GameState.Meta.activeEncounter.Value.PlayCard(_position);
        }
        
    }

    public void DisplayEffect(IExecutableEffect effect)
    {
        _effectSlotController.DisplayEffect(effect);
    }

    public void ClearEffects()
    {
        _effectSlotController.Clear();
    }

    public bool CurrentlyShowingOptions()
    {
        return _showingOptions;
    }

    public void SetDefaultCompliance(int defaultCompliance)
    {
        _defaultCompliance = defaultCompliance;
        SetCompliance(_defaultCompliance);
    }

    public void SetDefaultPatience(int defaultPatience)
    {
        _defaultPatience = defaultPatience;
        SetPatience(_defaultPatience);
    }

    public void SetCompliance(int compliance)
    {
        string complianceAsString = compliance.ToString();
        visibleCompliance.text = complianceAsString;

        if (compliance > _defaultCompliance)
        {
            visibleCompliance.color = Color.green;
        }
        else if (compliance < _defaultCompliance)
        {
            visibleCompliance.color = Color.red;
        }
        else
        {
            visibleCompliance.color = Color.black;
        }
    }


    public void SetPatience(int patience)
    {
        string patienceAsString = patience.ToString();
        visiblePatience.text = patienceAsString;

        if (patience > _defaultPatience)
        {
            visiblePatience.color = Color.red;
        }
        else if (patience < _defaultPatience)
        {
            visiblePatience.color = Color.green;
        }
        else
        {
            visiblePatience.color = Color.black;
        }
    }

    public void SetCardName(string name)
    {
        cardName.text = name;
    }

    public void SetCardDescription(string description)
    {
        cardDescription.text = description;
    }

    public void SetPosition(int idx)
    {
        _position = idx;
    }

    public int GetPosition()
    {
        return _position;
    }

    public bool IsHighlighted()
    {
        return _highlighted;
    }

    public void HighlightCard()
    {
        GameState.Meta.activeEncounter.Value.GetEncounterController().HighlightCard(this.gameObject);
        EventSystem.current.SetSelectedGameObject(gameObject);
        _highlighted = true;
        HideOptions();
    }

    public void UnHighlightCard()
    {
        GameState.Meta.activeEncounter.Value.GetEncounterController().UnHighlightCard();
        _highlighted = false;
        Debug.Log("Unhighlighted");
    }

    public void ShowOptions()
    {
        cardRect.color = selectionTint;
        EventSystem.current.SetSelectedGameObject(gameObject);
        options.SetActive(true);
        _showingOptions = true;
    }

    public void HideOptions()
    {
        cardRect.color = Color.white;
        options.SetActive(false);
        _showingOptions = false;
    }

    public void OnPointerClick(PointerEventData eventData) 
    {
        __evilToldToDeselect = false;
        if (!_highlighted)
        {
            ShowOptions();
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (_highlighted)
        {
            UnHighlightCard();
        }
        else
        {
            __evilToldToDeselect = true;
            __evilDeselectionDelay = 6;
        }
    }
}
