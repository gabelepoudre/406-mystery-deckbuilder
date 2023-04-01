using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTutorial : MonoBehaviour
{
    public void LeaveTutorialOnClick()
    {
        GameState.Meta.currentGameplayPhase.Value = GameState.Meta.GameplayPhases.Phase_1;
        GameState.Meta.currentAct.Value = 2;
    }
}
