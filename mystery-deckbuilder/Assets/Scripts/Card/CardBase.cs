using System.Collections.Generic;

/*
 * An abstract class that all conversation cards override/inherit from
 * All of a cards atrributes and information will be stored in a dictionary which can only be accessed through getter methods.
 * The values for the cards attributes are declared in derived card classes.
 */

public abstract class ConversationCard
{

    protected readonly int _id;
    protected IDictionary<string, string> _metadata = new Dictionary<string, string>();

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