using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSlotController : MonoBehaviour
{
    public class EffectController : MonoBehaviour
    {
        public GameObject[] effects;
        private bool[] space;
        private List<EffectController> effectControllers = new();

        void Start()
        {
            space = new bool[effects.Length];
            int count = 0;
            foreach (GameObject effect in effects)
            {
                effectControllers.Add(effect.GetComponent<EffectController>());
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
    }
}
