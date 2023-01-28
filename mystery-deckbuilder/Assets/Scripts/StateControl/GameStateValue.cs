using System.Collections.Generic;

public class GameStateValue<T> {
    private T __value;
    private List<GameStateValueSubscriber<T>> __subscribers;

    public GameStateValue(T value) {
        this.__value = value;
    }

    public T GetValue() { return this.__value; }

    public void Subscribe(GameStateValueSubscriber<T> subscriberObject) {
        this.__subscribers.Add(subscriberObject);
    }

    public void ChangeValue(T newValue) {
        this.__value = newValue;

        // notify the subscribers
        foreach (GameStateValueSubscriber<T> sub in this.__subscribers) {
            sub.OnValueChange(newValue);
        }
    }
}
