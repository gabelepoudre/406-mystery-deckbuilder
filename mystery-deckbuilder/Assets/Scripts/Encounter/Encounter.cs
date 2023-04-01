
/*
 * author(s): Gabriel LePoudre, William Metivier
 * 
 * The class that acts as the model for the current encounter, and uses the PrefabController
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class Encounter
{
    /* Static method to launch an Encounter from wherever as long as you have a vblid config */
    public static Encounter StartEncounter(EncounterConfig config, bool force = false)
    {
        if (GameState.Meta.activeEncounter.Value != null)
        {
            Debug.LogError("Tried to initialize an encounter while one was active");
            return null;
        }
        else if (GameState.Player.dailyDeck.Value.Count == 0)
        {
            // launch ENCOUNTER DISALLOWED popup
            GameObject prefabReference = GameObject.Find("EncounterController").GetComponent<PrefabRefs>().RejectEncounter;
            GameObject instantiated = GameObject.Instantiate(prefabReference);
            return null;
        }
        else if (GameState.Player.dailyDeck.Value.Count < 10 && !force)
        {
            // launch ARE YOU SURE popup
            GameObject prefabReference = GameObject.Find("EncounterController").GetComponent<PrefabRefs>().AreYouSure; ;
            GameObject instantiated = GameObject.Instantiate(prefabReference);
            AreYouSureController popController = instantiated.GetComponent<AreYouSureController>();
            popController.SetDescription("Are you sure you would like to launch this Encounter? You only have " + GameState.Player.dailyDeck.Value.Count + " cards remaining in your deck!");
            popController.SetConfig(config);
            return null;
        }
        else
        {
            Encounter encounter = new Encounter(config);
            GameState.Meta.activeEncounter.Value = encounter;
            return encounter;
        }
    }

    // end of statics

    private GameObject _encounterPrefab;
    private EncounterPrefabController _encounterController;
    private NPC _opponent;
    private List<IExecutableEffect> globalEffects = new();

    private List<Card> _hand = new();

    // statistics
    public StatisticsClass Statistics = new StatisticsClass();

    /* Statistics is used to hold information about the GameState and make it easier to implement conditionals */
    public class StatisticsClass
    {
        public List<int> ListOfPlayedCards { get; set; } = new();
        public int LastPatienceDamage { get; set; } = 0;
        public int LastComplianceDamage { get; set; } = 0;
        public string LastPlayElement { get; set; } = "";
        public int NumberOfPlays { get; set; } = 0;
        public int NumberOfDraws { get; set; } = 0;
        public int IntimidationCardsPlayed { get; set; } = 0;
        public int SympathyCardsPlayed { get; set; } = 0;
        public int PersuasionCardsPlayed { get; set; } = 0;
        public int PreparationCardsPlayed { get; set; } = 0;
        public int ConversationCardsPlayed { get; set; } = 0;
        public int IntimidationCardsInHand { get; set; } = 0;
        public int SympathyCardsInHand { get; set; } = 0;
        public int PersuasionCardsInHand { get; set; } = 0;
        public int PreparationCardsInHand { get; set; } = 0;
        public int ConversationCardsInHand { get; set; } = 0;
        public int NumberCardsInHand 
        { 
            get { return ConversationCardsInHand + PreparationCardsInHand; } 
        }
        public int Patience
        {
            get
            {
                return GameState.Meta.activeEncounter.Value.GetEncounterController().GetPatience();
            }
        }
        public float PatiencePercentOfTotal
        {
            get
            {
                return GameState.Meta.activeEncounter.Value.GetEncounterController().GetPatience() / (float)GameState.Meta.activeEncounter.Value.GetEncounterController().GetMaxPatience();
            }
        }

        public int Compliance
        {
            get
            {
                return GameState.Meta.activeEncounter.Value.GetEncounterController().GetCompliance();
            }
        }

        public float CompliancePercentOfTotal
        {
            get
            {
                return GameState.Meta.activeEncounter.Value.GetEncounterController().GetCompliance() / (float)GameState.Meta.activeEncounter.Value.GetEncounterController().GetMaxCompliance();
            }
        }
    }

    /* Constructor with config */
    public Encounter(EncounterConfig config)
    {
        GameObject prefabReference = GameObject.Find("EncounterController").GetComponent<PrefabRefs>().Encounter;
        Statistics = new StatisticsClass(); // we reinitialize this, just in case

        _encounterPrefab = GameObject.Instantiate(prefabReference);

        _encounterController = _encounterPrefab.GetComponent<EncounterPrefabController>();

        _encounterController.Initialize(config);

        _opponent = config.Opponent;
    }

    public NPC GetOpponent()
    {
        return _opponent;
    }

    public void RecalculateHandStatistics()
    {
        Statistics.IntimidationCardsInHand = 0;
        Statistics.PersuasionCardsInHand = 0;
        Statistics.PreparationCardsInHand = 0;
        Statistics.SympathyCardsInHand = 0;
        Statistics.ConversationCardsInHand = 0;
        foreach (Card card in GetHand())
        {
            string element = "";
            if (card.ElementOverridden)
            {
                element = card.ElementOverride;
            }
            else
            {
                element = card.GetElement();
            }

            switch (element)
            {
                case "Intimidation":
                    Statistics.IntimidationCardsInHand += 1;
                    Statistics.ConversationCardsInHand += 1;
                    break;

                case "Sympathy":
                    Statistics.SympathyCardsInHand += 1;
                    Statistics.ConversationCardsInHand += 1;
                    break;

                case "Persuasion":
                    Statistics.PersuasionCardsInHand += 1;
                    Statistics.ConversationCardsInHand += 1;
                    break;

                case "Preparation":
                    Statistics.PreparationCardsInHand += 1;
                    break;
            }
        }
    }

    public void ForceCardInHand(int cardID)
    {
        Card draw = (Card)Cards.CreateCardWithID(cardID);
        RecalculateHandStatistics();

        _hand.Add(draw);
        _encounterController.PlaceCard(draw);

        OnChange(); // we call this on all draws and plays
    }

    /* Draw a card, if we can. Draws trigger "OnChange" which recalculates all card values */
    public void DrawCard(int patienceCost)
    {
        if (_encounterController.PlaceMatFull())
        {
            Debug.Log("Can't draw, there is no place for the card");
        }
        else if (GameState.Player.dailyDeck.Value.Count == 0)
        {
            Debug.Log("Daily deck is empty!");
        }
        else
        {
            Debug.Log("Drawing a card");
            int draw_idx = -1;
            if (GameState.Meta.currentGameplayPhase.Value == GameState.Meta.GameplayPhases.Tutorial)
            {
                draw_idx = 0;
            }
            else
            {
                draw_idx = Mathf.RoundToInt((Random.value * (GameState.Player.dailyDeck.Value.Count - 1)));
            }

            int draw_value = GameState.Player.dailyDeck.Value[draw_idx];
            GameState.Player.dailyDeck.Value.RemoveAt(draw_idx);
            GameState.Player.dailyDeck.Raise();  // we manually raise the change because list changes are too deep to be registered automatically

            Card draw = (Card)Cards.CreateCardWithID(draw_value);
            if (draw == null)
            {
                Debug.LogError("Drew a card with an invalid index");
            }
            else
            {
                Statistics.NumberOfDraws += 1;
                _hand.Add(draw);
                RecalculateHandStatistics();
                _encounterController.PlaceCard(draw);
                
                OnChange(); // we call this on all draws and plays
                draw.OnDraw();

                bool continueGame = _encounterController.SetAndCheckPatience(_encounterController.GetPatience() - patienceCost);
                if (!continueGame)
                {
                    EndEncounter(false);
                    return;
                }
                _encounterController.ChangeHeadshotBasedOnPatience();
            }
        }
    }

    /* Resolves global effects, as part of OnChange */
    private void ResolveGlobals()
    {
        if (globalEffects.Count == 0)  // we add this here because the NPC isn't done initializing on definition for some reason
        {
            if (_opponent.affinity_intimidation > 0.5f)
            {
                globalEffects.Add(new EElementResistance("Intimidation", 0.5f));
            }
            else if (_opponent.affinity_intimidation < 0.5f)
            {
                globalEffects.Add(new EElementWeakness("Intimidation", 0.5f));
            }

            if (_opponent.affinity_sympathy > 0.5f)
            {
                globalEffects.Add(new EElementResistance("Sympathy", 0.5f));
            }
            else if (_opponent.affinity_sympathy < 0.5f)
            {
                globalEffects.Add(new EElementWeakness("Sympathy", 0.5f));
            }

            if (_opponent.affintiy_persuasion > 0.5f)
            {
                globalEffects.Add(new EElementResistance("Persuasion", 0.5f));
            }
            else if (_opponent.affintiy_persuasion < 0.5f)
            {
                globalEffects.Add(new EElementWeakness("Persuasion", 0.5f));
            }
        }
        List<IExecutableEffect> toRemove = new(); 
        foreach (IExecutableEffect e in globalEffects)
        {
            Debug.Log(e.GetName());
            Debug.Log(e.GetTerminationPlay());
            Debug.Log(Statistics.NumberOfPlays);
            if (e.GetTerminationPlay() <= Statistics.NumberOfPlays || e.ForceTermination())
            {
                toRemove.Add(e);
            }
            else
            {
                e.Execute();
            }
        }
        foreach(IExecutableEffect e in toRemove)
        {
            globalEffects.Remove(e);
        }
    }

    /* Reset and recalculate all cards on any change (draw or play) */
    private void OnChange()
    {
        foreach (Card c in _hand)
        {
            c.Clear();
        }
        ResolveGlobals();
        // wipe all card stuff, resolve all cards on change
        foreach (Card c in _hand)
        {
            c.OnChange();
        }
        if (GameState.Player.dailyDeck.Value.Count == 0 && Statistics.NumberCardsInHand == 0)
        {
            Debug.Log("Ended encounter because player had no cards left");
            EndEncounter(false);
        }
        
    }

    /* Play a card given it's position on the board (stored internally to the card class if Initialized properly)*/
    public void PlayCard(int position)
    {
        // find card
        Card card = null;

        foreach(Card c in _hand)
        {
            if (c.GetPosition() == position)
            {
                card = c;
            }
        }
        if (card == null)
        {
            Debug.Log("Card at position " + position.ToString() + "could not be found in deck");
        }

        // quick statistics
        string element = "";
        if (card.ElementOverridden)
        {
            element = card.ElementOverride;
        }
        else
        {
            element = card.GetElement();
        }

        switch (element)
        {
            case "Intimidation":
                Statistics.NumberOfPlays += 1;
                Statistics.LastPlayElement = "Intimidation";
                Statistics.IntimidationCardsPlayed += 1;
                Statistics.ConversationCardsPlayed += 1;
                break;
            case "Sympathy":
                Statistics.NumberOfPlays += 1;
                Statistics.LastPlayElement = "Sympathy";
                Statistics.SympathyCardsPlayed += 1;
                Statistics.ConversationCardsPlayed += 1;
                break;
            case "Persuasion":
                Statistics.NumberOfPlays += 1;
                Statistics.LastPlayElement = "Persuation";
                Statistics.PersuasionCardsPlayed += 1;
                Statistics.ConversationCardsPlayed += 1;
                break;
            case "Preparation":
                Statistics.NumberOfPlays += 1;
                Statistics.PreparationCardsPlayed += 1;
                Statistics.ConversationCardsPlayed += 1;
                break;
        }
        GameState.Meta.activeEncounterLastCardPlayedElement.Value = element;
        
        Statistics.ListOfPlayedCards.Add(card.GetId());

        int totalCompliance = card.GetTotalCompliance();
        int totalPatience = card.GetTotalPatience();
        Statistics.LastComplianceDamage = totalCompliance;
        Statistics.LastPatienceDamage = totalPatience;

        _hand.Remove(card);
        bool continueGame = _encounterController.SetAndCheckCompliance(_encounterController.GetCompliance() + totalCompliance);
        if (!continueGame)
        {
            EndEncounter(true);
            return;
        }

        continueGame = _encounterController.SetAndCheckPatience(_encounterController.GetPatience() - totalPatience);
        if (!continueGame)
        {
            EndEncounter(false);
            return;
        }
        _encounterController.ChangeHeadshotBasedOnPatience();

        // remove from hand for now, we don't want to apply effects to a played card
        RecalculateHandStatistics();

        // remove card
        _encounterController.RemoveCard(card);
        card.OnPlay();
        OnChange(); // we call this on all draws and plays

        GameState.Meta.activeEncounterPatienceDroppedByAmount.Value = totalPatience;
        GameState.Meta.activeEncounterComplianceRaisedByAmount.Value = totalCompliance;
    }

    /* Exposes the controller */
    public EncounterPrefabController GetEncounterController()
    {
        return _encounterController;
    }

    /* Exposes the hand (used mostly in conditionals) */
    public List<Card> GetHand()
    {
        return _hand;
    }

    public void AddGlobal(IExecutableEffect e)
    {
        globalEffects.Add(e);
    }

    public void InsertGlobal(IExecutableEffect e, int idx)
    {
        globalEffects.Insert(idx, e);
    }

    /* Note- Doesn't actually destroy the encounter anymore*/
    public void EndEncounter(bool victory)
    {
        foreach (Card c in _hand)
        {
            GameState.Player.dailyDeck.Value.Add(c.GetId());

        }
        GameState.Player.dailyDeck.Raise();
        if (victory)
        {
            _encounterController.DisplayYouWonScreen();
            GameState.Meta.activeEncounterInWinScreen.Value = true;
        }
        else
        {
            _encounterController.DisplayYouLostScreen();
            GameState.Meta.activeEncounterInLossScreen.Value = true;
        }
    }

    public void DestroyEncounter(bool victory)
    {
        GameState.Meta.lastEncounterEndedInVictory.Value = victory;
        GameState.Meta.activeEncounter.Value = null;

        //update State data
        if (victory)
        {
            GameState.NPCs.npcNameToEncountersWon[GameState.NPCs.lastNPCSpokenTo].Value += 1;
            GameState.Meta.activeEncounterInWinScreen.Value = false;
        }
        else
        {
            GameState.Meta.activeEncounterInLossScreen.Value = false;
        }
        GameState.NPCs.npcNameToEncountersCompleted[GameState.NPCs.lastNPCSpokenTo].Value += 1;
        

        GameObject.Destroy(_encounterPrefab);
    }
}


