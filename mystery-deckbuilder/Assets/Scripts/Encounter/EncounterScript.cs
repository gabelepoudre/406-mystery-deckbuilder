using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * script for the encounter gameobject
 * to use:
 *      encounter = Instantiate(encounter);
        encounter.GetComponent<EncounterScript>().StartEncounter()
 */
public class EncounterScript : MonoBehaviour
{
    [SerializeField] private int _threshold;
    [SerializeField] private int _compliance;
    [SerializeField] private int _patience;

    [SerializeField] private string _weakness; //using strings because that is how element is stored in the card class
    [SerializeField] private string _resistance; // !!!CARD CLASS AND NPC CLASS STRINGS MUST MATCH!!! 

    [SerializeField] private int _handSize = 5;
    private int playCount = 0;

    private int maxPatience = 10; //Not sure if this will ever change, but the option is there

    private List<int> deck = new List<int>(); //reference to a list of card id numbers that make up the deck 
    private List<int> handBackend = new List<int>(); //purgatory state between being in the deck and being discarded.
    private List<int> discard = new List<int>(); //list of cards that have been played and removed from the deck

    private List<ConversationCard> hand = new List<ConversationCard>(); //something weird is going on with all the "hands" I'll sort it out later
    private Dictionary<GameObject, ConversationCard> handFrontend = new Dictionary<GameObject, ConversationCard>(); //container to hold instantiated card objects
    private List<List<int>> filters = new List<List<int>>(); //experimental coolness

    private Sprite NpcSprite; //TODO: a sprite reference will need to be passed in in order to display who the player is in an encounter with

    public GameObject patienceBar;
    public GameObject complianceBar;

    public GameObject playField;
    private Dictionary<Transform, bool> locations = new Dictionary<Transform, bool>(); //<spawn location, location is occupied>

    public GameObject redCard;
    public GameObject blueCard;
    public GameObject greenCard;
    public GameObject prepCard;

    /**
     * initializes the ecounter. 
     * more parameters may be added as their implementaion is solidified
     */
    public void StartEncounter(int complianceThreshold, int startingCompliance, int startingPatience, string NpcWeakness, string NpcResistance)
    {
        GameState.Meta.activeEncounter.Value = this;

        _threshold = complianceThreshold;
        _compliance = startingCompliance;
        _patience = startingPatience;
        SetInitialBarValues();

        _weakness = NpcWeakness;
        _resistance = NpcResistance;

        InitializePlayArea();

        InitializeCards();
    }
    private void InitializePlayArea()
    {
        Transform[] tempArray = playField.GetComponentsInChildren<Transform>();
        foreach(Transform i in tempArray)
        {
            locations.Add(i, false);
        }
        locations[playField.transform] = true;
    }
    private void InitializeCards()
    {
        deck = GameState.CardInfo.currentDeck.Value;
        discard = GameState.CardInfo.currentDiscard.Value;

        AddFilter(-1, 1); //adding element weakness/resistance, -1 durration meens they will last forever
        AddFilter(-1, 2);

        _patience += _handSize; //so you still start with full patience
        for (int i = 0; i < _handSize; i++)
        {
            DrawCard();
        }
    }

    /**
     * initializes the bars
     */
    private void SetInitialBarValues()
    {
        patienceBar.GetComponent<BarScript>().Initialize(maxPatience, _patience);
        complianceBar.GetComponent<BarScript>().Initialize(_threshold, _compliance);
    }
    
    public void SetPatience(int val)
    {
        _patience = val;
        patienceBar.GetComponent<BarScript>().SetValue(_patience);
    }

    public int GetPatience()
    {
        return _patience;
    }

    /**
     * patience will never go above max
     */
    public void IncPatience(int inc)
    {
        if (_patience + inc <= maxPatience)
        {
            _patience += inc;
        }
        else
        {
            _patience = maxPatience;
        }
        patienceBar.GetComponent<BarScript>().SetValue(_patience);
    }

    public void DecPatience(int dec)
    {
        _patience -= dec;
        patienceBar.GetComponent<BarScript>().SetValue(_patience);
        if (_patience <= 0)
        {
            EndEncounter(false);
        }
    }

    public void SetCompliance(int val)
    {
        _compliance = val;
        complianceBar.GetComponent<BarScript>().SetValue(_compliance);
    }

    public int GetCompliance()
    {
        return _compliance;
    }

    public void IncCompliance(int inc)
    {
        _compliance += inc;
        complianceBar.GetComponent<BarScript>().SetValue(_compliance);
        if (_compliance >= _threshold)
        {
            EndEncounter(true);
        }
    }

    /**
     * compliance value cannot go bellow 0
     */
    public void DecCompliance(int dec)
    {
        if (_compliance - dec >= 0)
        {
            _compliance -= dec;
        }
        else
        {
            _compliance = 0;
        }
        complianceBar.GetComponent<BarScript>().SetValue(_compliance);
    }

    public string GetWeakness()
    {
        return _weakness;
    }

    public string GetResistance()
    {
        return _resistance;
    }

