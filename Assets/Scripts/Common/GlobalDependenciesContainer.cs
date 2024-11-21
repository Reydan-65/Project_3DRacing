using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependenciesContainer : Dependency
{
    private static GlobalDependenciesContainer instance;

    [SerializeField] private Pauser pauser;
    [SerializeField] private CompletionTracker completionTracker;
    [SerializeField] private RaceSequenceController raceSequenceController;
    [SerializeField] private RaceController raceController;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<Pauser>(pauser, monoBehaviourInScene);
        Bind<CompletionTracker>(completionTracker, monoBehaviourInScene);
        Bind<RaceSequenceController>(raceSequenceController, monoBehaviourInScene);
        Bind<RaceController>(raceController, monoBehaviourInScene);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectsToBind();
    }
}
