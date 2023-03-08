using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSlotController : MonoBehaviour
{
    public GameObject[] effects;
    private bool[] space;
    private List<EffectController> effectControllers = new();

    void Awake()
    {
        space = new bool[effects.Length];
        int count = 0;
        foreach (GameObject effect in effects)
        {
            effectControllers.Add(effect.GetComponent<EffectController>());
            if(effect.activeSelf)
            {
                effect.SetActive(false);
            }
            space[count] = false;
            count++;
        }
    }

    private int TryGetEmptyIndex()
    {
        for(int i = 0; i <= space.Length-1; i++)
        {
            if (!space[i])
            {
                return i;
            }
        }
        return -1;
    }

    public void DisplayEffect(IExecutableEffect effectBackend)
    {
        int id = TryGetEmptyIndex();
        if (id == -1)
        {
            Debug.LogError("More than limit of effects visually applied");
            return;
        } 
        else
        {
            effectControllers[id].Display(effectBackend);
            space[id] = true;
        }
    }

    public void Clear()
    {
        int count = 0;
        foreach (GameObject effect in effects)
        {
            if (effect.activeSelf)
            {
                effect.SetActive(false);
            }
            space[count] = false;
            count++;
        }
    }
}
