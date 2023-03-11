using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EffectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public GameObject effectTooltip;
    private IExecutableEffect _currentEffect;

    public void Display(IExecutableEffect effect)
    {
        _currentEffect = effect;
        image.color = effect.GetColor();
        gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        effectTooltip.SetActive(true);
        effectTooltip.GetComponent<EffectTooltipController>().SetTitle(_currentEffect.GetName());
        effectTooltip.GetComponent<EffectTooltipController>().SetDescription(_currentEffect.GetDescription());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        effectTooltip.SetActive(false);
    }
}
