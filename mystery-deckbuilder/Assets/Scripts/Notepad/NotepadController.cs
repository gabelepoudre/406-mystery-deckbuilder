using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//NotepadController helps to control the notepad and states within it
public class NotepadController : MonoBehaviour
{

    public GameObject notepadCanvas;
    public GameObject zone;
    public GameObject sus;
    public GameObject pause;
    public GameObject deck;
    public GameObject map;

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
    public void TutOpenSus()
    {
        try
        {
            if (GameState.Meta.tutProgress.Value == 24)
            {
                OpenNotepad();
                if (!GameState.Meta.notepadActive.Value)
                {
                    OpenNotepad();
                }
                Debug.Log("EEEEEEE");
            }

        }
        catch (MissingReferenceException e)
        {
            GameState.Meta.tutProgress.OnChange -= TutOpenSus;
        }
        catch (NullReferenceException e)
        {
            GameState.Meta.tutProgress.OnChange -= TutOpenSus;
        }
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
    public void TutOpenPause()
    {
        try
        {
            if (GameState.Meta.tutProgress.Value == 18)
            {
                OpenPause();
                if (!GameState.Meta.notepadActive.Value)
                {
                    OpenPause();
                }
            }

        }
        catch (MissingReferenceException e)
        {
            GameState.Meta.tutProgress.OnChange -= TutOpenPause;
        }
        catch (NullReferenceException e)
        {
            GameState.Meta.tutProgress.OnChange -= TutOpenPause;
        }
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
    public void TutOpenDeck()
    {
        try
        {
            if (GameState.Meta.tutProgress.Value == 20)
            {
                OpenDeck();
                if (!GameState.Meta.notepadActive.Value)
                {
                    OpenDeck();
                }
                Debug.Log("EEEEEEE");
            }

        }
        catch (MissingReferenceException e)
        {
            GameState.Meta.tutProgress.OnChange -= TutOpenDeck;
        }
        catch (NullReferenceException e)
        {
            GameState.Meta.tutProgress.OnChange -= TutOpenDeck;
        }
    }

    //Closes the notepad
    public void CloseNotepad()
    {
        GameState.Meta.notepadActive.Value = !GameState.Meta.notepadActive.Value;
        notepadCanvas.SetActive(GameState.Meta.notepadActive.Value);

    }
    public void TutClose()
    {
        try
        {
            if (GameState.Meta.tutProgress.Value == 25)
            {
                CloseNotepad();
                if (GameState.Meta.notepadActive.Value)
                {
                    CloseNotepad();
                }
                Debug.Log("EEEEEEE");
            }

        }
        catch (MissingReferenceException e)
        {
            GameState.Meta.tutProgress.OnChange -= TutClose;
        }
        catch (NullReferenceException e)
        {
            GameState.Meta.tutProgress.OnChange -= TutClose;
        }
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

        GameState.Meta.tutProgress.OnChange += TutOpenPause;
        GameState.Meta.tutProgress.OnChange += TutOpenDeck;
        GameState.Meta.tutProgress.OnChange += TutOpenSus;
        GameState.Meta.tutProgress.OnChange += TutClose;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
