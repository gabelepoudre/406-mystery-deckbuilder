using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CardHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject cardHighlight;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        cardHighlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardHighlight.SetActive(false);
    }

}
