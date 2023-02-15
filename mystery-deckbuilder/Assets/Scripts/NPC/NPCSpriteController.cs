using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpriteController : MonoBehaviour
{
    
    // Sprite Variables
    public SpriteRenderer spriteRenderer;
    public Sprite neutral;
    public Sprite happy;
    public Sprite angry;
    public Sprite stress;
    public Sprite worry;


    // Switch to Neutral Sprite
    public void GetNeutral(){
        spriteRenderer.sprite = neutral;
        
    }


    // Switch to Happy Sprite
    public void GetHappy(){
        spriteRenderer.sprite = happy;
        
    }


    // Switch to Angry Sprite
    public void GetAngry(){
        spriteRenderer.sprite = angry;
        
    }


    // Switch to Stress Sprite
    public void GetStress(){
        spriteRenderer.sprite = stress;
        
    }


    // Switch to Worry Sprite
    public void GetWorry(){
        spriteRenderer.sprite = worry;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

        // Ugly conditionals just for testing!!
        if(Input.GetKeyDown("a"))
        {
            GetAngry();
        }
        else if(Input.GetKeyDown("h"))
        {
            GetHappy();
        }
        else if(Input.GetKeyDown("w"))
        {
            GetWorry();
        }
        else if(Input.GetKeyDown("s"))
        {
            GetStress();
        }
        else if(Input.GetKeyDown("n"))
        {
            GetNeutral();
        }
        
    }
}
