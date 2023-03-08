/*
 * author(s): Gabriel LePoudre, William Metivier
 * 
 * The classes that define an Effect
 * 
 */

using UnityEngine;

public class Effect
{
    private int _numPlayTermination;
    public Effect(int duration)
    {
        _numPlayTermination = GameState.Meta.activeEncounter.Value.Statistics.NumberOfPlays + duration;
    }

    public int GetTerminationPlay()
    {
        return _numPlayTermination;
    }

    public int GetRemainingDuration()
    {
        return _numPlayTermination - GameState.Meta.activeEncounter.Value.Statistics.NumberOfPlays;
    }
}

public interface IExecutableEffect
{
    public int GetTerminationPlay();
    public void Execute();
    public Color GetColor();
    public string GetName();
    public string GetDescription();
}