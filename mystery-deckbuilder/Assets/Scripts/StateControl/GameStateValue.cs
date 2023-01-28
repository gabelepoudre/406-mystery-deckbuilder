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
    private T __value;
    private List<GameStateValueSubscriber<T>> __subscribers;

    public GameStateValue(T value) {
        this.__value = value;
    }

    public T GetValue() { return this.__value; }

    /* Adds a given subscriber to the subscriber list */
    public void Subscribe(GameStateValueSubscriber<T> subscriberObject) {
        this.__subscribers.Add(subscriberObject);
    }

    /*
     * Changes the value to another value of the same type
     * NOTE: changes notifies all subscribers of the change, if any exist
     */
    public void ChangeValue(T newValue) {
        this.__value = newValue;

        // notify the subscribers
        foreach (GameStateValueSubscriber<T> sub in this.__subscribers) {
            sub.OnValueChange(newValue);
        }
    }
}
