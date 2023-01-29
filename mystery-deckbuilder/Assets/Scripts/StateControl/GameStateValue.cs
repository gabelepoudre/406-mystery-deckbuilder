/*
 * author(s): Gabriel LePoudre
 * 
 * This script stores the GameStateValue class, which exists to store values pertaining to game state
 */

using System;
using System.Collections.Generic;


public interface IGameStateValue {
    public void Reset() { }
}

/*
 * The GameStateValue class is a wrapper for any value that includes a "pub-sub" architecture
 * Use this class to store values within the GameState.cs file and allow subscribers to be notified on change
 */
public class GameStateValue<T>: IGameStateValue {
    private T _value;
    private readonly T _defaultValue;
    public T Value {
        get { return _value; }
        set
        {
            _value = value;
            Raise();
        }
    }

    public T DefaultValue { get; }

    public Action OnChange { get; set; }

    public GameStateValue(T value, List<IGameStateValue> addThisTo)
    {
        this._defaultValue = value;
        this._value = this._defaultValue;
        addThisTo.Add(this);
    }

    private void Raise() {
        OnChange?.Invoke();
    }

    public void Reset() {
        Value = DefaultValue;
    }


}
