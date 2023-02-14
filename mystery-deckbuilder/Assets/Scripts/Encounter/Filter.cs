

using System;
using UnityEngine;

public static class Filters
{
    public static Array GetFilterByID(int id, int baseComp, int basePatience, string element) //param list: card element, base compliance, base patience. can use GameState for a lot of access stuff I guess 
    {
        switch (id)
        {
            case 0:
                return TestFilter();
            case 1:
                return WeaknessCheck(baseComp, element);
            case 2:
                return ResistanceCheck(baseComp, element);
            default:
                return null;
        }
    }

    private static Array TestFilter()
    {
        return null;
    }
    private static Array WeaknessCheck(int baseComp, string element) //needs access to card element, base compliance
    {
        int modifyAmount = 0;
        if (String.Equals(GameState.Meta.activeEncounter.Value.GetWeakness(), element))
        {
            modifyAmount = baseComp; 
            
        }
        int[] vals = new int[] {0, modifyAmount };
        return vals;
    }
    private static Array ResistanceCheck(int baseComp, string element) //needs access to card element, base compliance
    {
        int modifyAmount = 0;
        if (String.Equals(GameState.Meta.activeEncounter.Value.GetWeakness(), element))
        {
            modifyAmount = -(baseComp - 10);
        }
        int[] vals = new int[] { 0, modifyAmount };
        return vals;
    }
}

