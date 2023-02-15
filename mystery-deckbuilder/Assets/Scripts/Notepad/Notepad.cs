using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Notepad : MonoBehaviour
{
    //Variables
    public float numpages;
    public float numchapters;

    public GameObject notepadCanvas;
    public GameObject pageImage;

    public bool notebookActive;
    public int currentChapterID = 0;
    public int currentPageID = 0;

    public TMP_Text noteText;
    public TMP_Text chapterTitle;
    public TMP_Text pageTitle;
    public Dropdown tabs;

    public List<Chapter> chapterList = new List<Chapter>();
    public Chapter currentChapter;
    public Page currentPage;


    //Opens or closes the notepad when called
    public void OpenNotepad()
    {
        notebookActive = !notebookActive;
        notepadCanvas.SetActive(notebookActive);
    }

    //Changes the current chapter in the notebook
    public void ChangeTab(int chapID)
    {
        //Change current chapterID to the chapterID changed to
        currentChapterID = chapID;
        currentChapter = chapterList[currentChapterID];
        currentPageID=0;
       //display the new chapter
        DisplayNotes();
    }


    //Displays all current info to the notebook
    public void DisplayNotes()
    {
        //Display the chapter and page titles
        chapterTitle.text = currentChapter.GetTitle().ToString();
        pageTitle.text = currentPage.GetTitle().ToString();

        //Display each note the current page holds
        foreach(var item in currentPage.notes)
        {
            noteText.text = item.ToString();
        }    
    }


    //Move to the next page in the chapter
    public void NextPage()
    {
        //If next page, exists, go
        if(currentPageID < (currentChapter.GetNumPages() -1))
        {
            currentPageID += 1;
            DisplayNotes();
        }  
    }

    //Move to the previous page in the chapter
    public void PreviousPage()
    {
        //if previous page exists, go
        if(currentPageID > 0 )
        {
            currentPageID -= 1;
            DisplayNotes();
        }
    }

    public void SetDropdown()
    {
        //Set tab dropdown menu to contain each of the chapters within chapterList
        tabs.options.Clear();

        foreach (var item in chapterList)
        {
            tabs.options.Add(new Dropdown.OptionData() { text = item.title});

        }
        DropdownItemSelected(tabs);

        tabs.onValueChanged.AddListener(delegate{ DropdownItemSelected(tabs);});

    }

    //When dropdown item is selected, change to that chapter
    public void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        ChangeTab(index);
    }


    //Allows transition between pages of notebook using up and down arrow keys
    public void NavigateNotebook()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            NextPage();
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            PreviousPage();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Create Notebook that contains 3 chapters with 10 pages each
        //Chapter one
        Chapter one = new Chapter("Suspects");
        //Create pages for chapter one
        Page ch1p1 = new Page("Nibbles", pageImage);
        Page ch1p2 = new Page("Char2", pageImage);
        Page ch1p3 = new Page("Bear", pageImage);
        Page ch1p4 = new Page("Dog", pageImage);
        Page ch1p5 = new Page("Cat", pageImage);
        Page ch1p6 = new Page("Fish", pageImage);
        Page ch1p7 = new Page("Rat", pageImage);
        Page ch1p8 = new Page("Mouse", pageImage);
        Page ch1p9 = new Page("Lizard", pageImage);
        Page ch1p10 = new Page("Badger", pageImage);
        //add notes to the pages
        ch1p1.AddNotes("This is the nibbles note");
        ch1p2.AddNotes("This is the char2 note");
        ch1p3.AddNotes("This is the bear note");
        ch1p4.AddNotes("This is the dog note");
        ch1p5.AddNotes("This is the car note");
        ch1p6.AddNotes("This is the fish note");
        ch1p7.AddNotes("This is the rat note");
        ch1p8.AddNotes("This is the mouse note");
        ch1p9.AddNotes("This is the lizzard note");
        ch1p10.AddNotes("This is the badger note");
        //add the pages to the chapter
        one.AddPage(ch1p1);
        one.AddPage(ch1p2);
        one.AddPage(ch1p3);
        one.AddPage(ch1p4);
        one.AddPage(ch1p5);
        one.AddPage(ch1p6);
        one.AddPage(ch1p7);
        one.AddPage(ch1p8);
        one.AddPage(ch1p9);
        one.AddPage(ch1p10);
   
         //chapter two
        Chapter two = new Chapter("Zones");
        //create pages for chapter two
        Page ch2p1 = new Page("Main street", pageImage);
        Page ch2p2 = new Page("River", pageImage);
        Page ch2p3 = new Page("Restaruant", pageImage);
        Page ch2p4 = new Page("Church", pageImage);
        Page ch2p5 = new Page("Farm", pageImage);
        Page ch2p6 = new Page("Otherstreet", pageImage);
        Page ch2p7 = new Page("Motel", pageImage);
        Page ch2p8 = new Page("Garden", pageImage);
        Page ch2p9 = new Page("Pool", pageImage);
        Page ch2p10 = new Page("Graveyard", pageImage);
        //create notes for the pages
        ch2p1.AddNotes("This is the mainst note");
        ch2p2.AddNotes("This is the river note");
        ch2p3.AddNotes("This is the restar note");
        ch2p4.AddNotes("This is the church note");
        ch2p5.AddNotes("This is the farm note");
        ch2p6.AddNotes("This is the otherst note");
        ch2p7.AddNotes("This is the motel note");
        ch2p8.AddNotes("This is the garden note");
        ch2p9.AddNotes("This is the pool note");
        ch2p10.AddNotes("This is the grave note");
        //add the pages to the chapter
        two.AddPage(ch2p1);
        two.AddPage(ch2p2);
        two.AddPage(ch2p3);
        two.AddPage(ch2p4);
        two.AddPage(ch2p5);
        two.AddPage(ch2p6);
        two.AddPage(ch2p7);
        two.AddPage(ch2p8);
        two.AddPage(ch2p9);
        two.AddPage(ch2p10);

        //chapter three
        Chapter three = new Chapter("CH3");
        //create chapter 3 pages
        Page ch3p1 = new Page("Info", pageImage);
        Page ch3p2 = new Page("Idea 1", pageImage);
        Page ch3p3 = new Page("Idea 2", pageImage);
        Page ch3p4 = new Page("Idea 3", pageImage);
        Page ch3p5 = new Page("Idea 4", pageImage);
        Page ch3p6 = new Page("Idea 5", pageImage);
        Page ch3p7 = new Page("Idea 6", pageImage);
        Page ch3p8 = new Page("Idea 7", pageImage);
        Page ch3p9 = new Page("Idea 8", pageImage);
        Page ch3p10 = new Page("Idea 9", pageImage);
        //create notes for the pages
        ch3p1.AddNotes("This is the first note");
        ch3p2.AddNotes("This is the second note");
        ch3p3.AddNotes("This is the third note");
        ch3p4.AddNotes("This is the fourth note");
        ch3p5.AddNotes("This is the fifth note");
        ch3p6.AddNotes("This is the sixth note");
        ch3p7.AddNotes("This is the seventh note");
        ch3p8.AddNotes("This is the eighth note");
        ch3p9.AddNotes("This is the ninth note");
        ch3p10.AddNotes("This is the tenth note");
        //add the pages to the chapters
        three.AddPage(ch3p1);
        three.AddPage(ch3p2);
        three.AddPage(ch3p3);
        three.AddPage(ch3p4);
        three.AddPage(ch3p5);
        three.AddPage(ch3p6);
        three.AddPage(ch3p7);
        three.AddPage(ch3p8);
        three.AddPage(ch3p9);
        three.AddPage(ch3p10);
   
        //add each chapter to the chapter list
        chapterList.Add(one);
        chapterList.Add(two);
        chapterList.Add(three);

        //set the current chapter to be the first chapter
        currentChapter = chapterList[0];
        currentPage = one.pageList[currentPageID];

        //set the notebook to be not visible
        notebookActive = false;
        notepadCanvas.SetActive(notebookActive);

        //set chapter dropdown list 
        SetDropdown();

    }

    // Update is called once per frame
    void Update()
    {
        
        DisplayNotes();
        //update the current page
        currentPage = currentChapter.pageList[currentPageID];

        NavigateNotebook();

  

      

        
        
        
    }
}
