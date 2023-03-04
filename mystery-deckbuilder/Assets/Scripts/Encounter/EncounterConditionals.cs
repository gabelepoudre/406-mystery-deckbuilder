using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EncounterConditionals
{
    static private Encounter E
    {
        get
        {
            if (GameState.Meta.activeEncounter.Value == null)
            {
                Debug.LogError("Tried to get conditionals' encounter when no encounter was active");
                return null;
            }
            return GameState.Meta.activeEncounter.Value;
        }
    }
    public static bool NumberPlaysLessThan(int value)
    {
        return E.Statistics.NumberOfPlays < value;
    }
    public static bool NumberPlaysGreaterThan(int value)
    {
        return E.Statistics.NumberOfPlays > value;
    }
    public static bool NumberPlaysEqualTo(int value)
    {
        return E.Statistics.NumberOfPlays == value;
    }
    public static bool PatienceLessThan(int value)
    {
        return E.GetEncounterController().GetPatience() < value;
    }
    public static bool PatienceGreaterThan(int value)
    {
        return E.GetEncounterController().GetPatience() > value;
    }
    public static bool PatienceEqualTo(int value)
    {
        return E.GetEncounterController().GetPatience() == value;
    }
    public static bool ComplianceLessThan(int value)
    {
        return E.GetEncounterController().GetCompliance() < value;
    }
    public static bool ComplianceGreaterThan(int value)
    {
        return E.GetEncounterController().GetCompliance() > value;
    }
    public static bool ComplianceEqualTo(int value)
    {
        return E.GetEncounterController().GetCompliance() == value;
    }
    // todo! put these in statistics
    public static bool CardsOfElementInHandLessThan(string element, int value)
    {
        int num = 0;
        foreach (Card c in E.GetHand())
        {
            if (c.GetElement().ToLower() == element.ToLower())
            {
                num += 1;
            }
        }
        return num < value;
    }
    public static bool CardsOfElementInHandGreaterThan(string element, int value)
    {
        int num = 0;
        foreach (Card c in E.GetHand())
        {
            if (c.GetElement().ToLower() == element.ToLower())
            {
                num += 1;
            }
        }
        return num > value;
    }
    public static bool CardsOfElementInHandEqualTo(string element, int value)
    {
        int num = 0;
        foreach (Card c in E.GetHand())
        {
            if (c.GetElement().ToLower() == element.ToLower())
            {
                num += 1;
            }
        }
        return num == value;
    }
}
