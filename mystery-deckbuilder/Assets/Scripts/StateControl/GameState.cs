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


        public static GameStateValue<Encounter> activeEncounter = new(null, _gameStateValues);
        public static GameStateValue<bool> lastEncounterEndedInVictory = new(false, _gameStateValues);

        public static GameStateValue<bool> notepadActive = new(false, _gameStateValues);
        
    }


    /* GameStateValue holder class for Player data. Could be what they know for use in Dialogue trees */
    public class Player
    {
        public static GameStateValue<List<int>> fullDeck;

        // tutorial/testing TODO remove
        //static int[] startingDeck = { 10, 10, 10, 10, 17, 17, 17, 17 };
        static int[] startingDeck = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
        public static GameStateValue<List<int>> dailyDeck = new(new List<int>(startingDeck), _gameStateValues);
    }


    /* GameState holder class for NPCs data. Could be their current location */
    public class NPCs
    {

        //
        public static string lastNPCSpokenTo = "";

        //so we can access the encounters completed value with the name of the NPC
        public static Dictionary<string, GameStateValue<int>> npcNameToEncountersCompleted = new(){{"Nibbles", Nibbles.encountersCompleted}};

        //to keep track of what NPCs have been met by the player
        public static List<string> npcsMet = new List<string>();
        
        
        /* This GameStateValue references the GameStateValue representing the number of encounters completed for the 
           last NPC we talked to.
         */
        public static GameStateValue<GameStateValue<int>> latestNPCEncountersCompleted = new(null, _gameStateValues);

        //data specifically pertaining to Nibbles
        public static class Nibbles
        {
            //the number of encounters completed with Nibbles
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
        }
        

    }

    /* GameState holder class for Zones */
    public class Zones
    {
        //to keep track of what zones have been visited by the player
        public static List<string> zonesVisted = new List<string>();


    }

    /* GameState holder class for ongoing card and deck information*/
    public class CardInfo
    {
        //these are lists because they have to be in this context. just be mindfull of list length weirdness
        static int[] startingDeck = {1, 5, 9, 1, 5, 9, 1, 5, 9, 1, 5, 9};

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