using UnityEngine.Audio;
using System;
using UnityEngine;

// Based on "Introduction to AUDIO in Unity" by Brackeys (Youtube)
// USAGE:
//      Make sure this is placed in the first scene the game loads.
// TO PLAY A SOUND:
//      In any script:
//          FindObjectOfType<AudioManager>().Play("filename-of-sound");
// TO STOP A SOUND:
//      In any script:
//          FindObjectOfType<AudioManager>().Stop("filename-of-sound");


public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    // Set value for berry farm leaving after commotion.
    bool leftCommotion = false;

    // static reference to the current instance of the AudioManager.
    // singleton pattern.
    public static AudioManager instance;

    // Called before Start method
    private void Awake()
    {
        // continuation of singleton pattern.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Makes sure the AudioManager persists between scenes.
        // Otherwise, audio gets interrupted upon scene transition.
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        // Listen for if the player is in an encounter or not
        GameState.Meta.activeEncounter.OnChange += EncounterChange;

        // Listen for berry commotion
        GameState.NPCs.Crouton.finishedBerryCommotion.OnChange += BerryCommotionChange;

        // Listen for scene change
        GameState.Player.location.OnChange += LocationChange;

        // Play initial music
        Sound start = Array.Find(sounds, sound => sound.name == "music-placeholder-investigation");
        start.source.Play();

        // we need this to not play every sound effect when GameStates are reset 
        GameState.Meta.inMainMenu.Value = true;
    }

    private void Start()
    {
        
    }

    // want to call this from outside the script
    // finds a sound in array by matching string.
    public void Play(string name)
    {
        if (!GameState.Meta.inMainMenu.Value || name == "music-placeholder-investigation")  // will be refused if you change the main theme music unless you swap this
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Refused to play sound " + name + " because we are in main menu");
        }

    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }


    // Below here are scripts that pertain to specific audio scenarios

    // Change music on encounter enter or encounter exit.
    public void EncounterChange()
    {
        try
        {
            // here is the code

            // On encounter enter:
            if (GameState.Meta.activeEncounter.Value != null)
            {
                //     Stop playing all sounds
                foreach (Sound s in sounds)
                {
                    s.source.Stop();
                }
                //     Then, 
                //     Play investigation theme
                Play("music-placeholder-investigation");
            }
            

            // On encounter exit:
            if (GameState.Meta.activeEncounter.Value == null)
            {
                //     Stop playing all sounds
                foreach (Sound s in sounds)
                {
                    s.source.Stop();
                }
                //     Then, 
                //     Play town theme
                Play("music-town");
            }
            


        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounter.OnChange -= EncounterChange;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounter.OnChange -= EncounterChange;
        }
    }

    // Change music on Berry Commotion enter.
    public void BerryCommotionChange()
    {
        try
        {
            // here is the code

            // On Commotion enter:
            //     Stop playing all sounds
            foreach (Sound s in sounds)
            {
                s.source.Stop();
            }
            //     Then, 
            //     Play investigation theme
            Play("music-placeholder-investigation");

        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Crouton.finishedBerryCommotion.OnChange -= BerryCommotionChange;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Crouton.finishedBerryCommotion.OnChange -= BerryCommotionChange;
        }
    }

    // Location change listener.
    public void LocationChange()
    {
        try
        {
            // here is the code

            // On Berry Commotion exit:

            // (if the player has not left the commotion before,
            // and the berry commotion has happened)

            if (!leftCommotion && GameState.NPCs.Crouton.finishedBerryCommotion.Value)
            {
                // Flip event-happened bit
                leftCommotion = true;
                //     Stop playing all sounds
                foreach (Sound s in sounds)
                {
                    s.source.Stop();
                }
                //     Then, 
                //     Play town theme
                Play("music-town");
            }
 
            
            

        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Crouton.finishedBerryCommotion.OnChange -= BerryCommotionChange;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.NPCs.Crouton.finishedBerryCommotion.OnChange -= BerryCommotionChange;
        }
    }

}
