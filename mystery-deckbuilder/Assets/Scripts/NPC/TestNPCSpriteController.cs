using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPCSpriteController : MonoBehaviour
{


    public NPCSpriteController NPCSpriteController;

    // Update is called once per frame
    void Update()
    {
        
        // Long conditional for testing - press a letter to switch the sprite
        if(Input.GetKeyDown("a"))
        {
            NPCSpriteController.GetAngry();
        }
        else if(Input.GetKeyDown("h"))
        {
            NPCSpriteController.GetHappy();
        }
        else if(Input.GetKeyDown("w"))
        {
            NPCSpriteController.GetWorry();
        }
        else if(Input.GetKeyDown("s"))
        {
            NPCSpriteController.GetStress();
        }
        else if(Input.GetKeyDown("n"))
        {
            NPCSpriteController.GetNeutral();
        }
    }
}
