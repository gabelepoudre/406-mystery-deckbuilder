/*
 * author(s): Gabriel LePoudre
 * 
 * This script contains the GameStateValueSubscriber class for subscribing to a GameStateValue
 */


public abstract class GameStateValueSubscriber<T> {
    public GameStateValueSubscriber(GameStateValue<T> gameStateValue) {
        gameStateValue.Subscribe(this);
    }

    /* This is what is extended when you make one of these classes. What do you want it to do when it changes? */
    public abstract void OnValueChange(T newValue);
}
