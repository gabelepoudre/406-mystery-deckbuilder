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

    private BarScript _complianceBarScript;
    private BarScript _patienceBarScript;
    private PlaceMatPrefabController _placeMatScript;
    private NPCEncounterSpriteController _npcHeadshotScript;

    void Start()
    {
        _complianceBarScript = complianceBar.GetComponent<BarScript>();
        _patienceBarScript = patienceBar.GetComponent<BarScript>();
        _placeMatScript = cardPlaceMat.GetComponent<PlaceMatPrefabController>();
        _npcHeadshotScript = npcHeadshot.GetComponent<NPCEncounterSpriteController>();
    }

    public void Initialize(EncounterConfig config)
    {
        _npcHeadshotScript = config.Opponent.gameObject.GetComponent<NPCEncounterSpriteController>();
    }

}
