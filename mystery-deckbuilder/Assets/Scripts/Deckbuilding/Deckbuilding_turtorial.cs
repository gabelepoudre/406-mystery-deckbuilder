using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deckbuilding_turtorial : MonoBehaviour
{
    [SerializeField] private int count = 0; //what part the tutorial sequence is on

    public GameObject ThisIsYourDeck;
    public GameObject YourDeckMax21;
    public GameObject YouPlayCard;
    public GameObject YouRegainCardByReast;
    public GameObject ThisArrows;
    public GameObject ThisIsYourCollection;
    public GameObject ThisIsCurrentDeck;
    public GameObject ClearAll;
    public GameObject UndoAll;
    public GameObject WakeUp;
    public GameObject WakeUpWhenYouFinish;

    // Start is called before the first frame update
    void Start()//subscribe to stuff here
    {
        if (GameState.Meta.DeckBuildingTutorialComplete.Value)
        {
            Destroy(this.gameObject);
        }
        else { Next(); }

    }

    public void Next()
    {
        switch (count)
        {
            case 0:
                ThisIsYourDeck.SetActive(true); 
                break;
            case 1:
                ThisIsYourDeck.SetActive(false);
                YourDeckMax21.SetActive(true);
                break; 
            case 2:
                YourDeckMax21.SetActive(false);
                YouPlayCard.SetActive(true);
                break;
            case 3:
                YouPlayCard.SetActive(false);
                YouRegainCardByReast.SetActive(true);
                break;
            case 4:
                YouRegainCardByReast.SetActive(false);
                ThisArrows.SetActive(true);
                break;
            case 5:
                ThisArrows.SetActive(false);
                ThisIsYourCollection.SetActive(true);
                break;
            case 6:
                ThisIsYourCollection.SetActive(false);
                ThisIsCurrentDeck.SetActive(true);
                break;
            case 7:
                ThisIsCurrentDeck.SetActive(false);
                ClearAll.SetActive(true);
                break;
            case 8:
                ClearAll.SetActive(false);
                UndoAll.SetActive(true);
                break;
            case 9:
                UndoAll.SetActive(false);
                WakeUp.SetActive(true);
                break;
            case 10:
                WakeUp.SetActive(false);
                WakeUpWhenYouFinish.SetActive(true);
                break; 
            case 11:
                GameState.Meta.DeckBuildingTutorialComplete.Value = true;
                WakeUpWhenYouFinish.SetActive(false);
                Destroy(this.gameObject);
                break;
        }
        count++;
    }
    
        
    
}
