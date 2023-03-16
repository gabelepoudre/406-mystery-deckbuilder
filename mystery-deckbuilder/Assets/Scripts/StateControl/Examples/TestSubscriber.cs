/*
 * author(s): Gabriel LePoudre
 * 
 * Example script for displaying how one might subscribe to a GameStateValue and react
 */

using UnityEngine;

public class TestSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() 
    {
        // we "subscribe" to the value with this pattern. You could also use an anonymous method here
        GameState.Meta.currentDay.OnChange += OnDayChange;
    }

    /* Whenever a change to the day is detected, we print it here */
    void OnDayChange() 
    {
        Debug.Log("Example of a Game State subscriber detected that the day changed to: " + GameState.Meta.currentDay.Value.ToString());
    }

}
