/*
 * author(s): Gabriel LePoudre
 * 
 * This script stores the static "GameState" which serves as a static list of values you need to keep track of
 */

using System.Collections.Generic;


/*
 * The GameState static class is used to track all things state about our game. Because our genre has to \
 *  keep track of what the player knows, and descisions they have made 
 */
public static class GameState
{
    // stores the new values that have been made. Whenever you define a new GameStateValue, use this as arg2
    private static List<IGameStateValue> _gameStateValues = new();


    public static GameStateValue<int> currentDay = new(0, _gameStateValues); // The current game day, as an example


    /* GameStateValue holder class for Meta data about the game. Could be what "phase" or "mode" of gameplay */
    public class Meta 
    {
        /* Current Game "phase", or "mode" as a state machine enum */
        public enum GameplayPhases
        {
            Tutorial,
            Phase_1, // this is just "normal gameplay"
        }
        public static GameStateValue<GameplayPhases> currentGameplayPhase = 
            new(GameplayPhases.Tutorial, _gameStateValues);

        public static GameStateValue<EncounterScript> activeEncounter = new(null, _gameStateValues);
        
    }


    /* GameStateValue holder class for Player data. Could be what they know for use in Dialogue trees */
    public class Player
    {
       
    }


    /* GameState holder class for NPCs data. Could be their current location */
    public class NPCs
    {

    }

    /* GameState holder class for ongoing card and deck information*/
    public class CardInfo
    {
        //these are lists because they have to be in this context. just be mindfull of list length weirdness
        static int[] startingDeck = {1, 5, 9, 1, 5, 9, 1, 5, 9, 1, 5, 9};
        public static GameStateValue<List<int>> currentDeck = new(new List<int>(startingDeck), _gameStateValues);

        static int[] startingDiscard = { };
        public static GameStateValue<List<int>> currentDiscard = new(new List<int>(startingDiscard), _gameStateValues);
    }


    /* Sets all tracked GameStateValues to their default values. WARNING: Irreversible */
    public static void ResetCurrentGameState()
    {
        foreach (IGameStateValue gameState in _gameStateValues)
        {
            gameState.Reset();
        }
    }

}