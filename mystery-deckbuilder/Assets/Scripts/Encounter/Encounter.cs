using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class Encounter
{
    public static Encounter StartEncounter(EncounterConfig config)
    {
    //    if (GameState.Meta.activeEncounter.Value != null)
    //    {
    //        Debug.LogError("Tried to initialize an encounter while one was active");
    //        return null;
    //    }
    //    else
    //    {
           Encounter encounter = new Encounter(config);
    //        GameState.Meta.activeEncounter.Value = encounter;
            return encounter;
     //   }
    }

    public static void EndEncounter()
    {
        if (GameState.Meta.activeEncounter.Value == null)
        {
            Debug.LogError("Tried to close an encounter when none were active");
        }
        else
        {

        }
    }

    private GameObject _encounterPrefab;
    private EncounterPrefabController _encounterController;
    
    public Encounter(EncounterConfig config)
    {
        _encounterPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Encounter/Encounter.prefab");
        GameObject.Instantiate(_encounterPrefab);
    }
}
