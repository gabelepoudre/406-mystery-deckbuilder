using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//NotepadController helps to control the notepad and states within it
public class NotepadController : MonoBehaviour
{

    public GameObject notepadCanvas;
    public GameObject zone;
    public GameObject sus;
    public GameObject pause;
    public GameObject deck;

    //Opens the notepad and sets the first chapter (suspects) to be active
    public void OpenNotepad()
    {
        GameState.Meta.notepadActive.Value = !GameState.Meta.notepadActive.Value;
        notepadCanvas.SetActive(GameState.Meta.notepadActive.Value);

        sus.SetActive(true);
        pause.SetActive(false);
        deck.SetActive(false);
        zone.SetActive(false);
 
    }

    //Opens the notepad and sets the pause menu active
    public void OpenPause()
    {
        GameState.Meta.notepadActive.Value = !GameState.Meta.notepadActive.Value;
        notepadCanvas.SetActive(GameState.Meta.notepadActive.Value);

        sus.SetActive(false);
        pause.SetActive(true);
        deck.SetActive(false);
        zone.SetActive(false);
    }

    public void OpenDeck()
    {
        GameState.Meta.notepadActive.Value = !GameState.Meta.notepadActive.Value;
        notepadCanvas.SetActive(GameState.Meta.notepadActive.Value);

        sus.SetActive(false);
        pause.SetActive(false);
        deck.SetActive(true);
        zone.SetActive(false);
    }

    //Closes the notepad
    public void CloseNotepad()
    {
        GameState.Meta.notepadActive.Value = !GameState.Meta.notepadActive.Value;
        notepadCanvas.SetActive(GameState.Meta.notepadActive.Value);

    }

    //TODO - Save Game data
    public void SaveGame()
    {

    }


    //TODO - Load game data
    public void LoadGame()
    {
       

    }

    //Quits game and opens main menu
    public void QuitGame()
    {
        Debug.Log("quit pressed");
        //Open main menu
        SceneManager.LoadScene("MainMenu");
 

    }




    // Start is called before the first frame update
    void Start()
    {
        //set the notebook to be not visible
        GameState.Meta.notepadActive.Value = false;
        notepadCanvas.SetActive(GameState.Meta.notepadActive.Value);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
