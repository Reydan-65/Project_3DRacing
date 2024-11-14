using UnityEngine;

public class SceneDependenciesContainer : Dependency
{
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private InputControl inputControl;
    [SerializeField] private RaceCar raceCar;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private TrackPointCircuit trackPointCircuit;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceResultTime raceResultTime;
    [SerializeField] private RaceKeyboardStarter raceKeyboardStarter;

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<RaceStateTracker>(raceStateTracker, monoBehaviourInScene);
        Bind<InputControl>(inputControl, monoBehaviourInScene);
        Bind<RaceCar>(raceCar, monoBehaviourInScene);
        Bind<CameraController>(cameraController, monoBehaviourInScene);
        Bind<TrackPointCircuit>(trackPointCircuit, monoBehaviourInScene);
        Bind<RaceTimeTracker>(raceTimeTracker, monoBehaviourInScene);
        Bind<RaceResultTime>(raceResultTime, monoBehaviourInScene);
        Bind<RaceKeyboardStarter>(raceKeyboardStarter, monoBehaviourInScene);
    }

    private void Awake()
    {
        FindAllObjectsToBind();
    }
}
