using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
{
    [SerializeField] private RaceInfo raceInfo;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image doneImage;
    [SerializeField] private Image lockerImage;

    public Image DoneImage { get { return doneImage; } }

    private SceneTransitionManager sceneTransitionManager;

    public bool IsComplete
    {
        get
        {
            return gameObject.activeSelf && doneImage.enabled;
        }
    }

    private void Start()
    {
        sceneTransitionManager = FindAnyObjectByType<SceneTransitionManager>();
        ApplyProperty(raceInfo);

        if (gameObject.transform.GetSiblingIndex() == 0) SetButtonActive();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (raceInfo == null) return;

        if (raceInfo.SceneName != null && Interactable == true)
            sceneTransitionManager.LoadScene(raceInfo.SceneName);
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;
        if (property is RaceInfo == false) return;

        raceInfo = property as RaceInfo;

        icon.sprite = raceInfo.Icon;
        title.text = raceInfo.Title;
    }

    public void SetButtonActive()
    {
        Interactable = true;
        lockerImage.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
    }

    public void SetDone()
    {
        doneImage.enabled = true;
    }
}
