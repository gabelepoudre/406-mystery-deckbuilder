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
        if(Input.GetKeyDown("4"))
        {
            NPCSpriteController.GetPhaseFour(image);
        }
        else if(Input.GetKeyDown("1"))
        {
            NPCSpriteController.GetPhaseOne(image);
        }
        else if(Input.GetKeyDown("3"))
        {
            NPCSpriteController.GetPhaseThree(image);
        }
        else if(Input.GetKeyDown("2"))
        {
            NPCSpriteController.GetPhaseTwo(image);
        }
    }
}
