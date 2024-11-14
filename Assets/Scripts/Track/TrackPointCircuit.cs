using UnityEngine;
using UnityEngine.Events;

public enum TrackType
{
    Circular, Sprint
}

public class TrackPointCircuit : MonoBehaviour
{
    public event UnityAction<TrackPoint> TrackPointTriggered;
    public event UnityAction<int> LapCompleted;

    [SerializeField] private TrackType trackType;
    public TrackType TrackType => trackType;

    private TrackPoint[] trackPoints;
    private int lapsCompleted = -1;
    public int CompletedLaps => lapsCompleted;

    private void Awake()
    {
        BuildCircuit();
    }

    private void Start()
    {
        for (int i = 0; i < trackPoints.Length; i++)
        {
            trackPoints[i].Triggered += OnTrackPointTriggered;
        }

        trackPoints[0].AssignAsTarget();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < trackPoints.Length; i++)
        {
            trackPoints[i].Triggered -= OnTrackPointTriggered;
        }
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (trackPoint.IsTarget == false) return;

        trackPoint.Passed();
        trackPoint.Next?.AssignAsTarget();

        TrackPointTriggered?.Invoke(trackPoint);

        if (trackPoint.IsLast == true)
        {
            lapsCompleted++;

            if (trackType == TrackType.Sprint)
                LapCompleted?.Invoke(lapsCompleted);

            if (trackType == TrackType.Circular)
                if (lapsCompleted > 0)
                    LapCompleted?.Invoke(lapsCompleted);
        }
    }

    [ContextMenu(nameof(BuildCircuit))]
    private void BuildCircuit()
    {
        trackPoints = TrackCircuitBuilder.Build(transform, trackType);
    }
}
