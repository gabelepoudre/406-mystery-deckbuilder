using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterScriptRevised : MonoBehaviour
{

    public GameObject redCard;
    public GameObject blueCard;
    public GameObject greenCard;
    public GameObject prepCard;

    private GameObject InitializeCardPrefabGivenCardClass(Card card, Transform location)
    {
        GameObject frontendCard = null;
        switch (card.GetElement())
        {
            case "Intimidation":
                frontendCard = Instantiate(redCard, location);

                break;
            case "Sympathy":
                frontendCard = Instantiate(blueCard, location);
                break;
            case "Persuasion":
                frontendCard = Instantiate(greenCard, location);
                break;
            case "Preperation":
                frontendCard = Instantiate(redCard, location);
                break;
        }
        card.SetAndInitializeFrontendController(frontendCard.GetComponent<CardPrefabController>());
        return frontendCard;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
