/*
 * author(s): Gabriel LePoudre
 * 
 * For static methods to call in effects ONLY. This is because we can't guarantee
 *  an Encounter is active outside of an Effect
 *  
 *  NOTE: Largely untested
 */

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
        return E.Statistics.Patience < value;
    }
    public static bool PatienceGreaterThan(int value)
    {
        return E.Statistics.Patience > value;
    }
    public static bool PatienceEqualTo(int value)
    {
        return E.Statistics.Patience == value;
    }
    public static bool ComplianceLessThan(int value)
    {
        return E.Statistics.Compliance < value;
    }
    public static bool ComplianceGreaterThan(int value)
    {
        return E.Statistics.Compliance > value;
    }
    public static bool ComplianceEqualTo(int value)
    {
        return E.Statistics.Compliance == value;
    }
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
    public static bool NumberDrawsLessThan(int value)
    {
        return E.Statistics.NumberOfDraws < value;
    }
    public static bool NumberDrawsGreaterThan(int value)
    {
        return E.Statistics.NumberOfDraws > value;
    }
    public static bool NumberDrawsEqualTo(int value)
    {
        return E.Statistics.NumberOfDraws == value;
    }
}
