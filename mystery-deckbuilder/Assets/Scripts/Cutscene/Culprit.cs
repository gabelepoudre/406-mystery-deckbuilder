using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Culprit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public GameObject frameHighlight;
    public bool isBadGuy = false;
//    [SerializeField] private Object Ending;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isBadGuy)
        {
            SceneManager.LoadScene("Good Ending");
        }
        else
        {
            SceneManager.LoadScene("Bad Ending");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        frameHighlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        frameHighlight.SetActive(false);
    }
}
