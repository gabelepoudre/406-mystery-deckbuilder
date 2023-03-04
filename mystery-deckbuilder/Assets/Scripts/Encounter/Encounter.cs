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
    
    public Encounter(EncounterConfig config)
    {
        GameObject prefabReference = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Encounter/Encounter.prefab");
        _encounterPrefab = GameObject.Instantiate(prefabReference);

        _encounterController = _encounterPrefab.GetComponent<EncounterPrefabController>();
        Debug.Log("Next should NOT be null");
        Debug.Log(_encounterController);

        _encounterController.Initialize(config);
        Debug.Log("Next should NOT be null");
        Debug.Log(_encounterController);
    }


    public void DrawCard()
    {

        Debug.Log("AHHHHHHHHHH 3");
        Debug.Log(_encounterPrefab);

        Debug.Log("Next should NOT be null");
        Debug.Log(_encounterPrefab);
        _encounterController = _encounterPrefab.GetComponent<EncounterPrefabController>();
        Debug.Log("Next should NOT be null");
        Debug.Log(_encounterController);
        if (_encounterController.PlaceMatFull())
        {
            Debug.Log("Can't draw, there is no place for the card");
        }
        else
        {
            Debug.Log("Debug 2");
            int draw_idx = Mathf.RoundToInt((Random.value * (GameState.Player.dailyDeck.Value.Count-1)));

            int draw_value = GameState.Player.dailyDeck.Value[draw_idx];
            GameState.Player.dailyDeck.Value.RemoveAt(draw_idx);
            GameState.Player.dailyDeck.Raise();  // we manually raise the change because list changes are too deep to be registered automatically

            Card draw = (Card)Cards.CreateCardWithID(draw_value);
            if (draw == null)
            {
                Debug.LogWarning("Drawed a card with an invalid index");
            }
            _hand.Add(draw);
            _encounterController.PlaceCard(draw);
        }
    }


}
