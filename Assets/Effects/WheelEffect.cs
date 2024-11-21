using UnityEngine;

public class WheelEffect : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private ParticleSystem[] wheelsSmoke;

    [SerializeField] private float forwardSlipLimit;
    [SerializeField] private float sidewaySlipLimit;

    [SerializeField] private GameObject skidPrefab;

    [SerializeField] private AudioSource tireSound;

    private WheelHit wheelHit;
    private Transform[] skidTrails;

    private void Start()
    {
        skidTrails = new Transform[wheels.Length];
    }

    private void Update()
    {
        bool isSlip = false;

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetGroundHit(out wheelHit);

            if (wheels[i].isGrounded == true)
            {
                if (wheelHit.forwardSlip > forwardSlipLimit || wheelHit.sidewaysSlip > sidewaySlipLimit)
                {
                    if (skidTrails[i] == null)
                        skidTrails[i] = Instantiate(skidPrefab).transform;

                    if (tireSound.isPlaying == false)
                        tireSound.Play();

                    if (skidTrails[i] != null)
                    {
                        Vector3 pointPos = wheels[i].transform.position - wheelHit.normal * wheels[i].radius; /*new Vector3(wheelHit.point.x, wheelHit.point.y + 0.01f, wheelHit.point.z);*/
                        
                        skidTrails[i].position = pointPos;
                        skidTrails[i].forward = -wheelHit.normal;

                        wheelsSmoke[i].transform.position = skidTrails[i].position;
                        wheelsSmoke[i].Emit(1);
                    }

                    isSlip = true;
                    continue;
                }
            }

            skidTrails[i] = null;
            wheelsSmoke[i].Stop();
        }

        if (isSlip == false)
            tireSound.Stop();
    }
}
