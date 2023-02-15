using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPrefab : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject highlight;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        GameState.Meta.activeEncounter.Value.PlayCard(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.SetActive(false);
    }
}
