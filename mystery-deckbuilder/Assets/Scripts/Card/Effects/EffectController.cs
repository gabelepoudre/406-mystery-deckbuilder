using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour
{
    public Image image;
    private IExecutableEffect _currentEffect;
    public void Display(IExecutableEffect effect)
    {
        _currentEffect = effect;
        image.color = effect.GetColor();
        gameObject.SetActive(true);
    }
}
