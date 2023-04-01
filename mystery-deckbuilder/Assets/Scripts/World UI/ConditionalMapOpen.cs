using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalMapOpen : MonoBehaviour
{
    public GameObject miniMap;

    public void OpenMap()
    {
        // not conditional
        miniMap.SetActive(true);
        GameState.Meta.mapIsOpen.Value = true;
    }
}
