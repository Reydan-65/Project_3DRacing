using UnityEngine;
using UnityEngine.EventSystems;

public class UIExitButton : UIButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        Application.Quit();
    }
}
