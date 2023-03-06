using UnityEngine;
using UnityEngine.UI;

/**
 * generic script for controling a bar object
 */
public class BarScript : MonoBehaviour   
{
    public Slider slider;

    /**
     * sets both the bars max value and the bars current value(starting value)
     * used to set up a bar with a single function call
     */
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
