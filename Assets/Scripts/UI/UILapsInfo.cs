using TMPro;
using UnityEngine;

public class UILapsInfo : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<TrackPointCircuit>
{
    private const string SprintText = "Sprint Race";
    private const string CircularText = "Circular Race";

    [SerializeField] private TextMeshProUGUI raceTypeText;
    [SerializeField] private TextMeshProUGUI amountLapText;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private TrackPointCircuit trackPointCircuit;
    public void Construct(TrackPointCircuit obj) => trackPointCircuit = obj;

    private void Start()
    {
        raceTypeText.enabled = false;
        amountLapText.enabled = false;
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Completed += OnRaceCompleted;
    }
    
    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnPreparationStarted()
    {
        raceTypeText.enabled = true;
        amountLapText.enabled = true;
    }

    private void OnRaceCompleted()
    {
        raceTypeText.enabled = false;
        amountLapText.enabled = false;
    }

    private void Update()
    {
        int lap = trackPointCircuit.CompletedLaps + 1;

        if (lap >= raceStateTracker.LapsToComplete) lap = raceStateTracker.LapsToComplete;

        if (trackPointCircuit.TrackType == TrackType.Sprint)
        {
            raceTypeText.text = SprintText;
            amountLapText.enabled = false;
        }

        if (trackPointCircuit.TrackType == TrackType.Circular)
        {
            raceTypeText.text = CircularText;
            amountLapText.text = lap + " / " + raceStateTracker.LapsToComplete;
        }
    }
}
