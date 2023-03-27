using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GlubReactToCardPlay : MonoBehaviour
{
    public Image glub;
    public Sprite glubNeutral;
    public Sprite glubReactToPersuation;
    public Sprite glubReacttoIntimidation;
    public Sprite glubReactToSympathy;
    public Sprite glubReactToPrepration;
    public float secondsToNeutralSwap;

    private int _neutralTimer;
    
    public void SetNeutral()
    {
        glub.sprite = glubNeutral;
    }

    public void BeginNeutralTimer()
    {
        _neutralTimer = (int)(secondsToNeutralSwap * 50);
    }

    public void OnGlubChange()
    {
        try
        {
            switch (GameState.Meta.activeEncounterLastCardPlayedElement.Value)
            {
                case "Intimidation":
                    glub.sprite = glubReacttoIntimidation;
                    BeginNeutralTimer();
                    break;
                case "Persuasion":
                    glub.sprite = glubReactToPersuation;
                    BeginNeutralTimer();
                    break;
                case "Sympathy":
                    glub.sprite = glubReactToSympathy;
                    BeginNeutralTimer();
                    break;
                case "Preparation":
                    glub.sprite = glubReactToPrepration;
                    BeginNeutralTimer();
                    break;
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterLastCardPlayedElement.OnChange -= OnGlubChange;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Meta.activeEncounterLastCardPlayedElement.OnChange -= OnGlubChange;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetNeutral();
        GameState.Meta.activeEncounterLastCardPlayedElement.OnChange += OnGlubChange;
    }

    void FixedUpdate()
    {
        if (_neutralTimer > 0)
        {
            _neutralTimer -= 1;
            if (_neutralTimer == 0)
            {
                SetNeutral();
            }
        }
    }     


}
