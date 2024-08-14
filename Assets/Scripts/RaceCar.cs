using UnityEngine;

public class RaceCar : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private Transform[] wheelMeshs;
    [SerializeField] private float motorTorque;
    [SerializeField] private float brakeTorque;
    [SerializeField] private float steerAngle;

    private void Update()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelColliders[i].brakeTorque = Input.GetAxis("Jump") * brakeTorque;

            Vector3 wheelPosition;
            Quaternion wheelRotation;

            wheelColliders[i].GetWorldPose(out wheelPosition, out wheelRotation);

            wheelMeshs[i].position = wheelPosition;
            wheelMeshs[i].rotation = wheelRotation;
        }

        wheelColliders[0].steerAngle = Input.GetAxis("Horizontal") * steerAngle;
        wheelColliders[1].steerAngle = Input.GetAxis("Horizontal") * steerAngle;

    }
}