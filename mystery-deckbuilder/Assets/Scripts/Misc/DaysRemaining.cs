using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DaysRemaining : MonoBehaviour
{


    public TMP_Text daysRemainingText;
    // Start is called before the first frame update
    void Start()
    {
        daysRemainingText.text = (7 - GameState.Meta.currentDay.Value).ToString();
    }
}
