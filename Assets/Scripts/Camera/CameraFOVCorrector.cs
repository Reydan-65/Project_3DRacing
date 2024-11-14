using UnityEngine;

public class CameraFOVCorrector : CameraComponent
{
    [SerializeField] private float minFieldOfView;
    [SerializeField] private float maxFieldOfView;

    private float defaultFOV;

    private void Start()
    {
        camera.fieldOfView = defaultFOV;
    }

    private void Update()
    {
        camera.fieldOfView = Mathf.Lerp(minFieldOfView, maxFieldOfView, raceCar.NormalizeLinearVelocity);
    }
}
