using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterDisallowed : MonoBehaviour
{
    public void CloseSelf()
    {
        Destroy(gameObject);
    }
}
