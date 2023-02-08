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
    [SerializeField] private int _threshold;
    [SerializeField] private int _compliance;
    [SerializeField] private int _patience;
    private int maxPatience = 10; //Not sure if this will ever change, but the option is there

    private ArrayList deck; //TODO: this script will need a reference to the deck in whatever form that may take
    private Sprite NpcSprite; //TODO: a sprite reference will need to be passed in in order to display who the player is in an encounter with

    public GameObject patienceBar;
    public GameObject complianceBar;

    /**
     * initializes the ecounter. 
     * more parameters may be added as their implementaion is solidified
     */
    public void StartEncounter(int complianceThreshold, int startingCompliance, int startingPatience)
    {
        _threshold = complianceThreshold;
        _compliance = startingCompliance;
        _patience = startingPatience;
        SetInitialBarValues();
    }

    /**
     * initializes the bars
     */
    private void SetInitialBarValues()
    {
        patienceBar.GetComponent<BarScript>().Initialize(maxPatience, _patience);
        complianceBar.GetComponent<BarScript>().Initialize(_threshold, _compliance);
    }
    
    public void SetPatience(int val)
    {
        _patience = val;
        patienceBar.GetComponent<BarScript>().SetValue(_patience);
    }

    public int GetPatience()
    {
        return _patience;
    }

    /**
     * patience will never go above max
     */
    public void IncPatience(int inc)
    {
        if (_patience + inc <= maxPatience)
        {
            _patience += inc;
        }
        else
        {
            _patience = maxPatience;
        }
        patienceBar.GetComponent<BarScript>().SetValue(_patience);
    }

    public void DecPatience(int dec)
    {
        _patience -= dec;
        patienceBar.GetComponent<BarScript>().SetValue(_patience);
        if (_patience <= 0)
        {
            EndEncounter(false);
        }
    }

    public void SetCompliance(int val)
    {
        _compliance = val;
        complianceBar.GetComponent<BarScript>().SetValue(_compliance);
    }

    public int GetCompliance()
    {
        return _compliance;
    }

    public void IncCompliance(int inc)
    {
        _compliance += inc;
        complianceBar.GetComponent<BarScript>().SetValue(_compliance);
        if (_compliance >= _threshold)
        {
            EndEncounter(true);
        }
    }

    /**
     * compliance value cannot go bellow 0
     */
    public void DecCompliance(int dec)
    {
        if (_compliance - dec >= 0)
        {
            _compliance -= dec;
        }
        else
        {
            _compliance = 0;
        }
        complianceBar.GetComponent<BarScript>().SetValue(_compliance);
    }


    /**
     * can send the win/lose result, but there is no one to send it to right now
     */
    private void EndEncounter(bool victory)
    {
        Destroy(this.gameObject);
    }
}
