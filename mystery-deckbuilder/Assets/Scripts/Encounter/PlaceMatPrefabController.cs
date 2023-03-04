using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMatPrefabController : MonoBehaviour
{
    private List<(bool, Transform)> _cardLocations = new List<(bool, Transform)>();
    void Start()
    {
        foreach(Transform t in this.GetComponentsInChildren<Transform>())
        {
            if (t != gameObject.transform)
            {
                _cardLocations.Add((false, t));
            }
        }
    }

    public bool IsFull()
    {
        foreach((bool, Transform) loc in _cardLocations)
        {
            if (!loc.Item1)
            {
                return false;
            }
        }
        return true;
    }

    private void DebugLogTheLocations()
    {
        Debug.Log("------------****-------------");
        foreach ((bool, Transform) loc in _cardLocations)
        {
            Debug.Log(loc.Item1.ToString() + "|" + loc.Item2.ToString());
        }
    }

    public int GetEmptyTransformIndex()
    {       
        if (IsFull())
        {
            Debug.LogWarning("Tried to get empty card spot when mat is full");
            return 5;
        }

        for (int index = 0; index <= _cardLocations.Count - 1; index++)
        {
            (bool, Transform) location = _cardLocations[index];
            if (!location.Item1)
            {
                _cardLocations[index] = (true, location.Item2);
                return index;
            }
        }
        
        Debug.LogError("GetEmptyTransform() couldn't find a card location despite not being full");
        return 5; // this should never happen because of the first if in this method
    }

    public void ClearPosition(int position)
    {
        _cardLocations[position] = (false, _cardLocations[position].Item2);
    }

    public Transform GetTransformFromIndex(int index)
    {
        return _cardLocations[index].Item2;
    }

    //TODO, remove cards
}
