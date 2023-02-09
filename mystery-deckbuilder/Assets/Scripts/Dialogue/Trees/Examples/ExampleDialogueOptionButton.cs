using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDialogueOptionButton : MonoBehaviour
{

    public int optionID;
    public ExampleDialogueController controllerScript;

    public void ThisOptionPicked()
    {
        controllerScript.OptionSelection(optionID);
    }
}
