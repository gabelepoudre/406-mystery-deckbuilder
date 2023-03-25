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


    // Start is called before the first frame update
    void Start()//subscribe to stuff here
    {
        if (GameState.Meta.encounterTutorialComplete.Value)
        {
            Destroy(this.gameObject);
        }
        else //don't let these run unless the whole tutorial is going to be completed
        {
            GameState.Meta.activeEncounter.OnChange += Next;
            GameState.Player.dailyDeck.OnChange += Next;
        } 
    }

    public void Next()
    {
        switch (count)
        {
            case 0://start explaining bars
                mainStreetBlockers.SetActive(false);
                promptDialogue.SetActive(false);
                encounterBlockers.SetActive(true);
                promptBars.SetActive(true);
                break;
            case 1:
                promptBars.SetActive(false);
                promptBars1.SetActive(true);
                break;
            case 2:
                promptBars1.SetActive(false);
                promptBars2.SetActive(true);
                break;
            case 3://draw first card
                promptBars2.SetActive(false);
                drawBlocker.SetActive(false);
                promptDraw.SetActive(true);
                break;
            case 4://start explaining conversation cards
                drawBlocker.SetActive(true);
                cardBlocker1.SetActive(false);
                sblocker.SetActive(true);
                promptDraw.SetActive(false);
                promptConversation.SetActive(true);
                break;
            case 5:
                promptConversation.SetActive(false);
                promptConversation1.SetActive(true);
                break;
            case 6:
                promptConversation1.SetActive(false);
                promptConversation2.SetActive(true);
                break;
            case 7:
                promptConversation2.SetActive(false);
                promptHighlight.SetActive(true);
                break;
            case 8://draw second card
                promptHighlight.SetActive(false);
                drawBlocker.SetActive(false);
                promptDraw2.SetActive(true);
                break;
            case 9://start explaining prep cards
                promptDraw2.SetActive(false);
                drawBlocker.SetActive(true);
                cardBlocker1.SetActive(true);
                sblocker.SetActive(false);
                cardBlocker2.SetActive(false);
                sblocker1.SetActive(true);
                promptPreperation.SetActive(true);
                break;
            case 10://draw third card
                promptPreperation.SetActive(false);
                drawBlocker.SetActive(false);
                promptDraw2.SetActive(true);
                break;
            case 11://
                drawBlocker.SetActive(true);
                promptDraw2.SetActive(false);
                cardBlocker2.SetActive(true);
                sblocker1.SetActive(false);
                cardBlocker3.SetActive(false);
                sblocker2.SetActive(true);
                promptPreperation1.SetActive(true);
                break;
            case 12:
                promptPreperation1.SetActive(false);
                promptDone.SetActive(true);
                break;
            case 13://unsub here
                GameState.Meta.activeEncounter.OnChange -= Next;
                GameState.Player.dailyDeck.OnChange -= Next;
                GameState.Meta.encounterTutorialComplete.Value = true;
                Destroy(this.gameObject);
                break;



        }

        count++;
    }


}
