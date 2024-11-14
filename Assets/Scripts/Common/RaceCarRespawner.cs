using UnityEngine;

public class RaceCarRespawner : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceCar>, IDependency<InputControl>
{
    [SerializeField] private float respawnHeight;

    private TrackPoint respawnTrackPoint;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private RaceCar raceCar;
    public void Construct(RaceCar obj) => raceCar = obj;

    private InputControl inputControl;
    public void Construct(InputControl obj) => inputControl = obj;

    private void Start()
    {
        raceStateTracker.TrackPointPassed += OnTrackPointPassed;
    }

    private void OnDestroy()
    {
        raceStateTracker.TrackPointPassed -= OnTrackPointPassed;

    }

    private void OnTrackPointPassed(TrackPoint trackPoint)
    {
        respawnTrackPoint = trackPoint;
    }

    public void RaceCarRespawn()
    {
        if (respawnTrackPoint == null) return;

        if (raceStateTracker.RaceState != RaceState.Race) return;

        raceCar.Respawn(respawnTrackPoint.transform.position + respawnTrackPoint.transform.up * respawnHeight,
            respawnTrackPoint.transform.rotation);

        inputControl.Reset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) == true)
            RaceCarRespawn();
    }
}
