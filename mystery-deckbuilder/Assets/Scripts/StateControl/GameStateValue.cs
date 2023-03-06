/*
 * author(s): Gabriel LePoudre
 * 
 * This script contains the GameStateValue class, and the interface that it uses
 */

using System;
using System.Collections.Generic;

/* Interface for GameStateValues. Exists to ensure you can handle many GameStateValues of arbitrary type*/
public interface IGameStateValue 
{
    public void Reset() { }
}

/*
 * The GameStateValue class is a wrapper for any value that includes a "pub-sub" architecture
 * Use this class to store values within the GameState.cs file and allow subscribers to be notified on change
 * 
 * See: ../Examples/TestSubscriber.cs for an example of a MonoBehavior detecting changes to a value defined in GameState.cs
 */
public class GameStateValue<T>: IGameStateValue 
{
    private T _value;
    private readonly T _defaultValue;
    public T Value 
    {
        get { return _value; }
        set
        {
            _value = value;
            Raise();
        }
    }

    public T DefaultValue { get; }

    public Action OnChange { get; set; }  // this is the magic property needed for pub-sub

    public GameStateValue(T defaultValue, List<IGameStateValue> addOurselvesToThisList)
    {
        this._defaultValue = defaultValue;
        this._value = this._defaultValue;


        addOurselvesToThisList.Add(this);  // note, addOurselvesToThisList is used to track ALL GameStateValues
    }

    /* Emits the "OnChange" event, if anyone is listening*/
    public void Raise() 
    {
        OnChange?.Invoke();
    }

    /* Resets a GameStateValue to it's .DefaultValue */ 
    public void Reset() 
    {
        Value = DefaultValue;
    }

}
