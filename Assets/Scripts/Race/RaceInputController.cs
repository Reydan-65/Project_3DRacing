using UnityEngine;

public class RaceInputController : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<InputControl>
{
    private InputControl inputControl;
    public void Construct(InputControl obj) => inputControl = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceComplete;

        inputControl.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceComplete;
    }

    private void OnRaceStarted()
    {
        inputControl.enabled = true;
    }

    private void OnRaceComplete()
    {
        inputControl.Stop();
        inputControl.enabled = false;
    }
}
