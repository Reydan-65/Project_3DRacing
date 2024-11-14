using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SwitchGearSound : MonoBehaviour, IDependency<RaceCar>
{
    private RaceCar raceCar;
    public void Construct(RaceCar obj) => raceCar = obj;

    private AudioSource gearAudioSource;
    private float currentGear;

    private void Start()
    {
        gearAudioSource = GetComponent<AudioSource>();
        currentGear = raceCar.SelectedGear;
    }

    private void Update()
    {
        if (gearAudioSource.isPlaying == false)
        {
            if (raceCar.SelectedGear != currentGear)
            {
                gearAudioSource.Play();
                currentGear = raceCar.SelectedGear;
            }
        }
    }
}
