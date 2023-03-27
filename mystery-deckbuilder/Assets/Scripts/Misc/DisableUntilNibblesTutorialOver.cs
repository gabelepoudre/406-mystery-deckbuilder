using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisableUntilNibblesTutorialOver : MonoBehaviour
{
    public void OnTutChange()
    {
        try
        {
            if (GameState.Meta.encounterTutorialComplete.Value)
            {
                this.gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.encounterTutorialComplete.OnChange -= OnTutChange;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.encounterTutorialComplete.OnChange -= OnTutChange;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OnTutChange();
        GameState.Meta.encounterTutorialComplete.OnChange += OnTutChange;
    }

}
