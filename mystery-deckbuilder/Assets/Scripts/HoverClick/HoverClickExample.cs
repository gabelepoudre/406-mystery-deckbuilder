using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverClickExample : HoverClick
{

    public override void onMouseOver(){
        base.onMouseOver();
        Debug.Log("Mouse Hover Called");

    }

    public override void onMouseExit(){
        base.onMouseExit();
        Debug.Log("Mouse Exit Called");

    }

    public override void onClick(){
        base.onClick();
        Debug.Log("On Click Called");

    }
}