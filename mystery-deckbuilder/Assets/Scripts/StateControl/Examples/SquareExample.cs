using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareExample : MonoBehaviour
{

    public void Changed()
    {
        Debug.Log("Change");
    }

    void Awake()
    {
        GameState.Meta.currentGameplayPhase.Value = GameState.Meta.GameplayPhases.Phase_1;
        GameState.Meta.currentGameplayPhase.OnChange += Changed;
    }

    // Start is called before the first frame update
    void Start()
    {

        GameState.Meta.currentGameplayPhase.Value = GameState.Meta.GameplayPhases.Tutorial;

        if (GameState.Meta.currentGameplayPhase.Value == GameState.Meta.GameplayPhases.Tutorial)
        {
            Debug.Log("Tutorial");
        }
        else
        {
            Debug.Log("Normal");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
