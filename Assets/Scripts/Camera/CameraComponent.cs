using UnityEngine;

[RequireComponent (typeof(CameraController))]
public abstract class CameraComponent : MonoBehaviour
{
    protected RaceCar raceCar;
    protected new Camera camera;

    public virtual void SetProperties(RaceCar raceCar, Camera camera)
    {
        this.raceCar = raceCar;
        this.camera = camera;
    }
}
