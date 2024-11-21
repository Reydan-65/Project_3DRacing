using UnityEngine;
using UnityEngine.UI;

public class UISeasonButton : UISelectableButton
{
    [SerializeField] private Image commingSoonImage;
    [SerializeField] private Image lockerImage;
    [SerializeField] private Image doneImage;

    public Image LockerImage { get { return lockerImage; } }
    public Image DoneImage { get { return doneImage; } }

    public void SetButtonActive()
    {
        Interactable = true;
        lockerImage.gameObject.SetActive(false);
        commingSoonImage.gameObject.SetActive(true);
    }

    public void SetDone()
    {
        doneImage.enabled = true;
    }
}
