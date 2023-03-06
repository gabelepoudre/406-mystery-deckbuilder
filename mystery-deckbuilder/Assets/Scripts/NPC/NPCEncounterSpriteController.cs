using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPCEncounterSpriteController : MonoBehaviour
{

    public Sprite neutral;
    public Sprite happy;
    public Sprite angry;
    public Sprite stress;
    public Sprite worry;


    // Switch to Neutral Sprite
    public void GetNeutral(Image spriteImage)
    {
        spriteImage.sprite = neutral;
        
    }


    // Switch to Happy Sprite
    public void GetHappy(Image spriteImage)
    {
        spriteImage.sprite = happy;
        
    }


    // Switch to Angry Sprite
    public void GetAngry(Image spriteImage)
    {
        spriteImage.sprite = angry;
        
    }


    // Switch to Stress Sprite
    public void GetStress(Image spriteImage)
    {
        spriteImage.sprite = stress;
        
    }


    // Switch to Worry Sprite
    public void GetWorry(Image spriteImage)
    {
        spriteImage.sprite = worry;
        
    }

}
