using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        GameState.Meta.currentArc.OnChange += OnArcChange;
    }

    void OnArcChange() {
        if (GameState.Meta.currentArc.Value == 1)
        {
            gameObject.SetActive(false);
        }
    }


}
