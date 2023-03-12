using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectTooltipController : MonoBehaviour
{
    public Text title;
    public Text description;

    public void SetTitle(string newTitle)
    {
        title.text = newTitle;
    }

    public void SetDescription(string newDescription)
    {
        description.text = newDescription;
    }
}
