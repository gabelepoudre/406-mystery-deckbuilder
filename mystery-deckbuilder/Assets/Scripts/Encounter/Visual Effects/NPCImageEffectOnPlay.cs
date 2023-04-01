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
    private Transform _reactionPlacement;

    public void UpdateImage()
    {
        try
        {
            if (GameState.Meta.activeEncounter.Value != null)
            {
                if (_reactionPlacement == null)
                {
                    //_reactionPlacement = GameState.Meta.activeEncounter.Value.GetEncounterController().npcReactionSpawn;
                }
                _attachedNPCImageInEncounter = GameState.Meta.activeEncounter.Value.GetEncounterController().npcHeadshot;
            }
            else
            {
                _attachedNPCImageInEncounter = null;
                _reactionPlacement = null;
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

    // Start is called before the first frame update
    void Start()
    {
        _attachedNPC = gameObject.GetComponent<NPC>();

        GameState.Meta.activeEncounter.OnChange -= UpdateImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
