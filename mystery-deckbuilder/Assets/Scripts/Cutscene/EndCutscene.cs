using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * Class for loading ending cutscenes using a scene object
 */
public class EndCutscene : MonoBehaviour
{
    [SerializeField] private Object goodEnding;
    [SerializeField] private Object badEnding;
    
    public void LoadGoodEnding()
    {
        SceneManager.LoadScene(goodEnding.name);
    }

    public void LoadBadEnding()
    {
        SceneManager.LoadScene(badEnding.name);
    }
}
