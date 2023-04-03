using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerTutorial : MonoBehaviour
{
    [SerializeField] private int count = 0; //what part the tutorial sequence is on

    public GameObject mainStreetBlockers;
    public GameObject encounterBlockers;
    public GameObject drawBlocker;
    public GameObject cardBlocker1;
    public GameObject cardBlocker2;
    public GameObject cardBlocker3;
    public GameObject promptTutorial;
    public GameObject promptDialogue;
    public GameObject promptBars;
    public GameObject promptBars1;
    public GameObject promptBars2;
    public GameObject promptDraw;
    public GameObject promptConversation;
    public GameObject promptConversation1;
    public GameObject promptConversation2;
    public GameObject promptHighlight;
    public GameObject promptDraw2;
    public GameObject promptPreperation;
    public GameObject promptPreperation1;
    public GameObject promptDone;
    public GameObject sblocker;
    public GameObject sblocker1;
    public GameObject sblocker2;

    public GameObject paws;
    public GameObject glub;
    public GameObject notepad;
    public GameObject map;
    public GameObject secret;
    public GameObject UIBlocker5;
    public GameObject UIBlocker1;
    public GameObject UIBlocker4;
    public GameObject UIBlocker3;

    public GameObject promptTutorial1;
    public GameObject sblocker3;
    public GameObject secretBlocker;
    public GameObject sblocker5;
    public GameObject tBlocker;
    public GameObject tBlocker1;
    public GameObject mapBlocker;
    public GameObject promptPaws;
    public GameObject promptPaws1;
    public GameObject promptGlub;
    public GameObject promptGlub1;
    public GameObject promptGlub2;
    public GameObject promptGlub3;
    public GameObject promptNotepad;
    public GameObject promptNotepad1;
    public GameObject promptMap;
    public GameObject promptMap1;
    public GameObject promptSecret;
    public GameObject promptComplete;


    // Start is called before the first frame update
    void Start()//subscribe to stuff here
    {
        if (GameState.Meta.encounterTutorialComplete.Value)
        {
            Destroy(this.gameObject);
        }
        else //don't let these run unless the whole tutorial is going to be completed
        {
            paws.SetActive(false);
            glub.SetActive(false);
            notepad.SetActive(false);
            map.SetActive(false);
            secret.SetActive(false);
            GameState.Meta.activeEncounter.OnChange += Next;
            GameState.Player.dailyDeck.OnChange += Next;
        } 
    }

    public void Next()
    {
        switch (count)
        {
            case 0:
                promptTutorial.SetActive(false);
                promptDialogue.SetActive(true);
                sblocker3.SetActive(true);
                break;
            case 1://start explaining bars
                mainStreetBlockers.SetActive(false);
                promptDialogue.SetActive(false);
                encounterBlockers.SetActive(true);
                promptBars.SetActive(true);
                break;
            case 2:
                promptBars.SetActive(false);
                promptBars1.SetActive(true);
                break;
            case 3:
                promptBars1.SetActive(false);
                promptBars2.SetActive(true);
                break;
            case 4://draw first card
                promptBars2.SetActive(false);
                drawBlocker.SetActive(false);
                promptDraw.SetActive(true);
                break;
            case 5://start explaining conversation cards
                drawBlocker.SetActive(true);
                cardBlocker1.SetActive(false);
                sblocker.SetActive(true);
                promptDraw.SetActive(false);
                promptConversation.SetActive(true);
                break;
            case 6:
                promptConversation.SetActive(false);
                promptConversation1.SetActive(true);
                break;
            case 7:
                GameState.Meta.activeEncounter.Value.GetEncounterController().HighlightLock = true;
                promptConversation1.SetActive(false);
                promptConversation2.SetActive(true);
                break;
            case 8:
                promptConversation2.SetActive(false);
                promptHighlight.SetActive(true);
                break;
            case 9://draw second card
                GameState.Meta.activeEncounter.Value.GetEncounterController().HighlightLock = false;
                if (GameState.Meta.activeEncounter.Value.GetEncounterController().GetHighlightedCard() != null)
                {
                    CardPrefabController c = GameState.Meta.activeEncounter.Value.GetEncounterController().GetHighlightedCard().GetComponent<CardPrefabController>();
                    c.UnHighlightCard();
                }
                promptHighlight.SetActive(false);
                drawBlocker.SetActive(false);
                promptDraw2.SetActive(true);
                break;
            case 10://start explaining prep cards
                promptDraw2.SetActive(false);
                drawBlocker.SetActive(true);
                cardBlocker1.SetActive(true);
                sblocker.SetActive(false);
                cardBlocker2.SetActive(false);
                sblocker1.SetActive(true);
                promptPreperation.SetActive(true);
                break;
            case 11://draw third card
                promptPreperation.SetActive(false);
                drawBlocker.SetActive(false);
                promptDraw2.SetActive(true);
                break;
            case 12://
                drawBlocker.SetActive(true);
                promptDraw2.SetActive(false);
                cardBlocker2.SetActive(true);
                sblocker1.SetActive(false);
                cardBlocker3.SetActive(false);
                sblocker2.SetActive(true);
                promptPreperation1.SetActive(true);
                break;
            case 13:
                promptPreperation1.SetActive(false);
                promptDone.SetActive(true);
                break;
            case 14://unsub here
                
                GameState.Player.dailyDeck.OnChange -= Next;
                GameState.Meta.activeEncounter.OnChange -= Next;
                GameState.Meta.dialogueActive.OnChange += Next;
                promptDone.SetActive(false);
                encounterBlockers.SetActive(false);
                if (GameState.Meta.activeEncounter.Value.GetEncounterController().GetHighlightedCard() != null)
                {
                    CardPrefabController c = GameState.Meta.activeEncounter.Value.GetEncounterController().GetHighlightedCard().GetComponent<CardPrefabController>();
                    c.UnHighlightCard();
                }
                break;
            case 15: //UI starts
                mainStreetBlockers.SetActive(true);
                sblocker3.SetActive(false);
                break;
            case 16:
                GameState.Meta.dialogueActive.OnChange -= Next; //prevent skipping though by reopening dialogue
                promptTutorial1.SetActive(true);
                break;
            case 17: //paws
                promptTutorial1.SetActive(false);
                promptPaws.SetActive(true);
                paws.SetActive(true);
                UIBlocker5.SetActive(true);
                break;
            case 18:
                promptPaws.SetActive(false);
                promptPaws1.SetActive(true);
                tBlocker.SetActive(true);
                UIBlocker3.SetActive(true);
                break;
            case 19://deck/glub
                promptPaws1.SetActive(false);
                promptGlub.SetActive(true);
                glub.SetActive(true);
                UIBlocker1.SetActive(true);
                tBlocker.SetActive(false);
                UIBlocker3.SetActive(false);
                break;
            case 20:
                promptGlub.SetActive(false);
                promptGlub1.SetActive(true);
                tBlocker.SetActive(true);
                UIBlocker3.SetActive(true);
                tBlocker1.SetActive(true);
                break;
            case 21:
                promptGlub1.SetActive(false);
                promptGlub3.SetActive(true);
                break;
            case 22:
                promptGlub3.SetActive(false);
                promptGlub2.SetActive(true);
                break;
            case 23://notepad
                promptGlub2.SetActive(false);
                promptNotepad.SetActive(true);
                tBlocker.SetActive(false);
                UIBlocker3.SetActive(false);
                tBlocker1.SetActive(false);
                notepad.SetActive(true);
                UIBlocker4.SetActive(true);
                break;
            case 24:
                promptNotepad.SetActive(false);
                promptNotepad1.SetActive(true);
                tBlocker.SetActive(true);
                UIBlocker3.SetActive(true);
                break;
            case 25://map
                promptNotepad1.SetActive(false);
                promptMap.SetActive(true);
                tBlocker.SetActive(false);
                map.SetActive(true);
                UIBlocker3.SetActive(true);
                break;
            case 26:
                promptMap.SetActive(false);
                promptMap1.SetActive(true);
                mapBlocker.SetActive(true);
                break;
            case 27:
                promptMap1.SetActive(false);
                promptSecret.SetActive(true);
                secret.SetActive(true);
                //secretBlocker.SetActive(false);
                mapBlocker.SetActive(false);
                break;
            case 28:
                promptSecret.SetActive(false);
                promptComplete.SetActive(true);
                sblocker5.SetActive(true);
                break;
            case 29:
                GameState.Meta.encounterTutorialComplete.Value = true;
                Destroy(this.gameObject);
                break;


        }

        count++;
    }
    public void Skip()
    {
        paws.SetActive(true);
        glub.SetActive(true);
        notepad.SetActive(true);
        map.SetActive(true);
        secret.SetActive(true);
        GameState.Meta.activeEncounter.OnChange -= Next;
        GameState.Player.dailyDeck.OnChange -= Next;
        GameState.Meta.encounterTutorialComplete.Value = true;
        Destroy(this.gameObject);
    }


}
