using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPCSpriteController : MonoBehaviour
{

    public Image spriteImage;
    public Sprite neutral;
    public Sprite happy;
    public Sprite angry;
    public Sprite stress;
    public Sprite worry;


    // Switch to Neutral Sprite
    public void GetNeutral()
    {
        spriteImage.sprite = neutral;
        
    }


    // Switch to Happy Sprite
    public void GetHappy()
    {
        spriteImage.sprite = happy;
        
    }


    // Switch to Angry Sprite
    public void GetAngry()
    {
        spriteImage.sprite = angry;
        
    }


    // Switch to Stress Sprite
    public void GetStress()
    {
        spriteImage.sprite = stress;
        
    }


    // Switch to Worry Sprite
    public void GetWorry()
    {
        spriteImage.sprite = worry;
        
    }

}
