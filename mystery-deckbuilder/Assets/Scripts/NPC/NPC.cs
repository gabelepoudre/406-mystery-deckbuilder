using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //Variables 
    public string name;
    [Range(0.0f, 1.0f)]public float affinity_1;
    [Range(0.0f, 1.0f)]public float affinity_2;
    [Range(0.0f, 1.0f)]public float affintiy_3;
    [Range(0.0f, 1.0f)]public float startingPatience;
    [Range(0.0f, 1.0f)]public float startingCompliance;
    [Range(0.0f, 1.0f)]public float currentPatience;
    [Range(0.0f, 1.0f)]public float currentCompliance;
    public DialogueTree diaglogue;
    public gameObject character;



    public float getPatience()
    {
        return currentPatience;
    }
    public void changePatience(float x)
    {
        currentPatience = currentPatience - x;
    }
    public float getCompliance()
    {
        return currentCompliance;
    }
    public void setCompliance(float x)
    {
        currentCompliance = currentCompliance - x;
    }



    // Start is called before the first frame update
    void Start()
    {
        currentCompliance = startingCompliance;
        currentPatience = startingPatience;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
