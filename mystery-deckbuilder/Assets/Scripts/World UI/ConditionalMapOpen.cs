using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalMapOpen : MonoBehaviour
{
    public GameObject miniMap;

    public void OpenMap()
    {
        if (!GameState.Meta.dialogueActive.Value)
        {
            miniMap.SetActive(true);
        }
    }
}
