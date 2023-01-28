using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubscriber : MonoBehaviour
{
    private readonly ValueChangeSubscriber _valueChangeSubscriber = new();
    private class ValueChangeSubscriber : GameStateValueSubscriber<int>
    {
        public TestSubscriber _referenceToOwner;
        public ValueChangeSubscriber() : base(GameState.CurrentState().value) { }
        public override void OnValueChange(int newValue)
        {
            _referenceToOwner.TheLog();
        }
    }
    // Start is called before the first frame update
    void Start() {
        _valueChangeSubscriber._referenceToOwner = this;
    }

    void TheLog() {
        Debug.Log("I detected some change!");
    }


}
