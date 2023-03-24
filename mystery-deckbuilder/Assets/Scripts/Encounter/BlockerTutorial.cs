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
    public GameObject promptDialogue;
    public GameObject promptBars;
    public GameObject promptBars1;
    public GameObject promptBars2;
    public GameObject promptDraw;
    public GameObject promptConversation;
    public GameObject promptConversation1;
    public GameObject promptHighlight;
    public GameObject promptDraw2;
    public GameObject promptPreperation;
    public GameObject promptDone;


    // Start is called before the first frame update
    void Start()//subscribe to stuff here
    {
        GameState.Meta.activeEncounter.OnChange += Next;
        GameState.Player.dailyDeck.OnChange += Next;
    }

    public void Next()
    {
        switch (count)
        {
            case 0:
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
            case 3:
                promptBars2.SetActive(false);
                drawBlocker.SetActive(false);
                promptDraw.SetActive(true);
                break;
            case 4:
                drawBlocker.SetActive(true);
                cardBlocker1.SetActive(false);
                promptDraw.SetActive(false);
                promptConversation.SetActive(true);
                break;
            case 5:
                promptConversation.SetActive(false);
                promptConversation1.SetActive(true);
                break;
            case 6:
                promptConversation1.SetActive(false);
                promptHighlight.SetActive(true);
                break;
            case 7:
                promptHighlight.SetActive(false);
                drawBlocker.SetActive(false);
                promptDraw2.SetActive(true);
                break;
            case 8:
                promptDraw2.SetActive(false);
                drawBlocker.SetActive(true);
                cardBlocker2.SetActive(false);
                promptPreperation.SetActive(true);
                break;
            case 9:
                promptPreperation.SetActive(false);
                promptDone.SetActive(true);
                break;
            case 10://unsub here
                GameState.Meta.activeEncounter.OnChange -= Next;
                GameState.Player.dailyDeck.OnChange -= Next;
                Destroy(this.gameObject);
                break;



        }

        count++;
    }


}
