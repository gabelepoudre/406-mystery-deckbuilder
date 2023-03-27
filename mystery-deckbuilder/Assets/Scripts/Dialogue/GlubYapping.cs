using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GlubYapping : MonoBehaviour
{

    public Image glub;
    public Sprite glubNeutral;
    public Sprite glubTalk;
    public float secondsToNeutralSwap;

    private bool _neutral = true;
    private int _neutralTimer;
    private bool _yapping = false;

    public void SwapYap()
    {
        if (_neutral)
        {
            glub.sprite = glubTalk;
            
        }
        else
        {
            glub.sprite = glubNeutral;
        }
        _neutral = !_neutral;
        BeginSwapTimer();
    }

    public void BeginSwapTimer()
    {
        _neutralTimer = (int)(secondsToNeutralSwap * 50);
    }

    public void OnGlubChange()
    {
        try
        {
            if (GameState.Player.glubTalkingInDialogue.Value)
            {
                _yapping = true;
                BeginSwapTimer();
            }
            else
            {
                _yapping = false;
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.glubTalkingInDialogue.OnChange -= OnGlubChange;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            GameState.Player.glubTalkingInDialogue.OnChange -= OnGlubChange;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        glub.sprite = glubNeutral;
        GameState.Player.glubTalkingInDialogue.OnChange += OnGlubChange;
    }

    void FixedUpdate()
    {
        if (_neutralTimer > 0)
        {
            _neutralTimer -= 1;
            if (_neutralTimer == 0)
            {
                if (_yapping)
                {
                    SwapYap();
                }
                else
                {
                    glub.sprite = glubNeutral;

                }
            }
        } 
    }
}
