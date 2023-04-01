using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    //Variables 
    public string CharacterName;

    public int reactionOffsetX = 0;
    public int reactionOffsetY = 0;

    public bool IsBoss = false;

    [Range(0.0f, 1.0f)]public float affinity_intimidation = 0.5f;
    [Range(0.0f, 1.0f)]public float affinity_sympathy = 0.5f;
    [Range(0.0f, 1.0f)]public float affintiy_persuasion = 0.5f;

    [Range(1.0f, 500.0f)]public float startingPatience;
    [Range(1.0f, 500.0f)]public float startingCompliance;
    [Range(0.0f, 10.0f)]public float currentPatience;  // antiquated
    [Range(0.0f, 100.0f)]public float currentCompliance;  // antiquated

    [Range(0.0f, 100.0f)] public float ComplianceThreshhold;

    public int cardIDUnlockFromWinEncounter = -1;

    public NPCEncounterSpriteController encounterSprites;
    public GameObject reactionPrefab;

    public Dictionary<string, DialogueTree> DialogueTreeDictionary;


    private string _currentDialogueKey;
    public string CurrentDialogueKey { get => _currentDialogueKey; set {
        _currentDialogueKey = value;
        UpdateStaticDialogueKey();
    } }

    //update the key stored in GameState
    private void UpdateStaticDialogueKey()
    {
        if (GameState.NPCs.currentNPCDialogueKeys.ContainsKey(CharacterName))
        {
            GameState.NPCs.currentNPCDialogueKeys[CharacterName] = CurrentDialogueKey;
        }
        else
        {
            GameState.NPCs.currentNPCDialogueKeys.Add(CharacterName, CurrentDialogueKey);
        }
    }

    //get the key stored in GameState
    private void GetStaticDialogueKey()
    {
        if (GameState.NPCs.currentNPCDialogueKeys.ContainsKey(CharacterName))
        {
            CurrentDialogueKey = GameState.NPCs.currentNPCDialogueKeys[CharacterName];
        }
    }


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
        GetStaticDialogueKey();
        UseBerryFarmCrowdDialogue();
        HideIfNotBerryFarmScriptedEvent();
        HideIfNPCPresentAtBerryFarm();
        HideIfDay1();
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

    //if the NPC is just part of the crowd during the berry farm scripted event
    private void UseBerryFarmCrowdDialogue()
    {
        if (GameState.Meta.currentGameplayPhase.Value != GameState.Meta.GameplayPhases.Tutorial || GameState.Meta.currentDay.Value != 2)
        {
            return;
        }

        DialogueTree dialogue1 = new(new NPCNode(new string[] {"I can't believe the berries are gone!"}));
        DialogueTree dialogue2 = new(new NPCNode(new string[] {"Why would someone do something like this!?"}));
        DialogueTree dialogue3 = new(new NPCNode(new string[] {"Oh the humanity!!!"}));
        DialogueTree dialogue4 = new(new NPCNode(new string[] {"How will we ever recover from this????"}));
        DialogueTree dialogue5 = new(new NPCNode(new string[] {"It's over....."}));
        DialogueTree dialogue6 = new(new NPCNode(new string[] {"OMG NOOOOO!!!"}));
        DialogueTree dialogue7 = new(new NPCNode(new string[] {"Not when my PTO just got approved!"}));

        List<DialogueTree> trees = new() {dialogue1, dialogue2, dialogue3, dialogue4, dialogue5, dialogue6, dialogue7};
        var random = new System.Random();
        int index = random.Next(trees.Count);


        
        DialogueTreeDictionary.Add("BerryFarm", trees[index]);
        _currentDialogueKey = "BerryFarm";

        if (CharacterName == "Crouton" || CharacterName == "Black Bear" || CharacterName == "Elk Secretary")
        {
            DialogueTreeDictionary.Add("BerryFarmCrouton", new DialogueTree(new NPCNode(new string[] {"....."})));
            _currentDialogueKey = "BerryFarmCrouton";
        }
        
    }

    //if the event is over then hide the crowd NPC unless Austin or Austyn
    private void HideIfNotBerryFarmScriptedEvent()
    {

        if ((CharacterName == "Austin" || CharacterName == "Austyn"))
        {
            return;
        }
        else if (GameState.Meta.currentGameplayPhase.Value != GameState.Meta.GameplayPhases.Tutorial
        || GameState.Meta.currentDay.Value != 2)
        {
            if (SceneManager.GetActiveScene().name == "BerryFarm") { /*NOTE: I would check the location in GameState but starting location is motel and to test i start scene at berry farm*/
                gameObject.SetActive(false);
            }
        }
    }

    //hide the NPC unless they're at berry farm (during tutorial, day 2)
    private void HideIfNPCPresentAtBerryFarm()
    {
        if (GameState.Meta.currentGameplayPhase.Value == GameState.Meta.GameplayPhases.Tutorial && GameState.Meta.currentDay.Value == 2
        && SceneManager.GetActiveScene().name != "BerryFarm") //NOTE: see above for why not checking GameState for location right now
        {
            gameObject.SetActive(false);
        }
        
    }

    //hide the NPCs that mention the berry stuff during day 1
    private void HideIfDay1()
    {
        if (GameState.Meta.currentDay.Value != 1) return;

        if (CharacterName != "Nibbles" && CharacterName != "Austin" && CharacterName != "Marry")
        {
            gameObject.SetActive(false);
        }
    }

    




}
