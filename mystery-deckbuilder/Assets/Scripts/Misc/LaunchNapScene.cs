using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchNapScene : MonoBehaviour
{
    public Object sceneToLaunch = null;

    public void LaunchSetScene()
    {
        if(sceneToLaunch != null && GameState.Player.napsRemainingToday.Value > 0)
        {
            SceneManager.LoadScene(sceneToLaunch.name);
            GameState.Player.napsRemainingToday.Value -= 1;
        }
    }
}
