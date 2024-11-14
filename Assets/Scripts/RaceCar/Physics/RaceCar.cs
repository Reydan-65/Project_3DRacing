using System;
using UnityEngine;

[RequireComponent(typeof(RaceCarChassis))]
public class RaceCar : MonoBehaviour
{
    [SerializeField] private float maxBrakeTorque;
    [SerializeField] private float maxSteerAngle;

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float engineMaxTorque;
    // DEBUG
    [SerializeField] private float engineTorque;
    // DEBUG
    [SerializeField] private float engineRPM;
    [SerializeField] private float engineMinRPM;
    [SerializeField] private float engineMaxRPM;

    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;
    //DEBUG
    [SerializeField] private int selectedGearIndex;
    //DEBUG
    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear;
    [SerializeField] private float upShiftEngineRPM;
    [SerializeField] private float downShiftEngineRPM;


    [SerializeField] private float maxSpeed;
    // DEBUG
    [SerializeField] private float linearVelocity;

    [SerializeField] private float handBrakeDrag;

    private RaceCarChassis chassis;
    public Rigidbody Rigidbody => chassis == null ? GetComponent<RaceCarChassis>().Rigidbody : chassis.Rigidbody;  // если null получаем компонент : иначе "и так сойдёт"

    private float originalDrag;

    public float LinearVelocity => chassis.LinearVelocity;
    public float NormalizeLinearVelocity => chassis.LinearVelocity / maxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;
    public float OriginalDrag => originalDrag;
    public float HandBrakeDrag => handBrakeDrag;
    public int SelectedGearIndex => selectedGearIndex;
    public float SelectedGear => selectedGear;
    public float RearGear => rearGear;
    public float EngineRPM => engineRPM;
    public float EngineMaxRPM => engineMaxRPM;

    // DEBUG
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;

    private void Start()
    {
        chassis = GetComponent<RaceCarChassis>();
        originalDrag = chassis.Rigidbody.drag;
    }

    private void Update()
    {
        linearVelocity = LinearVelocity;

        UpdateEngineTorque();
        AutoGearShift();

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.MotorTorque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
    }

    // Gearbox

    private void AutoGearShift()
    {
        if (selectedGear < 0) return;
        if (engineRPM >= upShiftEngineRPM) UpGear();
        if (engineRPM < downShiftEngineRPM) DownGear();
    }

    public void UpGear()
    {
        ShiftGear(selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }

    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
    }

    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }

    public void ShiftToNeutral()
    {
        selectedGear = 0;
    }

    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;
    }

    private void UpdateEngineTorque()
    {
        engineRPM = engineMinRPM + Mathf.Abs(chassis.GetAverageRPM() * selectedGear * finalDriveRatio);
        engineRPM = Mathf.Clamp(engineRPM, engineMinRPM, engineMaxRPM);

        engineTorque = engineTorqueCurve.Evaluate(engineRPM / engineMaxRPM) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear);
    }

    public void Respawn(Vector3 position, Quaternion rotation)
    {
        Reset();

        transform.position = position;
        transform.rotation = rotation;
    }

    public void Reset()
    {
        chassis.Reset();

        chassis.MotorTorque = 0;
        chassis.BrakeTorque = 0;
        chassis.SteerAngle = 0;

        ThrottleControl = 0;
        BrakeControl = 0;
        SteerControl = 0;
    }
}