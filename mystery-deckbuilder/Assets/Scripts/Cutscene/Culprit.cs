using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Culprit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public GameObject frameHighlight;
    [SerializeField] private Object Ending;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        SceneManager.LoadScene(Ending.name);
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
