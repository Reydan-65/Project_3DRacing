using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceSequenceController : MonoBehaviour
{
    public RaceSequence RaceSequence;

    public bool CurrentRaceIsLast()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string lastRaceSceneName = RaceSequence.RaceInfos[RaceSequence.RaceInfos.Length - 1].SceneName;

        return lastRaceSceneName == sceneName;
    }

    public RaceInfo GetCurrentLoadedRaceInfo()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < RaceSequence.RaceInfos.Length; i++)
        {
            if (RaceSequence.RaceInfos[i].SceneName == sceneName)
                return RaceSequence.RaceInfos[i];
        }

        return null;
    }

    public RaceInfo GetNextRaceInfo(RaceInfo raceInfo)
    {
        for (int i = 0; i < RaceSequence.RaceInfos.Length; i++)
        {
            if (RaceSequence.RaceInfos[i].SceneName == raceInfo.SceneName)
            {
                if (i < RaceSequence.RaceInfos.Length - 1)
                    return RaceSequence.RaceInfos[i + 1];
            }
        }

        return null;
    }
}
