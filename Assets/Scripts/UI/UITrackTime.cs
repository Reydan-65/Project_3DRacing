using TMPro;
using UnityEngine;

public class UITrackTime : MonoBehaviour,IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    [SerializeField] private TextMeshProUGUI timeText;

    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        timeText.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        timeText.enabled = true;
        enabled = true;
    }

    private void OnRaceCompleted()
    {
        timeText.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        timeText.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
    }
}
