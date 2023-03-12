using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //Variables 
    public string CharacterName;

    [Range(0.0f, 1.0f)]public float affinity_intimidation = 0.5f;
    [Range(0.0f, 1.0f)]public float affinity_sympathy = 0.5f;
    [Range(0.0f, 1.0f)]public float affintiy_persuasion = 0.5f;


    [Range(0.0f, 10.0f)]public float startingPatience;
    [Range(0.0f, 100.0f)]public float startingCompliance;
    [Range(0.0f, 10.0f)]public float currentPatience;
    [Range(0.0f, 100.0f)]public float currentCompliance;

    [Range(0.0f, 100.0f)] public float ComplianceThreshhold;

    public NPCEncounterSpriteController encounterSprites;

    public Dictionary<string, DialogueTree> DialogueTreeDictionary;
    public string CurrentDialogueKey { get; set; }


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

    void Awake()
    {
        DialogueTreeDictionary = GetComponent<IDialogueTreeCollection>().GetDialogueTrees();
        CurrentDialogueKey = "Intro"; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
