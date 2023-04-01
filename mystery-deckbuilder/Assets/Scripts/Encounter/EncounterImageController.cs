/*
 * author(s): Greta Strueby
 * 
 * Fixes scaling issues with NPC Encounter Sprites, sets all to same width
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterImageController : MonoBehaviour
{
    
    public Image NPCHeadshot;
    
    // Start is called before the first frame update
    void Start()
    {
        // calculating the height of the encounter sprite, ensuring the width is always 600
        float NewHeight = (NPCHeadshot.sprite.rect.height*600)/NPCHeadshot.sprite.rect.width;
        NPCHeadshot.rectTransform.sizeDelta = new Vector2(600, NewHeight);
    }
}
