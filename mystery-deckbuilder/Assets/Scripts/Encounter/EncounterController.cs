using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterController : MonoBehaviour
{

    private bool EncounterActive()
    {
        return GameState.Meta.activeEncounter.Value != null;
    }

    private Encounter GetEncounter()
    {
        return GameState.Meta.activeEncounter.Value;
    }

    public void DrawCard()
    {
        if(EncounterActive())
        {
            GetEncounter().DrawCard();
        }
        else
        {
            Debug.LogWarning("Tried to draw card when no encounter was active!");
        }
    }
}
