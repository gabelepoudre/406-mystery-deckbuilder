using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEncounterWithNpc : MonoBehaviour
{
    public NPC opponent;

    public void LaunchEncounterOnClick()
    {
        Encounter.StartEncounter(new EncounterConfig(opponent, (int)opponent.startingCompliance, (int)opponent.startingPatience));
    }
}
