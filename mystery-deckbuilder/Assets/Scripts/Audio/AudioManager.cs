using UnityEngine.Audio;
using System;
using UnityEngine;

// Based on "Introduction to AUDIO in Unity" by Brackeys (Youtube)
// USAGE:
//      Make sure this is placed in the first scene the game loads.
// TO PLAY A SOUND:
//      In any script:
//          FindObjectOfType<AudioManager>().Play("filename-of-sound");


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
    }

    private void Start()
    {
        // Play initial music
        Play("music-test");
    }

    // want to call this from outside the script
    // finds a sound in array by matching string.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
