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
    }

    private void Start()
    {
        // Play initial music
        Play("music-placeholder-investigation");
    }

    // want to call this from outside the script
    // finds a sound in array by matching string.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
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
                Play("music-placeholder-town");
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


}
