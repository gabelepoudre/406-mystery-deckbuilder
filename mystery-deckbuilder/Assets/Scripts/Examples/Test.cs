using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public int delay;
    public bool resetGameState = false;
    private int _delayLeft;
    // Start is called before the first frame update
    void Start() {
        _delayLeft = delay;
    }

    // Update is called once per frame
    void Update() {
        if (_delayLeft > 0) {
            _delayLeft-=1;
        }
        else if (_delayLeft == 0) {
            GameStateValue<int> currentValueReference = GameState.CurrentState().value;
            Debug.Log(currentValueReference.GetValue());
            currentValueReference.ChangeValue(currentValueReference.GetValue() + 1);
            Debug.Log(currentValueReference.GetValue());
            _delayLeft -= 1;

            if (resetGameState) {
                GameState.ResetCurrentGameState();
            }
        }
    }
}
