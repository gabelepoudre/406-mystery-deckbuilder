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
    public int scale = 600;
    
    public void ChangeSize()
    {
        // calculating the height of the encounter sprite, ensuring the width is always 600
        float NewHeight = (NPCHeadshot.sprite.rect.height * scale) / NPCHeadshot.sprite.rect.width;
        NPCHeadshot.rectTransform.sizeDelta = new Vector2(scale, NewHeight);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeSize();
    }
}
