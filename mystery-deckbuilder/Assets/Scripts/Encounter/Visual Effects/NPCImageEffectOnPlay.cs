using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NPCImageEffectOnPlay : MonoBehaviour
{
    public Sprite npcReactionImage;

    public int reactionOffsetX = 0;
    public int reactionOffsetY = 0;

    private NPC _attachedNPC;
    private Image _attachedNPCImageInEncounter;
    private Image _reactionImage;

    public void UpdateImage()
    {
        try
        {
            if (GameState.Meta.activeEncounter.Value != null)
            {
                if (_reactionImage == null)
                {
                    _reactionImage = GameState.Meta.activeEncounter.Value.GetEncounterController().npcReactionSpawn;
                }
                _attachedNPCImageInEncounter = GameState.Meta.activeEncounter.Value.GetEncounterController().npcHeadshot;
            }
            else
            {
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

    public void CardPlayed()
    {
        try
        {
            TriggerReactionImage();
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.activeEncounterLastCardPlayedElement.OnChange -= CardPlayed;
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("E");
            GameState.Meta.activeEncounterLastCardPlayedElement.OnChange -= CardPlayed;
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
        _attachedNPC = gameObject.GetComponent<NPC>();

        GameState.Meta.activeEncounter.OnChange += UpdateImage;
        GameState.Meta.activeEncounterLastCardPlayedElement.OnChange += CardPlayed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
