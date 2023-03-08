using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

//PageNavigation allows navigation of the notepad UI as well as creation of chapters, pages, etc.
public class PageNavigation : MonoBehaviour
{

  
    public int numNotes;
    public GameObject seenImage;
    public GameObject unseenImage;

    public int currentChapterID = 0;

    public int currentPageID = 0;

    public TMP_Text noteText;

    public TMP_Text pageTitle;

    public Page currentPage;
    public int previousPageID;
    public List<Chapter> chapterList = new List<Chapter>();
    public Chapter currentChapter;


    //Move to the next page in the chapter
    public void NextPage()
    {
        //If next page, exists, go
        Debug.Log(currentPageID);
        
     if(currentPageID < (currentChapter.GetNumPages() -1))
     {
            currentPageID += 1;
            previousPageID +=1;
            DisplayNotes();
            Debug.Log("current page" + currentPageID);
     }
         
    }



    //Displays all current info to the notebook
    public void DisplayNotes()
    {
        

        if(currentPageID > 0)
        {
            //Set the previous image to be not active
            currentChapter.pageList[previousPageID].GetImage().SetActive(false);

        }
        if(currentPageID < (currentChapter.GetNumPages() -1))
        {
            currentChapter.pageList[currentPageID + 1].GetImage().SetActive(false);

        }

        

        currentPage.GetImage().SetActive(true);
        

       

        pageTitle.text = currentPage.GetTitle().ToString();

        //Display each note the current page holds
        foreach(var item in currentPage.notes)
        {
            noteText.text = item.ToString();
        } 

    }


    //Move to the previous page in the chapter
    public void PreviousPage()
    {
        //if previous page exists, go
        if(currentPageID > 0 )
        {
            // currentPage.GetImage().SetActive(false);
   
            currentPageID -= 1;
            DisplayNotes();
        }

        previousPageID = currentPageID + 1;
    }


    public void ChangedChapterSuspects()
    {
        Time.timeScale = 1;
  
        currentChapterID = 0;
        Debug.Log("changed chapter ID to " + currentChapterID);
        currentChapter = chapterList[currentChapterID];
        Debug.Log("changed cahpter");
        currentPage = currentChapter.pageList[0];
        DisplayNotes();
       
        
    }

    public void ChangedChapterDeck()
    {
        //in case coming from paused screen
        Time.timeScale = 1;
       
        currentChapterID = 3;
        currentChapter = chapterList[currentChapterID];


    }

    
    public void ChangedChapterZones()
    {
        //in case coming from paused screen
        Time.timeScale = 1;

        currentChapterID = 1;
        Debug.Log("current chapter id " + currentChapterID);
        Debug.Log("Size of chapterList " + chapterList.Count);
        
        currentChapter = chapterList[currentChapterID];
        currentPage = currentChapter.pageList[0];
        DisplayNotes();

    }


    public void PausePage()
    {
        
        currentPage.GetImage().SetActive(false);
        Time.timeScale = 0;
        currentChapterID = 2;
        currentChapter = chapterList[currentChapterID];
     


    }
    // Start is called before the first frame update
    void Awake()
    {


        Chapter p = new Chapter("Pause" );

        Chapter deckChapter = new Chapter("Deck");

        Chapter one = new Chapter("Suspect" );
        //Create pages for chapter one
        Page ch1p1 = new Page("Nibbles", unseenImage);
        Page ch1p2 = new Page("Char2", seenImage);
        Page ch1p3 = new Page("Bear", unseenImage);
        Page ch1p4 = new Page("Dog", seenImage);
        Page ch1p5 = new Page("Cat", unseenImage);
        Page ch1p6 = new Page("Fish", seenImage);
        Page ch1p7 = new Page("Rat", unseenImage);
        Page ch1p8 = new Page("Mouse", seenImage);
        Page ch1p9 = new Page("Lizard", unseenImage);
        Page ch1p10 = new Page("Badger", seenImage);
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
        Chapter two = new Chapter("Zone");
        //create pages for chapter two
        Page ch2p1 = new Page("Main street", unseenImage);
        Page ch2p2 = new Page("River", seenImage);
        Page ch2p3 = new Page("Restaruant", unseenImage);
        Page ch2p4 = new Page("Church", seenImage);
        Page ch2p5 = new Page("Farm", unseenImage);
        Page ch2p6 = new Page("Otherstreet", seenImage);
        Page ch2p7 = new Page("Motel", unseenImage);
        Page ch2p8 = new Page("Garden", seenImage);
        Page ch2p9 = new Page("Pool", unseenImage);
        Page ch2p10 = new Page("Graveyard", seenImage);
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


        chapterList.Add(one);//0
        chapterList.Add(two); //1
        chapterList.Add(p);//2
        chapterList.Add(deckChapter);//3
        Debug.Log(chapterList.Count);
        // ChangedChapterSuspects();
        currentChapter = chapterList[0];
        currentPageID = 0;
      
        currentPage = one.pageList[currentPageID];
        previousPageID = currentPageID -1;
        Debug.Log("current page" + currentPage.GetTitle());


    }

    // Update is called once per frame
    void Update()
    {

        
        DisplayNotes();
        //update the current page
        currentPage = currentChapter.pageList[currentPageID];
        
    }
}
