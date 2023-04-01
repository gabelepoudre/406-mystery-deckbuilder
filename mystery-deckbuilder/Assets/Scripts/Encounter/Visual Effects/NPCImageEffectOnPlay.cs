using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NPCImageEffectOnPlay : MonoBehaviour
{
    public Sprite npcReactionImage;
    public NPC linkedNPC;

    public bool getsMad = false;
    public bool sweats = false;
    public bool getFrustrated = false;

    private Image _attachedNPCImageInEncounter;
    private Image _reactionImage;

    private int flashTimerFrames = (int)(50*0.150);
    private int curFlashTimerFrames = 0;
    private int numFlashes = 0;
    private bool isFlashed = false;

    private bool isStretched = false;

    private bool haveSetNatural = false;  // apparently vector3 is non nullable
    private Vector3 naturalScale;

    public void OffsetImage()
    {
        _reactionImage.transform.position = new Vector3(_reactionImage.transform.position.x + linkedNPC.reactionOffsetX, _reactionImage.transform.position.y + linkedNPC.reactionOffsetY, _reactionImage.transform.position.z);
    }

    public void UpdateImage()
    {
        try
        {
            if (GameState.Meta.activeEncounter.Value != null)
            {
                if (_reactionImage == null)
                {
                    if (getsMad)
                    {
                        _reactionImage = GameState.Meta.activeEncounter.Value.GetEncounterController().MadImage;
                        OffsetImage();
                    }
                    else if (getFrustrated)
                    {
                        _reactionImage = GameState.Meta.activeEncounter.Value.GetEncounterController().FrustrationImage;
                        OffsetImage();
                    }
                    else if (sweats)
                    {
                        _reactionImage = GameState.Meta.activeEncounter.Value.GetEncounterController().SweatImage;
                        OffsetImage();
                    }
                    
                }
                else
                {
                    Debug.Log("Reaction image wasn't null, somehow");
                }
                _attachedNPCImageInEncounter = GameState.Meta.activeEncounter.Value.GetEncounterController().npcHeadshot;
                Debug.Log("npcHeadshot in encounter controller failed to set?" + _attachedNPCImageInEncounter == null);
            }
            else
            {
                Debug.Log("active encounter null on trigger");
                _attachedNPCImageInEncounter = null;
                _reactionImage = null;
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.activeEncounter.OnChange -= UpdateImage;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.activeEncounter.OnChange -= UpdateImage;
        }
    }

    public void HideReactionOnWinOrLoss()
    {
        try
        {
            if (GameState.Meta.activeEncounter.Value != null)
            {
                if (_reactionImage != null)
                {
                    _reactionImage.gameObject.SetActive(false);
                    isStretched = false;
                }
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.activeEncounterInLossScreen.OnChange -= HideReactionOnWinOrLoss;
            GameState.Meta.activeEncounterInWinScreen.OnChange -= HideReactionOnWinOrLoss;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.activeEncounterInLossScreen.OnChange -= HideReactionOnWinOrLoss;
            GameState.Meta.activeEncounterInWinScreen.OnChange -= HideReactionOnWinOrLoss;
        }
    }

    public void CardPlayed()
    {
        try
        {
            if (GameState.Meta.activeEncounterComplianceRaisedByAmount.Value > 40)
            {
                numFlashes = 12;
                curFlashTimerFrames = 1;
            }
            else if (GameState.Meta.activeEncounterComplianceRaisedByAmount.Value > 20)
            {
                numFlashes = 8;
                curFlashTimerFrames = 1;
            }
            else if (GameState.Meta.activeEncounterComplianceRaisedByAmount.Value > 0)
            {
                numFlashes = 4;
                curFlashTimerFrames = 1;
            }
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.activeEncounterComplianceRaisedByAmount.OnChange -= CardPlayed;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.activeEncounterComplianceRaisedByAmount.OnChange -= CardPlayed;
        }
    }

    public void StretchIfWeCan()
    {
        if (numFlashes > 0 && _attachedNPCImageInEncounter != null)
        {
            if (!haveSetNatural)
            {
                naturalScale = _attachedNPCImageInEncounter.transform.localScale;
                haveSetNatural = true;
            }

            if (numFlashes % 2 != 0)
            {
                if (!isStretched)
                {
                    _attachedNPCImageInEncounter.transform.localScale = new Vector3(_attachedNPCImageInEncounter.transform.localScale.x * 1.10f, _attachedNPCImageInEncounter.transform.localScale.y * 0.90f, _attachedNPCImageInEncounter.transform.localScale.z);
                    isStretched = true;
                }
            }
            else
            {
                if (isStretched)
                {
                    _attachedNPCImageInEncounter.transform.localScale = naturalScale;
                    isStretched = false;
                }
            }

        }
        else
        {
            if (haveSetNatural && isStretched)
            {
                _attachedNPCImageInEncounter.transform.localScale = naturalScale;
            }
        }
    }

    public void FlashNPCHeadshotIfWeCan()
    {
        if (_attachedNPCImageInEncounter != null && numFlashes > 0)
        {
            if (curFlashTimerFrames > 0)
            {
                curFlashTimerFrames -= 1;
            }
            else
            {
                numFlashes -= 1;
                curFlashTimerFrames = flashTimerFrames;
                if (isFlashed)
                {
                    _attachedNPCImageInEncounter.color = new Color(1, 1, 1, 1);
                    isFlashed = false;
                }
                else
                {
                    _attachedNPCImageInEncounter.color = new Color(0.8f, 0.8f, 0.8f, 1);
                    isFlashed = true;
                }
            }

            if (numFlashes == 0)
            {
                if (GameState.Meta.activeEncounterPatienceDroppedByAmount.Value > 0)
                {
                    TriggerReactionImage();
                }
            }
        }
        else if (isFlashed)
        {
            _attachedNPCImageInEncounter.color = new Color(1, 1, 1, 1);
            isFlashed = false;
        }
    }

    public void FadeReactionImageIfWeCan()
    {
        if (_reactionImage != null && _reactionImage.color.a > 0.0)
        { 
            float newAlpha = 0;
            // color randomly is 0 - 255 initially and then swaps to float 0 - 1 after set... why? I don't care
            if (_reactionImage.color.a > 2)
            {
                newAlpha = (_reactionImage.color.a / (float)255) - ((1 / (float)255)*2);
            }
            else
            {
                newAlpha = (_reactionImage.color.a - ((1 / (float)255) * 2));
            }
            
            if (newAlpha < 0.0)
            {
                newAlpha = 0f;
            }
            _reactionImage.color = new Color(_reactionImage.color.r, _reactionImage.color.g, _reactionImage.color.b, newAlpha);
        }
    }

    public void TriggerReactionImage()
    {
        _reactionImage.sprite = npcReactionImage;
        _reactionImage.color = new Color(_reactionImage.color.r, _reactionImage.color.g, _reactionImage.color.b, 255);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateImage();
        GameState.Meta.activeEncounter.OnChange += UpdateImage;
        GameState.Meta.activeEncounterComplianceRaisedByAmount.OnChange += CardPlayed;
        GameState.Meta.activeEncounterInLossScreen.OnChange += HideReactionOnWinOrLoss;
        GameState.Meta.activeEncounterInWinScreen.OnChange += HideReactionOnWinOrLoss;
    }

    private void FixedUpdate()
    {
        FadeReactionImageIfWeCan();
        FlashNPCHeadshotIfWeCan();
        StretchIfWeCan();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
