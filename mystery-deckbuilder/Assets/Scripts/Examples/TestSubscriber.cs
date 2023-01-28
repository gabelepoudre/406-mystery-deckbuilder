using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubscriber : MonoBehaviour
{
    private ValueChangeSubscriber _valueChangeSubscriber = new(this);
    private class ValueChangeSubscriber : GameStateValueSubscriber<int>
    {
        public TestSubscriber _referenceToOwner;
        public ValueChangeSubscriber(TestSubscriber referenceToOwner) : base(GameState.CurrentState().value) { _referenceToOwner = referenceToOwner; }
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
