using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * An abstract class that all conversation cards override/inherit from
 * All of a cards atrributes and information will be stored in a dictionary which can only be accessed through getter methods.
 * The values for the cards attributes are declared in derived card classes.
 */

public abstract class ConversationCard
{

    protected readonly int _id;
    protected IDictionary<string, string> _metadata = new Dictionary<string, string>();
    protected Transform placement;
    protected List<Action> methods = new List<Action>();

    public ConversationCard(int id)
    {
        this._id = id;
        
    }

    // some getters
    public int GetId() { return this._id; }
    public string GetElement() { return _metadata["element"]; }
    public string GetName() { return _metadata["name"]; }
    public string GetDescription() { return _metadata["description"]; }
    public int GetComplianceValue() { return int.Parse(_metadata["compliance"]); }
    public int GetPatienceValue() { return int.Parse(_metadata["patience"]); }
    public Transform GetTransform() { return placement; }
    public void SetTransform(Transform location) { placement = location;}
    public void Execute() { GameState.Meta.activeEncounter.Value.AddFilter(int.Parse(_metadata["duration"]), int.Parse(_metadata["filterId"])); }
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