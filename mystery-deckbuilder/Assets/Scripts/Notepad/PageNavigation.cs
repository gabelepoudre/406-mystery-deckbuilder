using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 
using System;

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

    public List<Chapter> chapterList = new List<Chapter>();
    public Chapter currentChapter;

    public GameObject headshotObject;

    //Sprites to display

    public Sprite nibblesHeadshot;
    public  Sprite AustinHeadshot;
    public  Sprite AustynHeadshot;
    public  Sprite AlanHeadshot;
    public  Sprite MarkHeadshot;
    public  Sprite SamuelHeadshot;
    public  Sprite DougHeadshot;
    public  Sprite ElkSecretaryHeadshot;
    public  Sprite RatLeaderHeadshot;
    public  Sprite RatPrinceHeadshot;
    public  Sprite BigRatHeadshot;
    public  Sprite BeeHeadshot;
    public  Sprite MarryHeadshot;
    public  Sprite WolverineHeadshot;
    public  Sprite CroutonHeadshot;
    public  Sprite NinaHeadshot;
    public  Sprite MikeHeadshot;
    public  Sprite SpeckHeadshot;
    public  Sprite OslowHeadshot;
    public  Sprite ClayHeadshot;
    public  Sprite blackbearHeadshot;

    public Sprite mainstreetHeadshot;
    public Sprite motelheadshot;
    public Sprite RailYardHeadshot;
    public Sprite PostOfficeHeadshot;
    public Sprite berryfarmheadshot;
    public Sprite barHeadshot;
    public Sprite BreakfastHeadshot;
    public Sprite RatCaveHeadshot;
    public Sprite boxcarHeadshot;
    public Sprite beaverLodgeHeadshot;



    //Dictionary containing NPC names to their clues
    public static Dictionary<string, string> NPCClues = new()
    {
        {"Nibbles", "Mike's Perogies is favourite restaurant."}, {"Austin", "..."}, {"Austyn", "Beaver Union has resources to steal berries."}, {"Alan", "Mentioned unmarked mail being sent to town hall...suspicious."}, 
        {"Mark", "Hard feelings about competition between berry festival and Beaver Union's harvest festival could be possible motive"}, {"Samuel", "Says Beaver Union are the kind of people to steal berries."}, {"Doug", "Recommends checking out the Rat Prince bar and suspicious rail cars."}, 
        {"Elk Secretary", "Rat Mob may be involved."}, {"Rat Leader", "..."}, {"Rat Prince", "He stole the berries."}, 
        {"Big Rat", "He seemed upset with his son. We should track him down."}, {"Bee", "..."}, {"Marry", "Motel secretary...she probably doesn't know anything."}, 
        {"Wolverine", "He has to know something about the Rat Prince."}, {"Black Bear", "He could help me solve this case."}, {"Crouton", "Elk Secretary has been pulling the strings as Mayor."}, 
        {"Nina", "Says something weird may be happening to Crouton's budget and legislation."}, {"Mike", "..."}, {"Speck", "Dropped a suspicious note"}, 
        {"Oslow", "Said the rat tunnels were unusually quiet the night of the disappearance. Suggests we talk to Elk"}, {"Clay", "He can't remember a thing...Trademark symptoms of being drugged. But by who?"}
    };

    //Dictionary containing Zone names to the NPC's found there
        public static Dictionary<string, string> ZoneClues = new()
    {
        {"RealMainStreet", "Nibbles, Elk Secretary, and Crouton"}, {"Motel", "Marry and Clay"}, {"RailYard", "Speck and Mark"}, {"PostOfficeInside", "Alan and Samuel"}, 
        {"BerryFarm", "Austin and Austyn"}, {"Bar", "Rat Prince and Wolverine"}, {"BreakfastPalace", "Nina"}, 
        {"RatMobCave", "Oslow and Big Rat"}, {"Boxcar", "Rat Prince and Wolverine"}, {"LumberYard", "Doug"}, 
    };

    

    


    //Move to the next page in the chapter
    public void NextPage()
    {
        //If next page, exists, go
        GameState.Meta.menuNotepadPageSwitch.Raise();
    
        
     if(currentPageID < (currentChapter.GetNumPages() -1))
     {
            currentPageID += 1;
          
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
          
            currentChapter.pageList[currentPageID -1].GetImage().SetActive(false);
            

        }
        if(currentPageID < (currentChapter.GetNumPages() -1))
        {
            currentChapter.pageList[currentPageID + 1].GetImage().SetActive(false);
            

        }
        

        
        //display current image
        currentPage.GetImage().SetActive(true);
        // currentPage.GetHeadshot.SetActive(true);

        //display current headshot 
        headshotObject.GetComponent<Image>().sprite = currentPage.GetHeadshot();
        
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

  
    }


    //Change the current chapter to be the suspect chapter
    public void ChangedChapterSuspects()
    {

        currentChapterID = 0;
        currentChapter = chapterList[currentChapterID];
        currentPage = currentChapter.pageList[0];
        currentPageID = 0;
        DisplayNotes();
        GameState.Meta.menuNotepadTabSwitch.Raise();

    }

    //Change the current chapter to be the Deck chapter
    public void ChangedChapterDeck()
    {
        currentChapterID = 3;
        currentChapter = chapterList[currentChapterID];
        GameState.Meta.menuNotepadTabSwitch.Raise();
    }

    
    //Change the current chapter to be the zone chapter
    public void ChangedChapterZones()
    {
        //in case coming from paused screen


        currentChapterID = 1;


        currentChapter = chapterList[currentChapterID];
        
        currentPage = currentChapter.pageList[0];
        currentPageID = 0;
        DisplayNotes();
        GameState.Meta.menuNotepadTabSwitch.Raise();
    }


    //Change the current chapter to be the Pause chapter and pause the game
    public void PausePage()
    {
        
        currentPage.GetImage().SetActive(false);
   
        currentChapterID = 2;
        currentChapter = chapterList[currentChapterID];
        GameState.Meta.menuNotepadTabSwitch.Raise();
    }

    

    public void UpdateNotepad()
    {
         try
        {
            //Change the suspect images to seen if they have been encountered
        foreach(var pages in chapterList[0].GetPageList())
        {

            pages.ClearNotes();
    
            pages.AddNotes("Notes for " + pages.GetTitle() + System.Environment.NewLine);
       
            
           
            //if character has been encountered
             if(GameState.NPCs.npcNameToMet.ContainsKey(pages.GetTitle()))
             {
       

                if(GameState.NPCs.npcNameToMet[pages.GetTitle()].Value == true)
                {
             

                    
                    Debug.Log("npc is met");
                    pages.SetImage(seenImage);
             
                    headshotObject.GetComponent<Image>().sprite = pages.GetHeadshot();

                    //add encounters
                    pages.AddNotes("Encounters completed with suspect: " +GameState.NPCs.npcNameToEncountersCompleted[pages.GetTitle()].Value + System.Environment.NewLine);
                    pages.AddNotes("Encounters won against suspect: " +GameState.NPCs.npcNameToEncountersWon[pages.GetTitle()].Value+ System.Environment.NewLine );

                    //if an encounter has been won, display the clues
                    if(GameState.NPCs.npcNameToEncountersWon[pages.GetTitle()].Value > 0)
                    {
                        if(NPCClues.ContainsKey(pages.GetTitle()))
                        {
                            pages.AddNotes("My notes: " + NPCClues[pages.GetTitle()] + System.Environment.NewLine);

                        }
                    
                        

                    }
                   
        
                }        
            }
        }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            foreach (GameStateValue<bool> gamestate in GameState.NPCs.npcNameToMet.Values)
            {
                gamestate.OnChange -= UpdateNotepad;
            }
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            foreach (GameStateValue<bool> gamestate in GameState.NPCs.npcNameToMet.Values)
            {
                gamestate.OnChange -= UpdateNotepad;
            }
        }
    


        try{
        foreach(var pages in chapterList[1].GetPageList())
        {
            pages.ClearNotes();
            pages.AddNotes("Notes for " + pages.GetTitle() + System.Environment.NewLine);
            //if zone has been unlocked/visited

            if(GameState.Zones.zonesVisted.Value.Contains(pages.GetTitle()))
            {
                
                pages.SetImage(seenImage);


                    //if we are in the tutorial round
                    if(GameState.Meta.currentGameplayPhase.Value == GameState.Meta.GameplayPhases.Tutorial)
                    {
                        if(pages.GetTitle() == "RealMainStreet")
                        {
                            pages.AddNotes("Townfolk found here: Nibbles");
                        }
                        if(pages.GetTitle() == "Motel")
                        {
                            pages.AddNotes("Townfolk found here: Marry");

                        }
                    }
                    else
                    //if we are in gameplay phase 1
              
                        {
                             pages.AddNotes("Townfolk found here: " + ZoneClues[pages.GetTitle()] + System.Environment.NewLine);
                        }

                }

            else
            {
                pages.SetImage(unseenImage);

            } 
                 
        }

        }
        
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");

            GameState.Zones.zonesVisted.OnChange -= UpdateNotepad;
      
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Zones.zonesVisted.OnChange -= UpdateNotepad;
        }
  
        

    }

    // Start is called before the first frame update
    void Awake()
    {

        Chapter p = new Chapter("Pause" );

        Chapter deckChapter = new Chapter("Deck");

        Chapter one = new Chapter("Suspect" );
        //Create pages for chapter one
        Page ch1p1 = new Page("Nibbles", unseenImage, nibblesHeadshot);
        Page ch1p2 = new Page("Austin", unseenImage, AustinHeadshot);
        Page ch1p3 = new Page("Austyn", unseenImage, AustynHeadshot);
        Page ch1p4 = new Page("Alan", unseenImage, AlanHeadshot);
        Page ch1p5 = new Page("Mark", unseenImage, MarkHeadshot);
        Page ch1p6 = new Page("Samuel", unseenImage, SamuelHeadshot);
        Page ch1p7 = new Page("Doug", unseenImage, DougHeadshot);
        Page ch1p8 = new Page("Elk Secretary", unseenImage, ElkSecretaryHeadshot);
        Page ch1p9 = new Page("Rat Leader", unseenImage, RatLeaderHeadshot);
        Page ch1p10 = new Page("Rat Prince", unseenImage, RatPrinceHeadshot);
        Page ch1p11 = new Page("Big Rat", unseenImage, BigRatHeadshot);
        Page ch1p12 = new Page("Bee", unseenImage, BeeHeadshot);
        Page ch1p13 = new Page("Marry", unseenImage, MarryHeadshot);
        Page ch1p14 = new Page("Wolverine", unseenImage, WolverineHeadshot);
        Page ch1p15 = new Page("Black Bear", unseenImage, blackbearHeadshot);
        Page ch1p16 = new Page("Crouton", unseenImage, CroutonHeadshot);
        Page ch1p17 = new Page("Nina", unseenImage, NinaHeadshot);
        // Page ch1p18 = new Page("Mike", unseenImage, MikeHeadshot);
        Page ch1p19 = new Page("Speck", unseenImage, SpeckHeadshot);
        Page ch1p20 = new Page("Oslow", unseenImage, OslowHeadshot);
        Page ch1p21 = new Page("Clay", unseenImage, ClayHeadshot);


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
        one.AddPage(ch1p11);
        one.AddPage(ch1p12);
        one.AddPage(ch1p13);
        one.AddPage(ch1p14);
        one.AddPage(ch1p15);
        one.AddPage(ch1p16);
        one.AddPage(ch1p17);
        // one.AddPage(ch1p18);
        one.AddPage(ch1p19);
        one.AddPage(ch1p20);
        one.AddPage(ch1p21);


        //chapter two
        Chapter two = new Chapter("Zone");
        //create pages for chapter two
        Page ch2p1 = new Page("RealMainStreet", seenImage, mainstreetHeadshot);
        Page ch2p2 = new Page("Motel", unseenImage, motelheadshot);
        Page ch2p3 = new Page("RailYard", unseenImage, RailYardHeadshot);
        Page ch2p4 = new Page("PostOfficeInside", unseenImage, PostOfficeHeadshot);
        Page ch2p5 = new Page("BerryFarm", unseenImage, berryfarmheadshot);
        Page ch2p6 = new Page("Bar", unseenImage, barHeadshot);
        Page ch2p7 = new Page("BreakfastPalace", unseenImage, BreakfastHeadshot);
        
        Page ch2p9 = new Page("RatMobCave", unseenImage, RatCaveHeadshot);
        Page ch2p10 = new Page("Boxcar", unseenImage, boxcarHeadshot);
        Page ch2p8 = new Page("LumberYard", unseenImage, beaverLodgeHeadshot);

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

        //add mainstreet to notebook since it is first location
        GameState.Zones.zonesVisted.Value.Add("RealMainStreet");

        
    

    }

    void Start()
    {
        UpdateNotepad();
        foreach (GameStateValue<bool> gamestate in GameState.NPCs.npcNameToMet.Values)
        {
            gamestate.OnChange += UpdateNotepad;
        }
 
        GameState.Zones.zonesVisted.OnChange += UpdateNotepad;
        
    }

    // Update is called once per frame
    void Update()
    {

        DisplayNotes();
        //update the current page
        currentPage = currentChapter.pageList[currentPageID];
        
    }
}
