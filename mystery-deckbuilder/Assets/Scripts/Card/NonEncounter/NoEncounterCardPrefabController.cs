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

public class NoEncounterCardPrefabController : MonoBehaviour, IPointerClickHandler, IDeselectHandler
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

    private int _defaultCompliance;
    private int _defaultPatience;

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

    public void OnPointerClick(PointerEventData eventData) 
    {
        Debug.Log("Card clicked");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Card deselected");
    }

}
