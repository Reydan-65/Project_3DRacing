using UnityEngine;

public class UISelectableButtonContainer : MonoBehaviour
{
    [SerializeField] private Transform buttonsContainer;

    public bool Interactable = true;
    public void SetInteractable(bool interactable) => Interactable = interactable;

    private UISelectableButton[] buttons;

    private int selectButtonIndex = 0;

    private void Start()
    {
        buttons = buttonsContainer.GetComponentsInChildren<UISelectableButton>();

        if (buttons == null)
            Debug.LogError("Button list is empty!");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter += OnPointerEnter;
        }

        if (Interactable == false) return;

        buttons[selectButtonIndex].SetFocus();
    }

    private void OnDestroy()
    {
        if (buttons == null) return;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter -= OnPointerEnter;
        }
    }

    private void OnPointerEnter(UIButton button)
    {
        SelectButton(button);
    }

    public void SelectButton(UIButton button)
    {
        if (Interactable == false) return;

        buttons[selectButtonIndex].SetUnFocus();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (button == buttons[i])
            {
                selectButtonIndex = i;
                button.SetFocus();
                break;
            }
        }
    }

    // TODO
    public void SelectNext() { }
    public void SelectPrevious() { }
}
