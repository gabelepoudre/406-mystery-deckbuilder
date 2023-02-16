using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //Variables 
    public string characterName;
    [Range(0.0f, 1.0f)]public float affinity_1;
    [Range(0.0f, 1.0f)]public float affinity_2;
    [Range(0.0f, 1.0f)]public float affintiy_3;
    [Range(0.0f, 1.0f)]public float startingPatience;
    [Range(0.0f, 1.0f)]public float startingCompliance;
    [Range(0.0f, 1.0f)]public float currentPatience;
    [Range(0.0f, 1.0f)]public float currentCompliance;
    public DialogueTree dialogue;

    //Get current patience
    public float GetPatience()
    {
        return currentPatience;
    }

    //Set current patience
    public void SetPatience(float x)
    {
        currentPatience = x;
    }

    //Get current compliance
    public float GetCompliance()
    {
        return currentCompliance;
    }

    //Set current compliance
    public void SetCompliance(float x)
    {
        currentCompliance = x;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCompliance = startingCompliance;
        currentPatience = startingPatience;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
