using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class that finds the gameobjects for each location within the zone and the functions for transitioning between them
public class MotelTransitions : MonoBehaviour
{
    GameObject exterior;
    GameObject interior;
    GameObject motelRoom;
    // Start is called before the first frame update
    public void Start()
    {
        exterior = GameObject.Find("Motel");
        interior = GameObject.Find("MotelLobby");
        motelRoom = GameObject.Find("MotelRoom");
        exterior.SetActive(true);
        interior.SetActive(false);
        motelRoom.SetActive(false);
    }
    public void MotelExteriorToLobby()
    {
        if (DialogueManager.Instance.DialogueActive) return;
        exterior.SetActive(false);
        interior.SetActive(true);
    }
    public void LobbyToMotelExterior()
    {
        if (DialogueManager.Instance.DialogueActive) return;
        interior.SetActive(false);
        exterior.SetActive(true);
    }
    public void LobbyToRoom()
    {
        if (DialogueManager.Instance.DialogueActive) return;
        interior.SetActive(false);
        motelRoom.SetActive(true);
    }
    public void RoomToLobby()
    {
        if (DialogueManager.Instance.DialogueActive) return;
        interior.SetActive(true);
        motelRoom.SetActive(false);
    }

}
