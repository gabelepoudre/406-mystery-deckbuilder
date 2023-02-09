using UnityEngine;

public class HoverClickExample : HoverClick
{

    public override void OnMouseEnter()
    {
        base.OnMouseEnter();
        Debug.Log("Mouse Enter Called");
    }

    public override void OnMouseOver()
    {
        base.OnMouseOver();
        Debug.Log("Mouse Hover Called");

    }

    public override void OnMouseExit()
    {
        base.OnMouseExit();
        Debug.Log("Mouse Exit Called");

    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        Debug.Log("Mouse clicked");
    }

  

}