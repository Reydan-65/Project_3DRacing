using UnityEngine;

public class LimitMiniMapCamera : MonoBehaviour
{
    [SerializeField] private RaceCar raceCar;

    private void LateUpdate()
    {
        transform.position = new Vector3(raceCar.transform.position.x, 500, raceCar.transform.position.z);
        transform.rotation = Quaternion.Euler(90,0,0);
    }
}