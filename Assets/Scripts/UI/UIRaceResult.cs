using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceResult : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceTimeTracker>
{
    private const string GoldName = "Gold";
    private const string PlayerName = "Player";
    private const string CurrentName = "Current";

    [SerializeField] private GameObject recordList;

    [SerializeField] private TextMeshProUGUI recordNameText;
    [SerializeField] private TextMeshProUGUI recordTimeText;

    [SerializeField] private TextMeshProUGUI currentNameText;
    [SerializeField] private TextMeshProUGUI currentTimeText;

    [SerializeField] private Image doneImage;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;

    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

    private void Start()
    {
        doneImage.gameObject.SetActive(false);
        raceResultTime.ResultsUpdated += OnResultUpdated;
        recordList.SetActive(false);
    }

    private void OnDestroy()
    {
        raceResultTime.ResultsUpdated -= OnResultUpdated;
    }

    private void OnResultUpdated()
    {
        recordList.SetActive(true);

        float currentTime = raceTimeTracker.CurrentTime;
        float goldTime = raceResultTime.GoldTime;
        float playerRecordTime = raceResultTime.PlayerRecordTime;

        if (currentTime > goldTime)
        {
            recordTimeText.color = Color.yellow;
            currentTimeText.color = Color.red;

            if (playerRecordTime > goldTime)
            {
                recordNameText.text = GoldName + ":";
                recordTimeText.text = StringTime.SecondToTimeString(goldTime);
            }
            else
            {
                recordNameText.text = PlayerName + ":";
                recordTimeText.text = StringTime.SecondToTimeString(playerRecordTime);
            }
        }
        else
        {
            recordNameText.text = PlayerName + ":";

            if (currentTime <= playerRecordTime)
            {
                recordTimeText.text = StringTime.SecondToTimeString(currentTime);
                recordTimeText.color = Color.green;
                currentTimeText.color = Color.green;
            }
            else
            {
                recordTimeText.text = StringTime.SecondToTimeString(playerRecordTime);
                recordTimeText.color = Color.yellow;
                currentTimeText.color = Color.red;
            }
        }

        if (raceResultTime.GetAbsoluteRecord() < raceResultTime.GoldTime)
            doneImage.gameObject.SetActive(true);

        currentNameText.text = CurrentName + ":";
        currentTimeText.text = StringTime.SecondToTimeString(currentTime);
    }
}
