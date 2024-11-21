using UnityEngine;

public class RaceUnlockController : MonoBehaviour
{
    [SerializeField] private CompletionTracker completionTracker;

    private UIRaceButton[] uIRaceButtons;

    private void Start()
    {
        uIRaceButtons = transform.GetComponentsInChildren<UIRaceButton>();

        var drawRace = 0;
        bool raceDone = true;

        string raceName = null;

        while (raceDone == true && drawRace < uIRaceButtons.Length && 
            completionTracker.TryIndex(drawRace,out var sceneName, out raceDone))
        {
            raceName = completionTracker.RacesInfo[drawRace].SceneName;

            if (raceDone)
                uIRaceButtons[drawRace].SetDone();

            uIRaceButtons[drawRace].SetButtonActive();

            drawRace++;
        }
    }
}
