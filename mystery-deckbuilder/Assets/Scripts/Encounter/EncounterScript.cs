using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * script for the encounter gameobject
 * to use:
 *      encounter = Instantiate(encounter);
        encounter.GetComponent<EncounterScript>().StartEncounter(int complianceThreshold, int startingCompliance, int startingPatience)
 */
public class EncounterScript : MonoBehaviour
{
    [SerializeField] private int threshold;
    [SerializeField] private int compliance;
    [SerializeField] private int patience;
    private ArrayList deck;
    private Sprite NPCsprite;
    public GameObject patienceBar;
    public GameObject complianceBar;

    /**
     * initializes the ecounter. 
     * more parameters may be added as their implementaion is solidified
     */
    public void StartEncounter(int complianceThreshold, int startingCompliance, int startingPatience)
    {
        threshold = complianceThreshold;
        compliance = startingCompliance;
        patience = startingPatience;
        SetInitialBarValues();
    }

    /**
     * initializes the bars
     */
    private void SetInitialBarValues()
    {
        patienceBar.GetComponent<BarScript>().Initialize(10, patience);
        complianceBar.GetComponent<BarScript>().Initialize(threshold, compliance);
    }
    public void ModifyPatience(int modVal)
    {
        patience += modVal;
        patienceBar.GetComponent<BarScript>().SetValue(patience);
        if (patience <= 0)
        {
            EndEncounter(false);
        }
    }
    public void ModifyCompliance(int modVal)
    {
        compliance += modVal;
        complianceBar.GetComponent<BarScript>().SetValue(compliance);
        if (compliance >= threshold)
        {
            EndEncounter(true);
        }
    }

    /**
     * can send the win/lose result, but there is no one to send it to right now
     */
    private void EndEncounter(bool victory)
    {
        Destroy(this.gameObject);
    }
}
