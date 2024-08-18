using UnityEngine;

[RequireComponent(typeof (RaceCarChassis))]
public class RaceCar : MonoBehaviour
{
    [SerializeField] private float maxBrakeTorque;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private AnimationCurve engineTorqueCurve;

    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float linearVelocity; // DEBUG

    [SerializeField] private float handBrakeDrag;

    private RaceCarChassis chassis;
    private float originalDrag;

    public float LinearVelocity => chassis.LinearVelocity;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;
    public float OriginalDrag => originalDrag;
    public float HandBrakeDrag => handBrakeDrag;

    // DEBUG
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;

    private void Start()
    {
        chassis = GetComponent<RaceCarChassis>();
        originalDrag = chassis._Rigidbody.drag;
    }

    private void Update()
    {
        linearVelocity = LinearVelocity; // DEBUG

        float engineTorque = engineTorqueCurve.Evaluate(LinearVelocity / maxSpeed) * maxMotorTorque;

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.MotorTorque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
    }
}