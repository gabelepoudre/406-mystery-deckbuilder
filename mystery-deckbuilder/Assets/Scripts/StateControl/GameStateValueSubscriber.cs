public abstract class GameStateValueSubscriber<T>
{
    public GameStateValueSubscriber(GameStateValue<T> gameStateValue)
    {
        gameStateValue.Subscribe(this);
    }
    public abstract void OnValueChange(T newValue);
}
