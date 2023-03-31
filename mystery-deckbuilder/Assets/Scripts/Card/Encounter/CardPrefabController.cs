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
using TMPro;

public class CardPrefabController : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private string[] modes = {"PLAY", "HIGHLIGHT" };
    private string currentMode = "PLAY";

    public Image highlight;
    public Image cardBackground;

    public Sprite intimPlayImage;
    public Sprite sympPlayImage;
    public Sprite persPlayImage;
    public Sprite prepPlayImage;
    public Image playButton;

    public Sprite intimHuhImage;
    public Sprite sympHuhImage;
    public Sprite persHuhImage;
    public Sprite prepHuhImage;
    public Image huhButton;

    public CardArtHolder cardArt;
    public TMP_Text cardName;
    public TMP_Text cardDescription;
    public TMP_Text visiblePatience;
    public TMP_Text visibleCompliance;
    public GameObject effectCirclePrefab;
    public GameObject options;
    public GameObject effectSlots;
    [SerializeField] Color selectionTint; 

    private List<GameObject> _effectCircles;  // TODO, we will eventually initialize circles which hover-over to display currently applied effects
    private int _defaultCompliance;
    private int _defaultPatience;
    private int _position;
    private string _element;
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
    public void SetBackground(int card_id)
    {
        cardBackground.sprite = cardArt.GetArtByCardID(card_id);
        switch (GetElement())
        {
            case "Intimidation":
                this.selectionTint = new Color(1, 0.855f, 0.855f);
                this.playButton.sprite = intimPlayImage;
                this.huhButton.sprite = intimHuhImage;
                break;
            case "Sympathy":
                this.selectionTint = new Color(0.874f, 0.889f, 1);
                this.playButton.sprite = sympPlayImage;
                this.huhButton.sprite = sympHuhImage;
                break;
            case "Persuasion":
                this.selectionTint = new Color(0.889f, 1, 0.874f);
                this.playButton.sprite = persPlayImage;
                this.huhButton.sprite = persHuhImage;
                break;
            case "Preparation":
                this.selectionTint = new Color(0.924f, 0.924f, 0.924f);
                this.playButton.sprite = prepPlayImage;
                this.huhButton.sprite = prepHuhImage;
                break;
        }
    }

    public bool GetHighlighted()
    {
        return _highlighted;
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
            visibleCompliance.color = new Color32(0, 255, 0, 255);
        }
        else if (compliance < _defaultCompliance)
        {
            visibleCompliance.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            visibleCompliance.color = new Color32(0, 0, 0, 255);
        }
    }


    public void SetPatience(int patience)
    {
        string patienceAsString = patience.ToString();
        visiblePatience.text = patienceAsString;

        if (patience > _defaultPatience)
        {
            visiblePatience.color = new Color32(255, 0, 0, 255);
        }
        else if (patience < _defaultPatience)
        {
            visiblePatience.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            visiblePatience.color = new Color32(0, 0, 0, 255);
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

    public void SetElement(string element)
    {
        _element = element;
    }

    public string GetElement()
    {
        return _element;
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
        if (!GameState.Meta.activeEncounter.Value.GetEncounterController().HighlightLock)
        {
            _highlighted = false;
            GameState.Meta.activeEncounter.Value.GetEncounterController().UnHighlightCard();
            Debug.Log("Unhighlighted");
        }
    }

    public void ShowOptions()
    {
        cardBackground.color = selectionTint;
        EventSystem.current.SetSelectedGameObject(gameObject);
        options.SetActive(true);
        _showingOptions = true;
    }

    public void HideOptions()
    {
        cardBackground.color = Color.white;
        options.SetActive(false);
        _showingOptions = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_highlighted)
        {
            ShowOptions();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_highlighted)
        {
            HideOptions();
        }
    }
        /*
        switch (currentMode)
        {
            case "PLAY":
                //show play button
                break;
            case "HIGHLIGHT":
                //show ? button
                break;
        }
    }
    public void OnPointerClick(PointerEventData eventData) 
    {
        
        __evilToldToDeselect = false;
        if (!_highlighted)
        {
            ShowOptions();
        }
    }
        */
    public void OnDeselect(BaseEventData eventData)
    {
        if (_highlighted)
        {
            UnHighlightCard();
        }
        /*
        else
        {
            __evilToldToDeselect = true;
            __evilDeselectionDelay = 6;
        }
        */
    }
}
