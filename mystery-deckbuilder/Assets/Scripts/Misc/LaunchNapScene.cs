using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchNapScene : MonoBehaviour
{
    public Object sceneToLaunch = null;

    public void LaunchSetScene()
    {
        if(GameState.Player.napsRemainingToday.Value > 0)
        {
            SceneManager.LoadScene(12);
            GameState.Player.napsRemainingToday.Value -= 1;
        }
    }
}