    public void DrawCard()
    {
        Transform place = null;
        
        foreach (KeyValuePair<Transform, bool> i in locations)
        {
            //Debug.Log("for");
            if (!i.Value)
            {
                //Debug.Log("if");
                place = i.Key;
                break;
            }
        }
        
        if (place == null) //if there is no space for new cards they can't draw (hopefully)
        {
            return;
        }

        locations[place] = true;

        DecPatience(1); //draw patience cost, happens after card draw is successful

        int cardID = deck[0];
        deck.Remove(cardID);
        handBackend.Add(cardID);

        ConversationCard card = (ConversationCard)Cards.CreateCardWithID(cardID);
        hand.Insert(0, card);

        card.SetTransform(place);

        GameObject frontendCard = null;
        switch (hand[0].GetElement())
        {
            case "Intimidation":
                frontendCard = Instantiate(redCard, place.position, place.rotation, playField.transform);
                handFrontend.Add(frontendCard, card); ; //TODO mess around with instantiation so that the cards get placed on the field nicely
                break;
            case "Sympathy":
                frontendCard = Instantiate(blueCard, place.position, place.rotation, playField.transform);
                handFrontend.Add(frontendCard, card);
                break;
            case "Persuasion":
                frontendCard = Instantiate(greenCard, place.position, place.rotation, playField.transform);
                handFrontend.Add(frontendCard, card);
                break;
            case "Preperation":
                frontendCard = Instantiate(redCard, place.position, place.rotation, playField.transform);
                handFrontend.Add(frontendCard, card);
                break;
        }

        Text[] textFields = frontendCard.GetComponentsInChildren<Text>(); // TODO navigate unity's awful gameobject heirarchy to put text values in the correct spot

        foreach (Text i in textFields)
        {
            if (i.CompareTag("Card Name"))
            {
                i.text = card.GetName();
            }
            else if (i.CompareTag("Card Description"))
            {
                i.text = card.GetDescription();
            }
            else if (i.CompareTag("Patience"))
            {
                i.text = card.GetPatienceValue().ToString();
            }
            else if (i.CompareTag("Compliance"))
            {
                i.text = card.GetComplianceValue().ToString();
            }
        }



        UpdateCards();
    }

    public void PlayCard(int ID, GameObject card)
    {
        playCount++;
        handBackend.Remove(ID);
        discard.Add(ID);

        locations[handFrontend[card].GetTransform()] = false; //I'm going insane
        hand.Remove(handFrontend[card]);
        handFrontend.Remove(card);
        Destroy(card);

        UpdateCards();

        foreach (List<int> i in filters) //removes filters as they expire
        {
            i[0]--;
            if(i[0] == 0)
            {
                filters.Remove(i);
            }
        }
    }

    public void UpdateCards()
    {
        
        foreach (KeyValuePair<GameObject, ConversationCard> i in handFrontend)
        {
            int[] modifyAmounts = (int[])ResolveFilters(i.Value); //[0] = patience modifier, [1] = compliance modifier

            Text[] textFields = i.Key.GetComponentsInChildren<Text>();

            foreach (Text j in textFields)
            {
                
                if (j.CompareTag("Patience"))
                {
                    j.text = (i.Value.GetPatienceValue() + modifyAmounts[0]).ToString();
                    if(i.Value.GetPatienceValue() + modifyAmounts[0] > i.Value.GetPatienceValue())
                    {
                        j.color = Color.green;
                    }
                    else if(i.Value.GetPatienceValue() + modifyAmounts[0] < i.Value.GetPatienceValue())
                    {
                        j.color = Color.red;
                    }
                    else
                    {
                        j.color = Color.black;
                    }
                    
                }
                else if (j.CompareTag("Compliance"))
                {
                    j.text = (i.Value.GetComplianceValue() + modifyAmounts[1]).ToString();
                    if (i.Value.GetComplianceValue() + modifyAmounts[1] > i.Value.GetComplianceValue())
                    {
                        j.color = Color.green;
                    }
                    else if (i.Value.GetComplianceValue() + modifyAmounts[1] < i.Value.GetComplianceValue())
                    {
                        j.color = Color.red;
                    }
                    else
                    {
                        j.color = Color.black;
                    }
                }
            }

        }
    }

    /** 
     * card object passes the id of the static object that calculates it's effect and the durration of how long that effect lingers.
     * "local" effects can just be duration 1 or something
     * this implementation allows cards to pass their lingering effects without them disapearing when the card is destroyed
     * 
     */
    public void AddFilter(int duration, int id)
    {
        //duration + playCount will be the play that the effect expires
        List<int> filter = new List<int>() {duration + playCount, id };
        filters.Add(filter);
    }

    public Array ResolveFilters(ConversationCard card)
    {
        
        int[] modifyAmounts = new int[] {0, 0}; //[0] = patience modifier, [1] = compliance modifier

        foreach (List<int> i in filters)
        {
            int[] tempArray = (int[])Filters.GetFilterByID(i[1], card.GetComplianceValue(), card.GetPatienceValue(), card.GetElement());
            modifyAmounts[0] += tempArray[0];
            modifyAmounts[1] += tempArray[1];
        }
        
        return modifyAmounts; 
    }

    /**
     * can send the win/lose result, but there is no one to send it to right now
     */
    private void EndEncounter(bool victory)
    {
        Destroy(this.gameObject);
    }
}
