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
    protected bool __forceTermination;
    public Effect(int duration)
    {
        _numPlayTermination = GameState.Meta.activeEncounter.Value.Statistics.NumberOfPlays + duration;
    }

    public bool ForceTermination()
    {
        return this.__forceTermination;
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
    public bool ForceTermination();
    public void Execute();
    public Color GetColor();
    public string GetName();
    public string GetDescription();
}