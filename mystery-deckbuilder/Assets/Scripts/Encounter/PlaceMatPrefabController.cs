/*
 * author(s): Gabriel LePoudre, William Metivier
 * 
 * For controlling the placemat (where the cards are)
 * Assumes that all child GameObjects of the placemat are spawnpoints
 */


using System.Collections.Generic;
using UnityEngine;

public class PlaceMatPrefabController : MonoBehaviour
{
    private List<(bool, Transform)> _cardLocations = new List<(bool, Transform)>();
    void Start()
    {
        foreach(Transform t in this.GetComponentsInChildren<Transform>())
        {
            if (t != gameObject.transform)  // we have to check if it is our Transform, for some reason
            {
                _cardLocations.Add((false, t));
            }
        }
    }

    /* Returns True if a card occupies all locations */
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

    /* Debug function */
    private void DebugLogTheLocations()
    {
        Debug.Log("------------****-------------");
        foreach ((bool, Transform) loc in _cardLocations)
        {
            Debug.Log(loc.Item1.ToString() + "|" + loc.Item2.ToString());
        }
    }

    /*
     * Gives you the first empty index
     * Somewhat dubiously also "Occupies" the index, even though theoretically a card may not be placed
     *  it always is in practice, though...?
     */
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

    /* Given an int position, clears it (does not handle destuction of the card there) */
    public void ClearPosition(int position)
    {
        _cardLocations[position] = (false, _cardLocations[position].Item2);
    }

    /* Given a position index, get the correlated spawn point transform */
    public Transform GetTransformFromIndex(int index)
    {
        return _cardLocations[index].Item2;
    }

}
