using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * put this button in a canvas object in the scene
 * press for encounter overlay
 */
public class EncounterTest : MonoBehaviour
{
    public GameObject encounter;
    public void StartEncounter()
    {
        encounter = Instantiate(encounter);
        encounter.GetComponent<EncounterScript>().StartEncounter(100, 0, 10);
    }

}
