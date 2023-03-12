using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * generic script for controling a bar object
 */
public class BarScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slider slider;
    public Text display;

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

    public int GetMax()
    {
        return (int)slider.maxValue;
    }

    public bool IsFull()
    {
        return GetMax() == GetValue();
    }

    public bool IsEmpty()
    {
        return GetValue() == 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        display.text = GetValue().ToString() + "/" + GetMax().ToString();
        display.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        display.gameObject.SetActive(false);
    }
}
