using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RaceCarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;
    [SerializeField] private float wheelBaseLength;
    [SerializeField] private Transform centerOfMass;

    [Header("AngularDrag")]
    [SerializeField] private float angularDragMin;
    [SerializeField] private float angularDragMax;
    [SerializeField] private float angularDragFactor;

    [Header("DownForce")]
    [SerializeField] private float downForceMin;
    [SerializeField] private float downForceMax;
    [SerializeField] private float downForceFactor;

    private new Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>() : rigidbody; // ���� null �������� ��������� : ����� "� ��� �����"

    // DEBUG
    public float MotorTorque;
    public float BrakeTorque;
    public float SteerAngle;

    public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;

    // Unity API
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (centerOfMass != null)
        {
            rigidbody.centerOfMass = centerOfMass.localPosition;
        }

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].ConfigureVehicleSubsteps(50, 50, 50);
        }
    }

    private void FixedUpdate()
    {
        UpdateAngularDrag();
        UpdateDownForce();

        UpdateWheelAxle();
    }

    // Public

    public float GetAverageRPM()
    {
        float sum = 0;

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            sum += wheelAxles[i].GetAverageRPM();
        }

        return sum / wheelAxles.Length;
    }

    public float GetWheelSpeed()
    {
        return GetAverageRPM() * wheelAxles[0].GetRadius() * 2 * 0.1885f; // 2pr = 2 * 0.1885
    }

    public bool RearWheelIsGrounded()
    {
        if (wheelAxles[1].WheelIsGrounded())
        {
            return true;
        }

        return false;
    }

    // Private

    private void UpdateAngularDrag()
    {
        rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
    }

    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
        rigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateWheelAxle()
    {
        int amountMotorWheel = 0;

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            if (wheelAxles[i].IsMotor == true)
            {
                amountMotorWheel += 2;
            }
        }

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();

            wheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheel);
            wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);
            wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
        }
    }

    public void Reset()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}