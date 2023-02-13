

public static class Filters
{
    public static object GetFilterByID(int id) //param list: card element, base compliance, base patience. can use GameState for a lot of access stuff I guess 
    {
        switch (id)
        {
            case 1:
                return TestFilter();
            default:
                return null;
        }
    }

    private static int TestFilter()
    {
        
        return 0;
    }
    private static int WeaknessCheck() //needs access to card element, base compliance
    {
        //GameState.Meta.activeEncounter.Value.GetWeakness;
        return 0;
    }
    private static int ResistanceCheck() //needs access to card element, base compliance
    {
        //GameState.Meta.activeEncounter.Value.GetResistance;
        return 0;
    }
}

