

using System;
using UnityEngine;

/**
 * Collection of static objects that act as pre set calculations for resolving card effects
 * 
 * calculations should be done against the base amount and returne as a flat amount, so that order of effects doesn't matter
 * 
 */
public static class Filters
{
    public static Array GetFilterByID(int id, Card card) //can use GameState for a lot of access stuff 
    {
        switch (id)
        {
            case 0:
                return NoEffect();
            case 1:
                return WeaknessCheck(card);
            case 2:
                return ResistanceCheck(card);
            default:
                return null;
        }
    }

    private static Array NoEffect()
    {
        return new int[] {0, 0};
    }
    private static Array WeaknessCheck(Card card) //needs access to card element, base compliance
    {
        int modifyAmount = 0;
        if (String.Equals(GameState.Meta.activeEncounter.Value.GetWeakness(), card.GetElement()))
        {
            modifyAmount = card.GetComplianceValue(); 
            
        }
        int[] vals = new int[] {0, modifyAmount };
        return vals;
    }
    private static Array ResistanceCheck(Card card) //needs access to card element, base compliance
    {
        int modifyAmount = 0;
        if (String.Equals(GameState.Meta.activeEncounter.Value.GetWeakness(), card.GetComplianceValue()))
        {
            modifyAmount = -10;
        }
        int[] vals = new int[] { 0, modifyAmount };
        return vals;
    }
}

