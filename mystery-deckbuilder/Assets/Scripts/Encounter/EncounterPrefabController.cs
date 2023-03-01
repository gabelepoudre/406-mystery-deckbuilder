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
    private GameObject _encounterPrefab;
    private NPCSpriteController _npcHeadshotScript;
    private NPCSpriteController _glubHeadshotScript;

    void Start()
    {
            
    }
}
