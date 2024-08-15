using UnityEngine;

public class RaceCarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;

    // DEBUG
    public float MotorTorque;
    public float BrakeTorque;
    public float SteerAngle;

    private void FixedUpdate()
    {
        UpdateWheelAxle();
    }

    private void UpdateWheelAxle()
    {
        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();

            wheelAxles[i].ApplyMotorTorque(MotorTorque);
            wheelAxles[i].ApplySteerAngle(SteerAngle);
            wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
        }
    }
}