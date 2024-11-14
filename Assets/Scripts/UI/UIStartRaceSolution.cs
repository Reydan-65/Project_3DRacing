using UnityEngine;

public class UIStartRaceSolution : MonoBehaviour, IDependency<RaceKeyboardStarter>
{
    [SerializeField] private GameObject startSolutionText;

    private RaceKeyboardStarter raceKeyboardStarter;
    public void Construct(RaceKeyboardStarter obj) => raceKeyboardStarter = obj;

    private void Start()
    {
        startSolutionText.SetActive(true);

        raceKeyboardStarter.RaceLaunched += OnRaceLaunched;
    }

    private void OnDestroy()
    {
        raceKeyboardStarter.RaceLaunched -= OnRaceLaunched;
    }

    private void OnRaceLaunched()
    {
        startSolutionText.SetActive(false);
    }
}
