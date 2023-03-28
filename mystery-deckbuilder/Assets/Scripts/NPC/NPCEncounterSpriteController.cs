using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPCEncounterSpriteController : MonoBehaviour
{

    public Sprite phaseOne;
    public Sprite PhaseTwo;
    public Sprite PhaseThree;
    public Sprite PhaseFour;


    // Switch to Phase Two Sprite
    public void GetPhaseTwo(Image spriteImage)
    {
        spriteImage.sprite = PhaseTwo;
        
    }


    // Switch to Phase One Sprite
    public void GetPhaseOne(Image spriteImage)
    {
        spriteImage.sprite = phaseOne;
        
    }


    // Switch to Phase Four Sprite
    public void GetPhaseFour(Image spriteImage)
    {
        spriteImage.sprite = PhaseFour;
        
    }


    // Switch to Phase Three Sprite
    public void GetPhaseThree(Image spriteImage)
    {
        spriteImage.sprite = PhaseThree;
        
    }


}
