using UnityEngine;

public class CameraPathFollower : CameraComponent
{
    [SerializeField] private Transform path;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private float movementSpeed;

    private Vector3[] pathPoints;
    private int pathPointIndex;

    private void Start()
    {
        pathPoints = new Vector3[path.childCount];

        SetPathPosition();
    }

    private void Update()
    {
        SetPathPosition();

        transform.position = Vector3.MoveTowards(transform.position, pathPoints[pathPointIndex], movementSpeed * Time.deltaTime);

        if (transform.position == pathPoints[pathPointIndex])
        {
            if (pathPointIndex == pathPoints.Length - 1)
            {
                pathPointIndex = 0;
            }
            else
                pathPointIndex++;
        }

        transform.LookAt(lookTarget);
    }

    private void SetPathPosition()
    {
        for (int i = 0; i < path.childCount; i++)
        {
            pathPoints[i] = path.GetChild(i).position;
        }
    }

    public void StartMoveToNearestPoint()
    {
        float minDistance = float.MaxValue;

        for (int i = 0; i < path.childCount; i++)
        {
            pathPoints[i] = path.GetChild(i).position;

            float distance = Vector3.Distance(transform.position, pathPoints[i]);

            if (distance < minDistance)
            {
                minDistance = distance;
                pathPointIndex = i;
            }
        }
    }

    public void SetLookTarget(Transform target)
    {
        lookTarget = target;
    }
}
