using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverClickExample : HoverClick
{

    

    public override void OnMouseOver(){
        base.OnMouseOver();
        Debug.Log("Mouse Hover Called");

    }

    public override void OnMouseExit(){
        base.OnMouseExit();
        Debug.Log("Mouse Exit Called");

    }

    public override void OnMouseDown(){
        base.OnMouseDown();
        Debug.Log("Mouse clicked");
    }

  

}