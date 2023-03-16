using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public Object sceneToLaunch;

    // String - exact name of scene to load
    [Header("Scene index (found in File > Build Settings > Scenes in Build)")]
    [Header("Scene has to be in the game build to work. Will implement loading by scene name later")]
    [SerializeField] private int _sceneIndex = 1;

    public void Play() 
    {
        // Stop playing title theme
        FindObjectOfType<AudioManager>().Stop("music-placeholder-investigation");

        // Start playing intro theme
        FindObjectOfType<AudioManager>().Play("music-placeholder-town");

        SceneManager.LoadScene(_sceneIndex);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
