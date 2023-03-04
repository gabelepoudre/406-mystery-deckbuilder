using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Encounter
{
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

    public static void EndEncounter()
    {
        if (GameState.Meta.activeEncounter.Value == null)
        {
            Debug.LogError("Tried to close an encounter when none were active");
        }
        else
        {
            //TODO
        }
    }


    // end of statics

    private GameObject _encounterPrefab;
    private EncounterPrefabController _encounterController;

    private List<Card> _hand = new();

    // statistics
    public StatisticsClass Statistics = new StatisticsClass();
    public class StatisticsClass
    {
        public int NumberOfPlays { get; set; } = 0;
        public int IntimidationCardsPlayed { get; set; } = 0;
        public int SympathyCardsPlayed { get; set; } = 0;
        public int PersuasionCardsPlayed { get; set; } = 0;
        public int PreparationCardsPlayed { get; set; } = 0;

    }
    
    public Encounter(EncounterConfig config)
    {
        GameObject prefabReference = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Encounter/Encounter.prefab");
        _encounterPrefab = GameObject.Instantiate(prefabReference);

        _encounterController = _encounterPrefab.GetComponent<EncounterPrefabController>();

        _encounterController.Initialize(config);
    }


    public void DrawCard()
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
            int draw_idx = Mathf.RoundToInt((Random.value * (GameState.Player.dailyDeck.Value.Count-1)));

            int draw_value = GameState.Player.dailyDeck.Value[draw_idx];
            GameState.Player.dailyDeck.Value.RemoveAt(draw_idx);
            GameState.Player.dailyDeck.Raise();  // we manually raise the change because list changes are too deep to be registered automatically

            Card draw = (Card)Cards.CreateCardWithID(draw_value);
            if (draw == null)
            {
                Debug.LogError("Drew a card with an invalid index");
            }
            _hand.Add(draw);
            _encounterController.PlaceCard(draw);
        }
    }

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

        // TODO, actual implementation of game state change

        // remove card
        _encounterController.RemoveCard(card);
    }


    public EncounterPrefabController GetEncounterController()
    {
        return _encounterController;
    }

    public List<Card> GetHand()
    {
        return _hand;
    }
}
