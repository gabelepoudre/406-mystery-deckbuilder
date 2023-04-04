using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMurderer : MonoBehaviour
{
    public void KillChildren()
    {
        foreach(Transform t in this.GetComponentInChildren<Transform>())
        {
            if (t != this.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
