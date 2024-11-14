using TMPro;
using UnityEngine;

public class GearIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gearText;

    private RaceCar raceCar;
    
    private void Start()
    {
        raceCar = GetComponent<RaceCar>();
    }

    void Update()
    {
        if (raceCar.SelectedGear == raceCar.RearGear)
            gearText.text = "R";
        else
            gearText.text = (raceCar.SelectedGearIndex + 1).ToString();
    }
}
