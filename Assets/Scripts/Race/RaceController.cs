using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour, IDependency<RaceResultTime>, IDependency<CompletionTracker>
{
    private const string MainMenu = "main_menu";

    [SerializeField] private RaceSequenceController raceSequenceController;

    private CompletionTracker completionTracker;
    public void Construct(CompletionTracker obj) => completionTracker = obj;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;

    private RaceInfo currentRaceInfo;
    public RaceInfo CurrentRaceInfo => currentRaceInfo;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != MainMenu)
            currentRaceInfo = raceSequenceController.GetCurrentLoadedRaceInfo();

        if (SceneManager.GetActiveScene().name != MainMenu)
            raceResultTime.ResultsUpdated += OnResultsUpdated;
    }

    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().name != MainMenu)
            raceResultTime.ResultsUpdated -= OnResultsUpdated;
    }

    private void OnResultsUpdated()
    {
        CheckRaceCondition();
    }

    private void CheckRaceCondition()
    {
        if (raceResultTime.GetAbsoluteRecord() < raceResultTime.GoldTime)
            Pass();
    }

    private void Pass()
    {
        completionTracker.SaveRaceResult(true);
    }
}
