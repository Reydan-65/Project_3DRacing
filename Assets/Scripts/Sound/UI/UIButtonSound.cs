using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    private new AudioSource audio;

    private UIButton[] uIButtons;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        uIButtons = GetComponentsInChildren<UIButton>(true);

        for (int i = 0; i < uIButtons.Length; i++)
        {
            uIButtons[i].PointerEnter += OnPointerEnter;
            uIButtons[i].PointerClick += OnPointerClick;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < uIButtons.Length; i++)
        {
            uIButtons[i].PointerEnter -= OnPointerEnter;
            uIButtons[i].PointerClick -= OnPointerClick;
        }
    }

    private void OnPointerEnter(UIButton arg0)
    {
        audio.PlayOneShot(hoverSound);
    }

    private void OnPointerClick(UIButton arg0)
    {
        audio.PlayOneShot(clickSound);
    }
}
