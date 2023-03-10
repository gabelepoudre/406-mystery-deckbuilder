using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{

    // String - exact name of scene to load
    [Header("Scene index (found in File > Build Settings > Scenes in Build)")]
    [Header("Scene has to be in the game build to work. Will implement loading by scene name later")]
    [SerializeField] private int _sceneIndex = 1;

    public void Play() 
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