/* An effect (as seen by E prefix) for an NPC weakness */
public class EElementWeakness : Effect, IExecutableEffect
{
    private string _element;

    private Color _color = new Color(50 / 255, 255 / 255, 50 / 255); // displayed on cards

    private float _mod;
    private string _name = "Element Weakness";
    private string _desc_1 = "Your opponent is susceptible to ";
    private string _desc_2 = "Increase Compliance by ";

    public EElementWeakness(string element, float mod) : base(99)
    {
        _element = element;
        _mod = mod;
    }

    public string GetDescription()
    {
        // string formatter? I barely know her!
        return _desc_1 + _element + "! " + _desc_2 + (_mod * 100).ToString() + "%";
    }

    public string GetName() { return _name; }
    public Color GetColor() { return _color; }

    /* Executes the effect. A conditional may be called within */
    public void Execute()
    {
        Encounter enc = GameState.Meta.activeEncounter.Value;
        List<Card> hand = enc.GetHand();
        foreach (Card c in hand)
        {
            if (c.ElementOverridden)
            {
                if (c.ElementOverride == _element)
                {
                    c.StackableComplianceMod += 0.5f;
                    c.DisplayEffect(this);
                }
                else
                {
                    return;
                }
            }
            else if (c.GetElement() == _element)
            {
                c.StackableComplianceMod += 0.5f;
                c.DisplayEffect(this);
            }
        }
    }
}

