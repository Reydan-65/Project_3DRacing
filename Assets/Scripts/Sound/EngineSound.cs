using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour, IDependency<RaceCar>
{
    [SerializeField] private float pitchModifier;
    [SerializeField] private float volumeModifier;
    [SerializeField] private float rpmModifier;

    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;

    private RaceCar raceCar;
    public void Construct(RaceCar obj) => raceCar = obj;

    private AudioSource engineAudioSource;

    private void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        engineAudioSource.pitch = basePitch + pitchModifier * ((raceCar.EngineRPM / raceCar.EngineMaxRPM) * rpmModifier);
        engineAudioSource.volume = baseVolume + volumeModifier * (raceCar.EngineRPM / raceCar.EngineMaxRPM);
    }
}
