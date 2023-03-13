using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, bool> map = new Dictionary<string, bool>(5);
    public List<GameObject> tags = new List<GameObject>(5);

    

    public void Openmap()
    {


        
    }

    public void enable(string place) {
        if (map.ContainsKey(place)) {map[place] = true; }
    }

    public void disable(string place)
    {
        if (map.ContainsKey(place)) { map[place] = false; }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
