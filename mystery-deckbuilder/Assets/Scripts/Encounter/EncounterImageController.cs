/*
 * author(s): Greta Strueby
 * 
 * Fixes scaling issues with NPC Encounter Sprites, sets all to same width
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EncounterImageController : MonoBehaviour
{
    
    public Image NPCHeadshot;
    public int scale = 600;
    
    public void ChangeSize()
    {
        try
        {
            // calculating the height of the encounter sprite, ensuring the width is always 600
            float NewHeight = (NPCHeadshot.sprite.rect.height * scale) / NPCHeadshot.sprite.rect.width;
            NPCHeadshot.rectTransform.sizeDelta = new Vector2(scale, NewHeight);
        }
        catch (MissingReferenceException e)
        {
            e.Message.Contains("e");
            NPCHeadshot.gameObject.SetActive(false);
        }
        catch (NullReferenceException e)
        {
            e.Message.Contains("e");
            NPCHeadshot.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeSize();
    }
}
