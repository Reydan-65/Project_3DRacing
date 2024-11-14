using TMPro;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;

    private RaceCarChassis raceCarChassis;
    private int raceCarSpeed;

    private void Start()
    {
        raceCarChassis = GetComponent<RaceCarChassis>();
    }

    void Update()
    {
        raceCarSpeed = (int)raceCarChassis.LinearVelocity;
        speedText.text = raceCarSpeed.ToString();
    }
}
