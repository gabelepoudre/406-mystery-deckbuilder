/*
 * author(s): Gabriel LePoudre
 * 
 * This script holds the example for an option button. It keeps a reference \
 *  to the script that created it (will be a prefab) and the optionID that it is correlated to
 * WARNING: Not developed for production use, just an example
 */

using UnityEngine;

public class ExampleDialogueOptionButton : MonoBehaviour
{

    public int optionID;
    public ExampleDialogueController controllerScript;

    /* 
     * In a somewhat silly choice, this is the method that is set as the example buttons OnClick.
     * Both public fields must be filled for this to work properly
     */
    public void ThisOptionPicked()
    {
        controllerScript.OptionSelection(optionID);
    }
}
