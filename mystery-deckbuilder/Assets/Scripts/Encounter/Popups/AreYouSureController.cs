using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreYouSureController : MonoBehaviour
{
    public Text description;
    private EncounterConfig _config;

    public void SetDescription(string descriptionText)
    {
        description.text = descriptionText;
    }

    public void SetConfig(EncounterConfig conf)
    {
        _config = conf;
    }

    public EncounterConfig GetConfig()
    {
        return _config;
    }

    public void LaunchEncounter()
    {
        Encounter.StartEncounter(_config, true);
        CloseSelf();
    }

    public void CloseSelf()
    {
        Destroy(gameObject);
    }
}
