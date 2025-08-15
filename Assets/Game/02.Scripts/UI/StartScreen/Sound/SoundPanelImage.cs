using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ESoundCondition
{
    None = -1,
    Big,
    Middle,
    Small,
    X
}

public class SoundPanelImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image bigSound;
    [SerializeField] private Image middleSound;
    [SerializeField] private Image smallSound;
    [SerializeField] private Image xSound;
    [SerializeField] private Image sound;

    [SerializeField] private bool isClickMute;
    [SerializeField] private bool isSoundPanelOpen;

    public Action OnClick;

    public void SetSoundImage(float volume)
    {
        float value = (int)(volume * 100f);
        if (value == 0)
        {
            ChangeCondition(ESoundCondition.X);
        }
        else if (value > 66)
        {
            ChangeCondition(ESoundCondition.Big);
        }
        else if (value > 33)
        {
            ChangeCondition(ESoundCondition.Middle);
        }
        else if (value > 0)
        {
            ChangeCondition(ESoundCondition.Small);
        }
    }

    public void ChangeCondition(ESoundCondition condition)
    {
        switch (condition)
        {
            case ESoundCondition.Big:
                {
                    SetFade(bigSound, 1);
                    SetFade(middleSound, 1);
                    SetFade(smallSound, 1);
                    xSound.gameObject.SetActive(false);
                }
                break;

            case ESoundCondition.Middle:
                {
                    SetFade(bigSound, 0);
                    SetFade(middleSound, 1);
                    SetFade(smallSound, 1);
                    xSound.gameObject.SetActive(false);
                }
                break;

            case ESoundCondition.Small:
                {
                    SetFade(bigSound, 0);
                    SetFade(middleSound, 0);
                    SetFade(smallSound, 1);
                    xSound.gameObject.SetActive(false);
                }
                break;

            case ESoundCondition.X:
                {
                    SetFade(bigSound, 0);
                    SetFade(middleSound, 0);
                    SetFade(smallSound, 0);
                    xSound.gameObject.SetActive(true);
                }
                break;
        }
    }

    private void SetFade(Image image, float value)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, value);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
