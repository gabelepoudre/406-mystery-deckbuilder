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
    
        
     if(currentPageID < (currentChapter.GetNumPages() -1))
     {
            currentPageID += 1;
            previousPageID +=1;
            DisplayNotes();
     }
         
    }



    //Displays all current info to the notebook
    public void DisplayNotes()
    {
        //hide previous images
        if(currentPageID > 0)
        {
            //Set the previous image to be not active
            currentChapter.pageList[previousPageID].GetImage().SetActive(false);

        }
        if(currentPageID < (currentChapter.GetNumPages() -1))
        {
            currentChapter.pageList[currentPageID + 1].GetImage().SetActive(false);

        }

        
        //display current image
        currentPage.GetImage().SetActive(true);
        
        pageTitle.text = currentPage.GetTitle().ToString();

        noteText.text = "";
        //Display each note the current page holds
        foreach(var item in currentPage.notes)
        {
            noteText.text = noteText.text + " " + item.ToString();
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


    //Change the current chapter to be the suspect chapter
    public void ChangedChapterSuspects()
    {
        Time.timeScale = 1;
        currentChapterID = 0;
        currentChapter = chapterList[currentChapterID];
        currentPage = currentChapter.pageList[0];
        DisplayNotes();
          
    }

    //Change the current chapter to be the Deck chapter
    public void ChangedChapterDeck()
    {
        //in case coming from paused screen
        Time.timeScale = 1;
        currentChapterID = 3;
        currentChapter = chapterList[currentChapterID];
    }

    
    //Change the current chapter to be the zone chapter
    public void ChangedChapterZones()
    {
        //in case coming from paused screen
        Time.timeScale = 1;

        currentChapterID = 1;

        currentChapter = chapterList[currentChapterID];
        currentPage = currentChapter.pageList[0];
        DisplayNotes();

    }


    //Change the current chapter to be the Pause chapter and pause the game
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
        Page ch1p2 = new Page("Austin", unseenImage);
        Page ch1p3 = new Page("Austyn", unseenImage);
        Page ch1p4 = new Page("Alan", unseenImage);
        Page ch1p5 = new Page("Mark", unseenImage);
        Page ch1p6 = new Page("Samuel", unseenImage);
        Page ch1p7 = new Page("Doug", unseenImage);
        Page ch1p8 = new Page("Elk Secretary", unseenImage);
        Page ch1p9 = new Page("Rat Leader", unseenImage);
        Page ch1p10 = new Page("Rat Prince", unseenImage);
        Page ch1p11 = new Page("Big Rat", unseenImage);
        Page ch1p12 = new Page("Bee", unseenImage);
        Page ch1p13 = new Page("Marry", unseenImage);
        Page ch1p14 = new Page("Wolverine", unseenImage);
        Page ch1p15 = new Page("Black Bear", unseenImage);
        Page ch1p16 = new Page("Crouton", unseenImage);
        Page ch1p17 = new Page("Nina", unseenImage);
        Page ch1p18 = new Page("Mike", unseenImage);
        Page ch1p19 = new Page("Speck", unseenImage);
        Page ch1p20 = new Page("Oslow", unseenImage);
        Page ch1p21 = new Page("Clay", unseenImage);


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
        Page ch2p1 = new Page("Mainstreet", seenImage);
        Page ch2p2 = new Page("River", unseenImage);
        Page ch2p3 = new Page("Restaruant", unseenImage);
        Page ch2p4 = new Page("Church", unseenImage);
        Page ch2p5 = new Page("Farm", unseenImage);
        Page ch2p6 = new Page("Otherstreet", unseenImage);
        Page ch2p7 = new Page("Motel", unseenImage);
        Page ch2p8 = new Page("Garden", unseenImage);
        Page ch2p9 = new Page("Pool", unseenImage);
        Page ch2p10 = new Page("Graveyard", unseenImage);

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
       
        
        currentChapter = chapterList[0];
        currentPageID = 0;
      
        currentPage = one.pageList[currentPageID];
    


        previousPageID = currentPageID -1;


        //Change the suspect images to seen if they have been encountered
        //for each character in suspects
        foreach(var pages in chapterList[0].GetPageList())
        {
            pages.AddNotes("Notes for " + pages.GetTitle());
            //if character has been encountered
             if(GameState.NPCs.npcNameToMet.ContainsKey(pages.GetTitle()))
             {
                if(GameState.NPCs.npcNameToMet[pages.GetTitle()].Value == true)
                {
                    pages.SetImage(seenImage);
                    pages.AddNotes("Encounters completed with suspect: " +GameState.NPCs.npcNameToEncountersCompleted[pages.GetTitle()].Value );
                    pages.AddNotes("Encounters won against suspect: " +GameState.NPCs.npcNameToEncountersWon[pages.GetTitle()].Value );

                }
               

            }

        }

        foreach(var pages in chapterList[1].GetPageList())
        {
            pages.AddNotes("Notes for zone " + pages.GetTitle());
            //if zone has been unlocked/visited
            if(GameState.Zones.zonesVisted.Contains(pages.GetTitle()))
            {
                pages.SetImage(seenImage);

            }
                    
        }


    }

    // Update is called once per frame
    void Update()
    {

        
        DisplayNotes();
        //update the current page
        currentPage = currentChapter.pageList[currentPageID];
        
    }
}
