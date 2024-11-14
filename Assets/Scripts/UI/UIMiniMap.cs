using UnityEngine;

public class UIMiniMap : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject miniMapObject;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        miniMapObject.SetActive(false);
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.PreparationStarted -= OnRaceCompleted;
    }

    private void OnPreparationStarted()
    {
        miniMapObject.SetActive(true);
    }

    private void OnRaceCompleted()
    {
        miniMapObject.SetActive(false);
    }
}
