using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickASuspectLauncher : MonoBehaviour
{

    public GameObject pickASuspectPrefab;
    // Start is called before the first frame update
    public void Launch()
    {
        Instantiate(pickASuspectPrefab);
    }
}
