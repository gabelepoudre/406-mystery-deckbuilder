using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NotepadController helps to control the notepad and states within it
public class NotepadController : MonoBehaviour
{

    public GameObject notepadCanvas;
    public GameObject zone;
    public GameObject sus;
    public GameObject pause;
    public GameObject deck;

    public void OpenNotepad()
    {
        GameState.Meta.notepadActive.Value = !GameState.Meta.notepadActive.Value;
        notepadCanvas.SetActive(GameState.Meta.notepadActive.Value);

        sus.SetActive(true);
        pause.SetActive(false);
        deck.SetActive(false);
        zone.SetActive(false);


      
 
    }

    public void CloseNotepad()
    {
        GameState.Meta.notepadActive.Value = !GameState.Meta.notepadActive.Value;
        notepadCanvas.SetActive(GameState.Meta.notepadActive.Value);

    }

    public void SaveGame()
    {

    }


    public void LoadGame()
    {
       

    }

    public void QuitGame()
    {
        Debug.Log("quit pressed");
        //Open main menu
        //Application.Quit() will only work from a built application NOT in editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();

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
