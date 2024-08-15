using UnityEngine;

public class RaceCarInputControl : MonoBehaviour
{
    [SerializeField] private RaceCar raceCar;

    private void Update()
    {
        raceCar.ThrottleControl = Input.GetAxis("Vertical");
        raceCar.BrakeControl = Input.GetAxis("Jump");
        raceCar.SteerControl = Input.GetAxis("Horizontal");
    }
}