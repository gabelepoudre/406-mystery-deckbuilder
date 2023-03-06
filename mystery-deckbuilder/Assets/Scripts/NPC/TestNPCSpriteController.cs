using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestNPCSpriteController : MonoBehaviour
{

    public Image image;



    public NPCEncounterSpriteController NPCSpriteController;

    // Update is called once per frame
    void Update()
    {
        
        // Long conditional for testing - press a letter to switch the sprite
        if(Input.GetKeyDown("a"))
        {
            NPCSpriteController.GetAngry(image);
        }
        else if(Input.GetKeyDown("h"))
        {
            NPCSpriteController.GetHappy(image);
        }
        else if(Input.GetKeyDown("w"))
        {
            NPCSpriteController.GetWorry(image);
        }
        else if(Input.GetKeyDown("s"))
        {
            NPCSpriteController.GetStress(image);
        }
        else if(Input.GetKeyDown("n"))
        {
            NPCSpriteController.GetNeutral(image);
        }
    }
}
