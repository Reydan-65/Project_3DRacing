using UnityEngine;

[RequireComponent(typeof (RaceCarChassis))]
public class RaceCar : MonoBehaviour
{
    [SerializeField] private float maxBrakeTorque;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private AnimationCurve engineTorqueCurve;

    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSpeed;

    private RaceCarChassis chassis;

    public float LinearVelocity => chassis.LinearVelocity;

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
        float engineTorque = engineTorqueCurve.Evaluate(LinearVelocity / maxSpeed) * maxMotorTorque;

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.MotorTorque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
    }
}