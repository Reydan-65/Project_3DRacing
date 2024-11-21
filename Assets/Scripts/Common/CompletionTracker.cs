using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletionTracker : MonoBehaviour
{
    public const string filename = "completion.dat";

    [Serializable]
    public class Data
    {
        public string SceneName;
        public bool IsCompleted = false;
    }

    private List<Data> data = new List<Data>();

    [SerializeField]private RaceInfo[] racesInfo;
    public RaceInfo[] RacesInfo => racesInfo;

    private void Awake()
    {
        Saver<List<Data>>.TryLoad(filename, ref data);

        while (data.Count < racesInfo.Length)
        {
            data.Add(new Data());
        }

        for (int i = 0; i < racesInfo.Length; i++)
        {
            data[i].SceneName = racesInfo[i].SceneName;
        }
    }

    public bool TryIndex(int id, out string sceneName, out bool isCompleted)
    {
        if (id >= 0 && id < data.Count)
        {
            sceneName = data[id].SceneName;
            isCompleted = data[id].IsCompleted;

            return true;
        }

        sceneName = null;
        isCompleted = false;

        return false;
    }

    public void SaveRaceResult(bool isCompleted)
    {
        SaveResult(SceneManager.GetActiveScene().name, isCompleted);
    }

    private void SaveResult(string sceneName, bool isCompleted)
    {
        for(int i = 0;i < data.Count;i++)
        {
            if (data[i].SceneName == sceneName)
            {
                if (isCompleted)
                {
                    data[i].IsCompleted = isCompleted;

                    Saver<List<Data>>.Save(filename, data);
                }

                return;
            }
        }
    }
}
