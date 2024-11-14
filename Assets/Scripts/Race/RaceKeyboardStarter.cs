using UnityEngine;
using UnityEngine.Events;

public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
{
    public event UnityAction RaceLaunched;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            raceStateTracker.LaunchPreparationStarted();

            RaceLaunched?.Invoke();
        }
    }
}
