using System;
using UnityEngine;

public class InputControl : MonoBehaviour, IDependency<RaceCar>
{
    [SerializeField] private RaceCarChassis chassis;
    [SerializeField] private AnimationCurve brakeCurve;
    [SerializeField] private AnimationCurve steerCurve;

    [SerializeField][Range(0.0f, 1.0f)] private float autoBrakeStrength = 0.5f;

    private RaceCar raceCar;
    public void Construct(RaceCar obj) => raceCar = obj;

    private new Rigidbody rigidbody;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handBrakeAxis;

    private void Start()
    {
        rigidbody = chassis.Rigidbody;
    }

    private void Update()
    {
        wheelSpeed = raceCar.WheelSpeed;

        UpdateAxis();
        UpdateThrottleAndBrake();
        UpdateSteer();
        UpdateHandBrake();
        UpdateAutoBrake();

        // Debug
        // Переключение скорости
        if(Input.GetKeyDown(KeyCode.E)) raceCar.UpGear();
        if(Input.GetKeyDown(KeyCode.Q)) raceCar.DownGear();
    }

    private void UpdateThrottleAndBrake()
    {
        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            raceCar.ThrottleControl = Mathf.Abs(verticalAxis);
            raceCar.BrakeControl = 0;
        }
        else
        {
            raceCar.ThrottleControl = 0;
            raceCar.BrakeControl = brakeCurve.Evaluate(wheelSpeed / raceCar.MaxSpeed);
        }

        // Gears
        if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f) raceCar.ShiftToReverseGear();
        if (verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed < 0.5f) raceCar.ShiftToFirstGear();
    }

    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handBrakeAxis = Input.GetAxis("Jump");
    }

    private void UpdateSteer()
    {
        raceCar.SteerControl = steerCurve.Evaluate(raceCar.WheelSpeed / raceCar.MaxSpeed) * horizontalAxis;
    }

    private void UpdateHandBrake()
    {
        if (handBrakeAxis == 1)
        {
            if (chassis.RearWheelIsGrounded())
            {
                rigidbody.drag = raceCar.HandBrakeDrag;
            }
        }
        else
        {
            rigidbody.drag = raceCar.OriginalDrag;
        }
    }

    private void UpdateAutoBrake()
    {
        if (verticalAxis == 0)
        {
            raceCar.BrakeControl = brakeCurve.Evaluate(wheelSpeed / raceCar.MaxSpeed) * autoBrakeStrength;
        }
    }

    public void Reset()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handBrakeAxis = 0;

        raceCar.ThrottleControl = 0;
        raceCar.SteerControl = 0;
        raceCar.BrakeControl = 0;
    }

    public void Stop()
    {
        Reset();

        raceCar.BrakeControl = 1;
    }
}