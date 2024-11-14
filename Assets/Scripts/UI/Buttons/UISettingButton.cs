using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingButton : UISelectableButton, IScriptableObjectProperty
{
    [SerializeField] private Setting setting;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Image previousImage;
    [SerializeField] private Image nextImage;

    private void Start()
    {
        ApplyProperty(setting);
    }

    public void SetNextValueSetting()
    {
        setting?.SetNextValue();
        setting?.Apply();

        UpdateInfo();
    }

    public void SetPreviousValueSetting()
    {
        setting?.SetPreviousValue();
        setting?.Apply();

        UpdateInfo();
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;
        if (property is Setting == false) return;

        setting = property as Setting;

        UpdateInfo();
    }

    private void UpdateInfo()
    {
        titleText.text = setting.Title;
        valueText.text = setting.GetStringValue();

        previousImage.enabled = !setting.isMinValue;
        nextImage.enabled = !setting.isMaxValue;
    }
}
