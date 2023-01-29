using System;
using System.Reflection;
using System.Collections.Generic;

public static class GameState
{
    private static List<IGameStateValue> _gameStateValues = new();

    /*
     * ------------------
     * USE THIS SPACE TO DEFINE GAME STATES YOU WOULD LIKE TO TRACK
     * \/--------------\/
     */

    public static GameStateValue<double> playerSpeed = new(5.0, _gameStateValues);
    public class Meta 
    {
        public static GameStateValue<int> currentArc = new(1, _gameStateValues);
    }

    /*
    * /\--------------/\
    * USE THE ABOVE SPACE TO DEFINE GAME STATES YOU WOULD LIKE TO TRACK
    * ------------------
    */


    public static void ResetCurrentGameState()
    {
        foreach (IGameStateValue gameState in _gameStateValues)
        {
            gameState.Reset();
        }
    }

}