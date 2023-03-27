/*
 * author(s): Gabriel LePoudre
 * 
 * This script stores the static "GameState" which serves as a static list of values you need to keep track of
 */

using System.Collections.Generic;
using System;


/*
 * The GameState static class is used to track all things state about our game. Because our genre has to \
 *  keep track of what the player knows, and descisions they have made 
 */
public static class GameState
{
    // stores the new values that have been made. Whenever you define a new GameStateValue, use this as arg2
    private static List<IGameStateValue> _gameStateValues = new();


  //  public static GameStateValue<int> currentDay = new(8, _gameStateValues); // The current game day, as an example


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

        
        
        public static GameStateValue<int> currentAct = new(1, _gameStateValues);

        public static GameStateValue<int> currentDay = new(1, _gameStateValues);


        public static GameStateValue<Encounter> activeEncounter = new(null, _gameStateValues);
        public static GameStateValue<bool> lastEncounterEndedInVictory = new(false, _gameStateValues);


        public static GameStateValue<bool> notepadActive = new(false, _gameStateValues);
        public static GameStateValue<bool> dialogueActive = new(false, _gameStateValues);
        public static GameStateValue<bool> justSlept = new(false, _gameStateValues);
        
    }


    /* GameStateValue holder class for Player data. Could be what they know for use in Dialogue trees */
    public class Player
    {
        //static int[] startingDeck = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21};

        //static int[] startingDeck = {1, 1, 1, 3, 3, 3, 7, 8, 8, 8, 11, 11, 11, 12, 13, 13, 14, 14, 17, 17, 19};

        //static int[] startingDeck = {1, 1, 1, 5, 5, 5, 9, 9, 9, 4, 4, 4, 8, 8, 8, 18, 18, 18, 17, 17, 17};

        static int[] startingDeck = {1, 5, 9, 4, 8, 18, 17, 1, 5, 9, 4, 8, 18, 17, 1, 5, 9, 4, 8, 18, 17};
        public static GameStateValue<List<int>> fullDeck = new(new List<int>(startingDeck), _gameStateValues);
        public static GameStateValue<List<int>> dailyDeck = new(new List<int>(startingDeck), _gameStateValues);
        public static GameStateValue<List<int>> collection = new(new List<int>(startingDeck), _gameStateValues);
        public static GameStateValue<int> maximumCardsAllowedInDeck = new(21, _gameStateValues);


        public enum Locations
        {
            Motel,
            Bar,
            Boxcar,
            LumberYard,
            RailYard,
            RatMobCave,
            BerryFarm,
            BreakfastPalace,
            RealMainStreet,
            MotelOfficeInside,
            MotelRoomInside,
            PostOfficeInside
        }

        public static GameStateValue<Dictionary<Locations, int>> locationBuildIndex = new(new Dictionary<Locations, int>()
        {
            [Locations.Motel] = 3,
            [Locations.Bar] = 4,
            [Locations.Boxcar] = 5,
            [Locations.LumberYard] = 6,
            [Locations.RailYard] = 7,
            [Locations.RatMobCave] = 8,
            [Locations.BerryFarm] = 9,
            [Locations.BreakfastPalace] = 10,
            [Locations.RealMainStreet] = 2,
            [Locations.PostOfficeInside] = 11
        }, _gameStateValues);


        //NOTE: state listeners of certain NPCs (like Rat Prince) that are placed in multiple locations rely on this
        public static GameStateValue<Locations> location = new(Locations.Motel, _gameStateValues);


        public static GameStateValue<Dictionary<Locations, bool>> locationsViewable = new(new Dictionary<Locations, bool>()
        {
            [Locations.Motel] = false,
            [Locations.Bar] = false,
            [Locations.Boxcar] = false,
            [Locations.LumberYard] = false,
            [Locations.RailYard] = false,
            [Locations.RatMobCave] = false,
            [Locations.BerryFarm] = false,
            [Locations.BreakfastPalace] = false,
            [Locations.RealMainStreet] = false,
            [Locations.MotelOfficeInside] = false,
            [Locations.MotelRoomInside] = false,
            [Locations.PostOfficeInside] = false
        }, _gameStateValues);

        public static GameStateValue<int> napsRemainingToday = new(1, _gameStateValues);

    }


    /* GameState holder class for NPCs data. Could be their current location 
     * NOTE: be sure to update encountersCompleted and encountersWon for every NPC because they have dialogue
     * that is dependent on these
    */
    public class NPCs
    {

        //NOTE: updates automatically in NPCdialoguetrigger
        public static string lastNPCSpokenTo = "";
        

        //to keep track of what NPCs have been met by the player
        public static List<string> npcsMet = new List<string>();
        
        /* So we can access NPC values with the name of the NPC (it doesn't let me access the class)
         *
         *  FORGIVE ME FATHER FOR I HAVE SINNED
         *  YES I KNOW THIS IS PROFOUNDLY GROTESQUE I AM SO SORRY FOR DOING THIS BUT I WILL CHANGE IT AFTER THE BETA (UNLESS I FORGET)
         *  
         *  Select a culprit uses npcNameToMet btw
         */
        public static Dictionary<string, GameStateValue<bool>> npcNameToMet = new(){{"Nibbles", Nibbles.met}, 
        {"Austin", Austin.met}, {"Austyn", Austyn.met}, {"Alan", Alan.met}, 
        {"Mark", Mark.met}, {"Samuel", Samuel.met}, {"Doug", Doug.met}, 
        {"Elk Secretary", Elk.met}, {"Rat Leader", Rat_Leader.met}, {"Rat Prince", Rat_Prince.met}, 
        {"Big Rat", Big_Rat.met}, {"Bee", Bee.met}, {"Marry", Marry.met}, 
        {"Wolverine", Wolverine.met}, {"Black Bear", Black_Bear.met}, {"Crouton", Crouton.met}, 
        {"Nina", Nina.met}, {"Mike", Mike.met}, {"Speck", Speck.met}, 
        {"Oslow", Oslow.met}, {"Clay", Clay.met}};

        public static Dictionary<string, GameStateValue<int>> npcNameToEncountersCompleted = new(){{"Nibbles", Nibbles.encountersCompleted}, 
        {"Austin", Austin.encountersCompleted}, {"Austyn", Austyn.encountersCompleted}, {"Alan", Alan.encountersCompleted}, 
        {"Mark", Mark.encountersCompleted}, {"Samuel", Samuel.encountersCompleted}, {"Doug", Doug.encountersCompleted}, 
        {"Elk Secretary", Elk.encountersCompleted}, {"Rat Leader", Rat_Leader.encountersCompleted}, {"Rat Prince", Rat_Prince.encountersCompleted}, 
        {"Big Rat", Big_Rat.encountersCompleted}, {"Bee", Bee.encountersCompleted}, {"Marry", Marry.encountersCompleted}, 
        {"Wolverine", Wolverine.encountersCompleted}, {"Black Bear", Black_Bear.encountersCompleted}, {"Crouton", Crouton.encountersCompleted}, 
        {"Nina", Nina.encountersCompleted}, {"Mike", Mike.encountersCompleted}, {"Speck", Speck.encountersCompleted}, 
        {"Oslow", Oslow.encountersCompleted}, {"Clay", Clay.encountersCompleted}};

        public static Dictionary<string, GameStateValue<int>> npcNameToEncountersWon = new(){{"Nibbles", Nibbles.encountersWon}, 
        {"Austin", Austin.encountersWon}, {"Austyn", Austyn.encountersWon}, {"Alan", Alan.encountersWon}, 
        {"Mark", Mark.encountersWon}, {"Samuel", Samuel.encountersWon}, {"Doug", Doug.encountersWon}, 
        {"Elk Secretary", Elk.encountersWon}, {"Rat Leader", Rat_Leader.encountersWon}, {"Rat Prince", Rat_Prince.encountersWon}, 
        {"Big Rat", Big_Rat.encountersWon}, {"Bee", Bee.encountersWon}, {"Marry", Marry.encountersWon}, 
        {"Wolverine", Wolverine.encountersWon}, {"Black Bear", Black_Bear.encountersWon}, {"Crouton", Crouton.encountersWon}, 
        {"Nina", Nina.encountersWon}, {"Mike", Mike.encountersWon}, {"Speck", Speck.encountersWon}, 
        {"Oslow", Oslow.encountersWon}, {"Clay", Clay.encountersWon}};

        

        //we'll be switching scenes so we have to statically store NPC dialogue keys
        public static Dictionary<string, string> currentNPCDialogueKeys = new();
        
        public static class Nibbles
        {
           
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Austin
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Austyn
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Alan
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Mark
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Samuel
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Doug
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Elk
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Rat_Leader
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Rat_Prince
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
            
        }

        public static class Big_Rat
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Bee
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Marry
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Wolverine
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Black_Bear
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Crouton
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);

            //whether the player has done the berry commotion sequence or not
            public static GameStateValue<bool> finishedBerryCommotion = new(false, _gameStateValues);

            //whether the she has given the player evidence
            public static GameStateValue<bool> gaveEvidence = new(false, _gameStateValues);
        }

        public static class Nina
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Mike
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Speck
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Oslow
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
        }

        public static class Clay
        {
            public static GameStateValue<int> encountersCompleted = new(0, _gameStateValues);
            public static GameStateValue<int> encountersWon = new(0, _gameStateValues);
            public static GameStateValue<bool> met = new(false, _gameStateValues);
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