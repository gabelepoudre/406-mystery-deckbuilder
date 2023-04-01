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
        float newheight = (NPCHeadshot.sprite.rect.height*600)/NPCHeadshot.sprite.rect.width;
        NPCHeadshot.rectTransform.sizeDelta = new Vector2(600, newheight);
    }
}
