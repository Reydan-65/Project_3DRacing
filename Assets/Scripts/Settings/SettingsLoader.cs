using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    [SerializeField] private Setting[] allSettings;

    private void Start()
    {
        for (int i = 0; i < allSettings.Length; i++)
        {
            allSettings[i].Load();
            allSettings[i].Apply();
        }
    }
}
