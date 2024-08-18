using System;
using UnityEngine;

public class RaceCarInputControl : MonoBehaviour
{
    [SerializeField] private RaceCar raceCar;
    [SerializeField] private RaceCarChassis chassis;
    [SerializeField] private AnimationCurve brakeCurve;
    [SerializeField] private AnimationCurve steerCurve;

    [SerializeField][Range(0.0f, 1.0f)] private float autoBrakeStrength = 0.5f;

    private new Rigidbody rigidbody;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handBrakeAxis;

    private void Start()
    {
        rigidbody = chassis._Rigidbody;
    }

    private void Update()
    {
        wheelSpeed = raceCar.WheelSpeed;

        UpdateAxis();
        UpdateThrottleAndBrake();
        UpdateHandBrake();
        UpdateAutoBrake();
        UpdateSteer();
    }

    private void UpdateThrottleAndBrake()
    {
        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            raceCar.ThrottleControl = verticalAxis;
            raceCar.BrakeControl = 0;
        }
        else
        {
            raceCar.ThrottleControl = 0;
            raceCar.BrakeControl = brakeCurve.Evaluate(wheelSpeed / raceCar.MaxSpeed);
        }
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
}