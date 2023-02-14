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

    [SerializeField] private int _handSize = 3;
    private int playCount = 0;

    private int maxPatience = 10; //Not sure if this will ever change, but the option is there

    private List<int> deck = new List<int>(); //reference to a list of card id numbers that make up the deck 
    private List<int> handBackend = new List<int>(); //purgatory state between being in the deck and being discarded.
    private List<int> discard = new List<int>(); //list of cards that have been played and removed from the deck

    private List<ConversationCard> hand = new List<ConversationCard>(); //something weird is going on with all the "hands" I'll sort it out later
    private List<GameObject> handFrontend = new List<GameObject>(); //container to hold instantiated card objects
    private List<List<int>> filters = new List<List<int>>(); //experimental coolness

    private Sprite NpcSprite; //TODO: a sprite reference will need to be passed in in order to display who the player is in an encounter with

    public GameObject patienceBar;
    public GameObject complianceBar;

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

        InitializeCards();
    }
    
    private void InitializeCards()
    {
        deck = GameState.CardInfo.currentDeck.Value;
        discard = GameState.CardInfo.currentDiscard.Value;

        AddFilter(-1, 1); //adding element weakness/resistance, -1 durration meens they will last forever
        AddFilter(-1, 2);

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
        int cardID = deck[0];
        deck.Remove(cardID);
        handBackend.Add(cardID);

        hand.Insert(0, (ConversationCard)Cards.CreateCardWithID(cardID));
        switch (hand[0].GetElement())
        {
            case "Intimidation":
                handFrontend.Insert(0, Instantiate(redCard)); //TODO mess around with instantiation so that the cards get placed on the field nicely
                break;
            case "Sympathy":
                handFrontend.Insert(0, Instantiate(blueCard));
                break;
            case "Persuasion":
                handFrontend.Insert(0, Instantiate(greenCard));
                break;
        }
        Text[] textFields = handFrontend[0].GetComponentsInChildren<Text>(); // TODO navigate unity's awful gameobject heirarchy to put text values in the correct spot



        UpdateCards();
    }

    public void PlayCard(int ID)
    {
        playCount++;
        handBackend.Remove(ID);
        discard.Add(ID);

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
        
        foreach (ConversationCard i in hand)
        {
            int[] modifyAmounts = (int[])ResolveFilters(i); //TODO more putting text values in spots
            
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
