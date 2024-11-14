using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SpeedEffect : MonoBehaviour, IDependency<RaceCar>
{
    [SerializeField] private new Camera camera;
    [SerializeField] private ParticleSystem speedParticle;

    [SerializeField] private float minIntensityValue;
    [SerializeField] private float maxIntensityValue;

    [Space (10)][Header ("WindSound Settings")]
    [SerializeField] private float pitchModifier;
    [SerializeField] private float volumeModifier;
    [SerializeField] private float speedModifier;

    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;

    private RaceCar raceCar;
    public void Construct(RaceCar obj) => raceCar = obj;

    private PostProcessVolume speedPostProcess;
    private ChromaticAberration speedChromaticAberration;
    private Vignette speedVignette;
    private MotionBlur speedMotionBlur;

    private AudioSource speedAudioSource;

    private bool isFast = false;

    private void Start()
    {
        speedAudioSource = GetComponent<AudioSource>();

        speedPostProcess = camera.GetComponent<PostProcessVolume>();
        speedPostProcess.profile.TryGetSettings(out speedChromaticAberration);
        speedPostProcess.profile.TryGetSettings(out speedVignette);
        speedPostProcess.profile.TryGetSettings(out speedMotionBlur);
    }

    private void Update()
    {
        if (raceCar.NormalizeLinearVelocity >= 0.5)
            isFast = true;
        else
            isFast = false;

        if (isFast == true)
        {
            if (speedParticle.isPlaying == false)
                speedParticle.Play();
            if (speedAudioSource.isPlaying == false)
            {
                speedAudioSource.Play();
                speedAudioSource.pitch = basePitch + pitchModifier * ((raceCar.LinearVelocity / raceCar.MaxSpeed) * speedModifier);
                speedAudioSource.volume = baseVolume + volumeModifier * ((raceCar.LinearVelocity / raceCar.MaxSpeed));
            }
        }
        else
        {
            speedParticle.Stop();
            speedAudioSource.Stop();
        }

        if (raceCar.NormalizeLinearVelocity >= 0.1)
        {
            float value = Mathf.Lerp(minIntensityValue, maxIntensityValue, raceCar.NormalizeLinearVelocity);

            speedChromaticAberration.intensity.value = value;
            speedVignette.intensity.value = value;

            speedMotionBlur.shutterAngle.value = Mathf.Lerp(0, 160, raceCar.NormalizeLinearVelocity);
            speedMotionBlur.sampleCount.value = (int)Mathf.Lerp(4, 20, raceCar.NormalizeLinearVelocity);
        }
        else
        {
            speedChromaticAberration.intensity.value = 0;
            speedVignette.intensity.value = 0;
            speedMotionBlur.shutterAngle.value = 0;
            speedMotionBlur.sampleCount.value = 4;
        }
    }
}
