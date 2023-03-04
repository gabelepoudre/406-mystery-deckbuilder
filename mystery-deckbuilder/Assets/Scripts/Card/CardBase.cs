using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * An abstract class that all conversation cards override/inherit from
 * All of a cards atrributes and information will be stored in a dictionary which can only be accessed through getter methods.
 * The values for the cards attributes are declared in derived card classes.
 */




public abstract class Card
{

    protected readonly int _id;
    protected IDictionary<string, string> _metadata = new Dictionary<string, string>();
    protected CardPrefabController frontendController;
    protected int position;
    protected List<Action> methods = new List<Action>();

    private int _stackableComplianceMod = 0;  // Note! Stackable values are ALWAYS multiplications. No additions
    private int _unstackableComplianceMod = 0;  // Note! Unstackable values are ALWAYS additions. No multiplications
    private int _complianceOverride = 0; // Note! Override is for fully overrulling a compliance value (e.g. the next card you play will give no compliance)
    private bool _complianceOverridden = false;

    private int _stackablePatienceMod = 0;  // Note! Stackable values are ALWAYS multiplications. No additions
    private int _unstackablePatienceMod = 0;  // Note! Unstackable values are ALWAYS additions. No multiplications
    private int _patienceOverride = 0; // Note! Override is for fully overrulling a patience value (e.g. the next card you play will cost no patience)
    private bool _patienceOverridden = false;



    public Card(int id)
    {
        this._id = id;
    }
    
    public int GetId() { return this._id; }
    public string GetElement() { return _metadata["element"]; }
    public string GetName() { return _metadata["name"]; }
    public string GetDescription() { return _metadata["description"]; }

    public int GetDefaultCompliance() { return int.Parse(_metadata["compliance"]); }
    public int GetStackableComplianceMod() { return _stackableComplianceMod; }
    public int GetUnstackableComplianceMod() { return _unstackableComplianceMod; }
    public int GetStackableCompliance() { return GetStackableComplianceMod() + GetDefaultCompliance(); }
    public int GetTotalCompliance() { return GetStackableCompliance() + GetUnstackableComplianceMod(); }
    public bool ComplianceOverriden() { return _complianceOverridden; }
    public int ComplianceOverrideValue() { return _complianceOverride; }


    public int GetDefaultPatience() { return int.Parse(_metadata["patience"]); }
    public int GetStackablePatienceMod() { return _stackablePatienceMod; }
    public int GetUnstackablePatienceMod() { return _unstackablePatienceMod; }
    public int GetStackablePatience() { return GetStackablePatienceMod() + GetDefaultPatience(); }
    public int GetTotalPatience() { return GetStackablePatience() + GetUnstackablePatienceMod(); }
    public bool PatienceOverriden() { return _patienceOverridden; }
    public int PatienceOverrideValue() { return _patienceOverride; }

    public int GetPosition() { return position; }
    public void SetPosition(int index) { position = index; }
//    public void Execute() { GameState.Meta.activeEncounter.Value.AddFilter(int.Parse(_metadata["duration"]), int.Parse(_metadata["filterId"])); }
    public void SetAndInitializeFrontendController(CardPrefabController controller)
    {
        frontendController = controller;
        frontendController.SetCardName(GetName());
        frontendController.SetCardDescription(GetDescription());
        frontendController.SetDefaultPatience(GetDefaultPatience());
        frontendController.SetPosition(GetPosition());
        if (GetElement() != "Preparation")
        {
            frontendController.SetDefaultCompliance(GetDefaultCompliance());
        }
        Debug.Log("Ran Card Initialization");
    }
    public CardPrefabController GetFrontendController()
    {
        return frontendController;
    }
}

/*
 * An abstract class that all preparation cards override/inherit from
 * All of a cards atrributes and information will be stored in a dictionary which can only be accessed through getter methods.
 * The values for the cards attributes are declared in derived card classes.
 */
public abstract class PreparationCard
{

    protected readonly int _id;
    protected IDictionary<string, string> _metadata = new Dictionary<string, string>();
    protected List<Action> methods = new List<Action>();

    public PreparationCard(int id)
    {
        this._id = id;


    }

    // some getters
    public int GetId() { return this._id; }
    public string GetName() { return _metadata["name"]; }
    public string GetDescription() { return _metadata["description"]; }
    public string GetCost() { return _metadata["cost"]; }

}