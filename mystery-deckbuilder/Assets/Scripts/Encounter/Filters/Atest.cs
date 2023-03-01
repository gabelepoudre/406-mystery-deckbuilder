using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABehaviour
{
    public virtual void DoStuff()
    {
        Debug.Log("Did stuff base");
    }
} 

public class B1 : ABehaviour
{

}

public class B2 : ABehaviour
{
    public override void DoStuff()
    {
        Debug.Log("Did not base stuff");
    }
}

public class Atest : MonoBehaviour
{
    private ABehaviour _b; 

    // Start is called before the first frame update
    void Start()
    {
        _b = new B2();
        _b.DoStuff();
    }
}
