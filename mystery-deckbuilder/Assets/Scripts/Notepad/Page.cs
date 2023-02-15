using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page 
{


    public int numNotes;
    public string title;
    public GameObject img;
    public List<string> notes;

    // //Pages hold title, image, number of notes, and contain ID's

    public Page(string title, GameObject img)
    {
        this.title = title;
        this.numNotes = 0;
        this.notes = new List<string>();
        this.img = img;
    }

    //Set the number of notes in the page
    public void SetNumNotes(int x)
    {
        this.numNotes = x;

    }

    //Returns as an int the number of notes in the page
    public int GetNumNotes()
    {
        return this.numNotes;

    }

    //Returns as a string the title of the page
    public string GetTitle()
    {
        return this.title;

    }

    //Sets the title of the page
    public void SetTitle(string title)
    {
        this.title = title;

    }

    //Returns as a string the note at the given index
    public string GetNotesAt(int index)
    {
        return notes[index];

    }

    //Adds a note to the page
    public void AddNotes(string note)
    {
        notes.Add(note);
        numNotes +=1;

    }


}
