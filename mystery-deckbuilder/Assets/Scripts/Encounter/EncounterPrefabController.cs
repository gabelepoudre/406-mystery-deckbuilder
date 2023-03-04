using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EncounterPrefabController : MonoBehaviour
{
    public Image npcHeadshot;
    public Image glubHeadshot;
    public GameObject complianceBar;
    public GameObject patienceBar;
    public GameObject cardPlaceMat;

    public GameObject redCard;
    public GameObject blueCard;
    public GameObject greenCard;
    public GameObject greyCard;

    private BarScript _complianceBarScript;
    private BarScript _patienceBarScript;
    private PlaceMatPrefabController _placeMatScript;
    private NPCEncounterSpriteController _npcHeadshotScript;

    void Start()
    {
        _complianceBarScript = complianceBar.GetComponent<BarScript>();
        _patienceBarScript = patienceBar.GetComponent<BarScript>();
        _placeMatScript = cardPlaceMat.GetComponent<PlaceMatPrefabController>();
    }

    public void Initialize(EncounterConfig config)
    {
        //_npcHeadshotScript = config.Opponent.gameObject.GetComponent<NPCEncounterSpriteController>();
    }

    public void PlaceCard(Card card)
    {
        // find an open index
        int idx = _placeMatScript.GetEmptyTransformIndex();

        // set card position to that index
        card.SetPosition(idx);

        // get transform
        Transform empty = _placeMatScript.GetTransformFromIndex(idx);

        // actually instantiate the card
        GameObject cardFrontend = null;
        switch (card.GetElement())
        {
            case "Intimidation":
                cardFrontend = Instantiate(redCard, empty.position, empty.rotation, _placeMatScript.gameObject.transform);  
                break;
            case "Sympathy":
                cardFrontend = Instantiate(blueCard, empty.position, empty.rotation, _placeMatScript.gameObject.transform);
                break;
            case "Persuasion":
                cardFrontend = Instantiate(greenCard, empty.position, empty.rotation, _placeMatScript.gameObject.transform);
                break;
            case "Preperation":
                cardFrontend = Instantiate(redCard, empty.position, empty.rotation, _placeMatScript.gameObject.transform);
                break;
        }

        // Initialize with the proper info
        CardPrefabController frontendController = cardFrontend.GetComponent<CardPrefabController>();
        card.SetAndInitializeFrontendController(frontendController);
    }

    public bool PlaceMatFull()
    {
        return _placeMatScript.IsFull();
    }
}
