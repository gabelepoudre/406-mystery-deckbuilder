using UnityEngine;

/**
 * put this button in a canvas object in the scene
 * press for encounter overlay
 */
public class EncounterTest : MonoBehaviour
{
    public NPC npc;
    public void StartEncounter()
    {
        EncounterConfig conf = new EncounterConfig(npc, 200, 50);
        Encounter encounterInstance = Encounter.StartEncounter(conf);
    }

}
