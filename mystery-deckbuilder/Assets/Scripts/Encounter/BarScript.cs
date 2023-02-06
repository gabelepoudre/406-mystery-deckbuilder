using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * generic script for controling a bar object
 */
public class BarScript : MonoBehaviour   
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(int maxVal, int val)
    {
        SetMax(maxVal);
        SetValue(val);
    }
    public void SetMax(int maxVal)
    {
        slider.maxValue = maxVal;
    }

    public void SetValue(int val)
    {
        slider.value = val;
    }

    public int GetValue()
    {
        return (int)slider.value;
    }
}
