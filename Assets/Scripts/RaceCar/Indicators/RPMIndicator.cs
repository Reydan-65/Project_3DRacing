using UnityEngine;
using UnityEngine.UI;

public class RPMIndicator : MonoBehaviour
{
    [SerializeField] private Image rpmImage;
    
    private RaceCar raceCar;

    private void Start()
    {
        raceCar = GetComponent<RaceCar>();
        rpmImage.fillAmount = 0;
    }

    void Update()
    {
        if (raceCar.EngineRPM > 0)
        {
            float fillAmount = Mathf.Clamp01(raceCar.EngineRPM / raceCar.EngineMaxRPM);
            rpmImage.fillAmount = fillAmount;
        }
        else
            rpmImage.fillAmount = 0;
    }
}
