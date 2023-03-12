
/*
 * author(s): Gabriel LePoudre, William Metivier
 * 
 * The class that acts as the model for the current encounter, and uses the PrefabController
 * 
 */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Encounter
{
    /* Static method to launch an Encounter from wherever as long as you have a vblid config */
    public static Encounter StartEncounter(EncounterConfig config)
    {
        if (GameState.Meta.activeEncounter.Value != null)
        {
            Debug.LogError("Tried to initialize an encounter while one was active");
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
    private List<IExecutableEffect> globalEffects = new();

    private List<Card> _hand = new();

    // statistics
    public StatisticsClass Statistics = new StatisticsClass();

    /* Statistics is used to hold information about the GameState and make it easier to implement conditionals */
    public class StatisticsClass
    {
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
        public int Patience
        {
            get
            {
                return GameState.Meta.activeEncounter.Value.GetEncounterController().GetPatience();
            }
        }
        public int Compliance
        {
            get
            {
                return GameState.Meta.activeEncounter.Value.GetEncounterController().GetCompliance();
            }
        }
    }

    /* Constructor with config */
    public Encounter(EncounterConfig config)
    {
        GameObject prefabReference = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Encounter/Encounter.prefab");
        Statistics = new StatisticsClass(); // we reinitialize this, just in case

        _encounterPrefab = GameObject.Instantiate(prefabReference);

        _encounterController = _encounterPrefab.GetComponent<EncounterPrefabController>();

        _encounterController.Initialize(config);

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

            int draw_idx = Mathf.RoundToInt((Random.value * (GameState.Player.dailyDeck.Value.Count-1)));

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
                // quick statistics
                switch (draw.GetElement())
                {
                    case "Intimidation":
                        Statistics.IntimidationCardsInHand += 1;
                        Statistics.ConversationCardsInHand += 1;
                        Statistics.NumberOfDraws += 1;
                        break;
                    case "Sympathy":
                        Statistics.SympathyCardsInHand += 1;
                        Statistics.ConversationCardsInHand += 1;
                        Statistics.NumberOfDraws += 1;
                        break;
                    case "Persuasion":
                        Statistics.PersuasionCardsInHand += 1;
                        Statistics.ConversationCardsInHand += 1;
                        Statistics.NumberOfDraws += 1;
                        break;
                    case "Preparation":
                        Statistics.PreparationCardsInHand += 1;
                        Statistics.NumberOfDraws += 1;
                        break;
                }

                _hand.Add(draw);
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
        if (globalEffects.Count == 0)
        {
            // TODO: REMOVE, this should not be hard coded
            globalEffects.Add(new EElementWeakness("Sympathy", 0.5f));
            globalEffects.Add(new EElementResistance("Intimidation", 0.5f));
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
        // wipe all card stuff, resolve all cards on change
        foreach (Card c in _hand)
        {
            c.Clear();
            c.OnChange();
        }
        ResolveGlobals();
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
        switch (card.GetElement())
        {
            case "Intimidation":
                Statistics.IntimidationCardsInHand -= 1;
                Statistics.ConversationCardsInHand -= 1;

                Statistics.NumberOfPlays += 1;
                Statistics.IntimidationCardsPlayed += 1;
                Statistics.ConversationCardsPlayed += 1;
                break;
            case "Sympathy":
                Statistics.SympathyCardsInHand -= 1;
                Statistics.ConversationCardsInHand -= 1;

                Statistics.NumberOfPlays += 1;
                Statistics.SympathyCardsPlayed += 1;
                Statistics.ConversationCardsPlayed += 1;
                break;
            case "Persuasion":
                Statistics.PersuasionCardsInHand -= 1;
                Statistics.ConversationCardsInHand -= 1;

                Statistics.NumberOfPlays += 1;
                Statistics.PersuasionCardsPlayed += 1;
                Statistics.ConversationCardsPlayed += 1;
                break;
            case "Preparation":
                Statistics.PreparationCardsInHand -= 1;

                Statistics.NumberOfPlays += 1;
                Statistics.PreparationCardsPlayed += 1;
                Statistics.ConversationCardsPlayed += 1;
                break;
        }

        int totalCompliance = card.GetTotalCompliance();
        int totalPatience = card.GetTotalPatience();

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
        _hand.Remove(card);

        // remove card
        _encounterController.RemoveCard(card);
        card.OnPlay();
        OnChange(); // we call this on all draws and plays
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

    public void EndEncounter(bool victory)
    {
        GameState.Meta.lastEncounterEndedInVictory.Value = victory;
        GameState.Meta.activeEncounter.Value = null;

        //update State data
        if (victory)
        {
            GameState.NPCs.npcNameToEncountersWon[GameState.NPCs.lastNPCSpokenTo].Value += 1;
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
            if (c.GetElement() == _element)
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
            if (c.GetElement() == _element)
            {
                c.StackableComplianceMod -= 0.5f;
                c.DisplayEffect(this);
            }
        }
    }
}