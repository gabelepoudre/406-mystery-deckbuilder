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
}

public interface IExecutableEffect
{
    public int GetTerminationPlay();
    public void Execute(Encounter enc);
}