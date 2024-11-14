using UnityEngine;

public class CameraShaker : CameraComponent
{
    [SerializeField] private WheelCollider[] wheels;

    private float shakeAmount;

    private void Update()
    {
        if (raceCar.NormalizeLinearVelocity >= 0 && raceCar.NormalizeLinearVelocity < 0.5) shakeAmount = 0;
        if (raceCar.NormalizeLinearVelocity >= 0.5 && raceCar.NormalizeLinearVelocity < 0.85) shakeAmount = 0.5f;
        if (raceCar.NormalizeLinearVelocity >= 0.85 && raceCar.NormalizeLinearVelocity <= 1) shakeAmount = 1;

        for (int i = 0; i < wheels.Length; i++)
            if (wheels[i].isGrounded == true) transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
    }
}
