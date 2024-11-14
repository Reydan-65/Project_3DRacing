using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceCar>
{
    [SerializeField] private new Camera camera;
    [SerializeField] private CameraFollow cameraFollower;
    [SerializeField] private CameraShaker cameraShaker;
    [SerializeField] private CameraFOVCorrector cameraFOVCorrector;
    [SerializeField] private CameraPathFollower cameraPathFollower;

    private RaceCar raceCar;
    public void Construct(RaceCar obj) => raceCar = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private float switchCameraDelay = 1.0f;

    private void Awake()
    {
        cameraFollower.SetProperties(raceCar, camera);
        cameraShaker.SetProperties(raceCar, camera);
        cameraFOVCorrector.SetProperties(raceCar, camera);
    }

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Completed += OnCompleted;

        cameraFollower.enabled = false;
        cameraPathFollower.enabled = true;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Completed -= OnCompleted;
    }

    private void OnPreparationStarted()
    {
        cameraFollower.enabled = true;
        cameraPathFollower.enabled = false;
    }

    private void OnCompleted()
    {
        StartCoroutine(PrepairCamera());
    }

    IEnumerator PrepairCamera()
    {
        yield return new WaitForSeconds(switchCameraDelay);

        cameraPathFollower.enabled = true;
        cameraPathFollower.StartMoveToNearestPoint();
        cameraPathFollower.SetLookTarget(raceCar.transform);

        cameraFollower.enabled = false;
    }
}
