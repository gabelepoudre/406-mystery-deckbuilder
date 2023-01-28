/*
 * author(s): Gabriel LePoudre
 * 
 * This script stores the GameStateValue class, which exists to store values pertaining to game state
 */

using System.Collections.Generic;

/*
 * The GameStateValue class is a wrapper for any value that includes a "pub-sub" architecture
 * Use this class to store values within the GameState.cs file and allow subscribers to be notified on change
 */
public class GameStateValue<T> {
    private T _value;
    private List<GameStateValueSubscriber<T>> _subscribers = new();

    public GameStateValue(T value) {
        this._value = value;
    }

    public T GetValue() { return this._value; }

    /* Adds a given subscriber to the subscriber list */
    public void Subscribe(GameStateValueSubscriber<T> subscriberObject) {
        this._subscribers.Add(subscriberObject);
    }

    /*
     * Changes the value to another value of the same type
     * NOTE: changes notifies all subscribers of the change, if any exist
     */
    public void ChangeValue(T newValue) {
        this._value = newValue;

        // notify the subscribers
        foreach (GameStateValueSubscriber<T> sub in this._subscribers) {
            sub.OnValueChange(newValue);
        }
    }
}
