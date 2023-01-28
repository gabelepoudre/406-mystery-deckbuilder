using System;

public class StateClass
{
    public StateClass() { }
    public GameStateValue<int> value = new(0);
}
public static class GameState {
    private static StateClass _currentGameState = new();

    public static StateClass CurrentState() { return _currentGameState; }
    public static void ResetCurrentGameState() { _currentGameState = new StateClass(); }
}



/*
public static class GameState {
    public static GameStateValue<bool> gameState = new(true);
}
*/


