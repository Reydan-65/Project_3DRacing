using TMPro;
using UnityEngine;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private TextMeshProUGUI timerText;
    
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Started += OnRaceStarted;

        timerText.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnPreparationStarted()
    {
        timerText.enabled = true;
        enabled = true;
    }

    private void OnRaceStarted()
    {
        timerText.enabled = false;
        enabled = false;
    }
    
    private void Update()
    {
        timerText.text = raceStateTracker.CountDownTimer.Value.ToString("F0");

        if (timerText.text == "0")
            timerText.text = "GO!";
    }
}
