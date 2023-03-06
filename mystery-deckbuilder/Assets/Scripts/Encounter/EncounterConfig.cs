/*
 * author(s): Gabriel LePoudre
 * 
 * For Initializing Encounters. Mostly handled by NPC calls
 */

public class EncounterConfig
{
    public int MaximumPatience { get; set; }
    public int MaximumCompliance { get; set; }
    public NPC Opponent { get; set; }

    public EncounterConfig(NPC opponent, int maxCompliance = 50, int maxPatience = 10)
    {
        Opponent = opponent;
        MaximumCompliance = maxCompliance;
        MaximumPatience = maxPatience;
    }
}
