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

    private List<Card> hand = new List<Card>(); //something weird is going on with all the "hands" I'll sort it out later (probably not)
    private Dictionary<GameObject, Card> handFrontend = new Dictionary<GameObject, Card>(); //container to hold instantiated card objects
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
    public void StartEncounter(int complianceThreshold, int startingCompliance, int startingPatience, string npcWeakness, string npcResistance)
    {
        if(GameState.Meta.activeEncounter.Value != null) 
        {
            Debug.LogError("There is already an avtive Encounter");
            Destroy(this.gameObject);
            return;
        }
        GameState.Meta.activeEncounter.Value = this;

        _threshold = complianceThreshold;
        _compliance = startingCompliance;
        _patience = startingPatience;
        SetInitialBarValues();

        _weakness = npcWeakness;
        _resistance = npcResistance;

        InitializePlayArea();

        InitializeCards();
        SetPatience(startingPatience); //Initialize cards uses DrawCard to draw the starting hand, DrawCard increments patience, this resets it to where it should be
    }
    /**
     * creates a colection of transforms that cards can be instantiated at
     */
    private void InitializePlayArea()
    {
        Transform[] tempArray = playField.GetComponentsInChildren<Transform>();
        foreach(Transform i in tempArray)
        {
            locations.Add(i, false);
        }
        locations[playField.transform] = true;
    }
    /**
     * sets up the deck and discard pile
     * draws the first hand
     */
    private void InitializeCards()
    {
        deck = GameState.Player.currentDeck.Value;
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
     * Patience is stored in a format where values from the same location could be either positive or negative. 
     * it makes sense to have one funtion rather than having additional checks to determine which function to use
     * 
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

    /**
     * Compliance is stored in a format where values from the same location could be either positive or negative. 
     * it makes sense to have one funtion rather than having additional checks to determine which function to use
     * 
     * compliance value cannot go bellow 0
     */
    public void IncCompliance(int inc)
    {
        if (_compliance + inc >= 0)
        {
            _compliance += inc;
        }
        else
        {
            _compliance = 0;
        }
        complianceBar.GetComponent<BarScript>().SetValue(_compliance);
        complianceBar.GetComponent<BarScript>().SetValue(_compliance);
        if (_compliance >= _threshold)
        {
            EndEncounter(true);
        }
    }

    public string GetWeakness()
    {
        return _weakness;
    }

    public string GetResistance()
    {
        return _resistance;
    }

    /**
     * checks if there is a card to draw
     * finds a place for the card
     * updates backend
     * creates card object
     * intsatiates prefab
     * sets prefab to match card object
     */
    public void DrawCard()
    {
        if (deck.Count == 0) //if you are out of cards, you can't draw more
        {
            return;
        }

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

        IncPatience(-1); //draw patience cost, happens after card draw is successful

        
        int cardID = deck[0];
        deck.Remove(cardID);
        handBackend.Add(cardID);

        Card card = (Card)Cards.CreateCardWithID(cardID);
        hand.Insert(0, card);

        card.SetTransform(place);

        GameObject frontendCard = null;
        switch (hand[0].GetElement())
        {
            case "Intimidation":
                frontendCard = Instantiate(redCard, place.position, place.rotation, playField.transform);  //TODO mess around with instantiation so that the cards get placed on the field nicely (only if the current 5-slot method is not sufficient)
                handFrontend.Add(frontendCard, card);
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

        Text[] textFields = frontendCard.GetComponentsInChildren<Text>(); 
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

    /**
     * Represents the act of playing a card
     * called from the card prefab script
     */
    public void PlayCard(GameObject card) //BUG (bandaided) seems to want to complete the call stack even after the object is destroyed, throws an exception
    {
        int ID = handFrontend[card].GetId();
        playCount++;
        handBackend.Remove(ID);
        discard.Add(ID);

        Text[] textFields = card.GetComponentsInChildren<Text>(); 

        foreach (Text i in textFields) 
        {
            if (i.CompareTag("Compliance"))
            {
                IncCompliance(int.Parse(i.text));
            }
            else if (GameState.Meta.activeEncounter.Value == null) //check so that the call stack doesn't run away after Encounter is destroyed
            {
                return;
            }
            else if (i.CompareTag("Patience"))
            {
                IncPatience(int.Parse(i.text));
            }
        }

        if (GameState.Meta.activeEncounter.Value == null) //check so that the call stack doesn't run away after Encounter is destroyed
        {
            return;
        }

        handFrontend[card].Execute();

        locations[handFrontend[card].GetTransform()] = false; //I'm going insane
        hand.Remove(handFrontend[card]);
        handFrontend.Remove(card);
        Destroy(card);

        
        for (int i = 0; i < filters.Count; i++) //removes filters as they expire
        {
            if (filters[i][0] == 0)
            {
                filters.RemoveAt(i);
            }
            else
            {
                filters[i][0]--;
            }
        }

        UpdateCards();
    }

    /**
     * Updates the visible card objects 
     * changes their patience and complience values
     * changes the text colour to show relation to base value
     */
    private void UpdateCards()
    {
        
        foreach (KeyValuePair<GameObject, Card> i in handFrontend) //looks like an optimization problem, but N of handFrontend is max 5 and N of textFields is max 4
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
     * card object passes the id of the static object that calculates it's effect and the duration of how long that effect lingers.
     * duration -1 (or any negative number) will persist indefinitly 
     * "local" effects can just be duration 1 or something
     * this implementation allows cards to pass their lingering effects without them disapearing when the card is destroyed
     * 
     */
    public void AddFilter(int duration, int id)
    {
        List<int> filter = new List<int>() {duration, id };
        filters.Add(filter);
    }

    private Array ResolveFilters(Card card)
    {
        
        int[] modifyAmounts = new int[] {0, 0}; //[0] = patience modifier, [1] = compliance modifier

        foreach (List<int> i in filters)
        {
            int[] tempArray = (int[])Filters.GetFilterByID(i[1], card);
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
        deck.AddRange(handBackend); //puts unused cards back into deck
        GameState.Player.currentDeck.Value = deck;
        GameState.CardInfo.currentDiscard.Value = discard;
        GameState.Meta.activeEncounter.Value = null;

        //NOTE: this is just a temporary thing until we implement a proper system for initiating post-encounter dialogues
        //TODO: remove this after implementing a proper system for initiating post-encounter dialogues
        GameObject.Find("Nibbles").transform.GetComponent<NibblesDialogueTrigger>().StartEndOfEncounterDialogue();

        Destroy(this.gameObject);
    }
}