/* An effect (as seen by E prefix) for an NPC strength */
public class EElementResistance : Effect, IExecutableEffect
{
    private string _element;

    private Color _color = new Color(255 / 255, 50 / 255, 50 / 255); // displayed on cards

    private float _mod;
    private string _name = "Element Resistance";
    private string _desc_1 = "Your opponent is resistant against ";
    private string _desc_2 = "Reduce Compliance by ";

    public EElementResistance(string element, float mod) : base(99)
    {
        _element = element;
        _mod = mod;
    }

    public string GetDescription()
    {
        // string formatter? I barely know her!
        return _desc_1 + _element + "! " + _desc_2 + (-(_mod) * 100).ToString() + "%";
    }

    public string GetName() { return _name; }
    public Color GetColor() { return _color; }

    /* Executes the effect. A conditional may be called within */
    public void Execute()
    {
        Encounter enc = GameState.Meta.activeEncounter.Value;
        List<Card> hand = enc.GetHand();
        foreach (Card c in hand)
        {
            if (c.ElementOverridden)
            {
                if (c.ElementOverride == _element)
                {
                    c.StackableComplianceMod -= 0.5f;
                    c.DisplayEffect(this);
                }
                else
                {
                    return;
                }
            }
            else if (c.GetElement() == _element)
            {
                c.StackableComplianceMod -= 0.5f;
                c.DisplayEffect(this);
            }
        }
    }
}