using UnityEngine;

[RequireComponent(typeof (RaceCarChassis))]
public class RaceCar : MonoBehaviour
{
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxBrakeTorque;
    [SerializeField] private float maxSteerAngle;

    private RaceCarChassis chassis;

    // DEBUG
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;

    private void Start()
    {
        chassis = GetComponent<RaceCarChassis>();
    }

    private void Update()
    {
        chassis.MotorTorque = maxMotorTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
    }
}