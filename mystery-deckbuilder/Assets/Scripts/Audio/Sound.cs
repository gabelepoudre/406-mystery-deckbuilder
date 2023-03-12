using UnityEngine.Audio;
using UnityEngine;

// Based on "Introduction to AUDIO in Unity" by Brackeys (Youtube)

// Allows an array of Sound to appear in the Unity editor
[System.Serializable]

public class Sound
{
    // name searchable by AudioManager!
    public string name;

    public AudioClip clip;

    // toggle whether or not audio loops!
    public bool loop;
    
    // Gives it a volume slider
    [Range(0f, 1f)]
    public float volume;

    // Gives it a pitch slider
    [Range(0.1f, 3f)]
    public float pitch;

    // Hides it in Unity editor
    [HideInInspector]
    public AudioSource source;


}
