using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundSelectPanel : MonoBehaviour
{
    [SerializeField] private Button bgmButton;
    [SerializeField] private Button masterButton;
    [SerializeField] private Button effectButton;
    [SerializeField] private Button selectButton;

    public Action<ESoundPlayerType> OnSelectedSoundButton;
    public Action OnClosed;


    public void Init()
    {
        selectButton.onClick.RemoveAllListeners();
        bgmButton.onClick.RemoveAllListeners();
        effectButton.onClick.RemoveAllListeners();

        selectButton.onClick.AddListener(CloseSelectPanel);
        masterButton.onClick.AddListener(() => OnSelect(ESoundPlayerType.Master));
        bgmButton.onClick.AddListener(() => OnSelect(ESoundPlayerType.BGM));
        effectButton.onClick.AddListener(() => OnSelect(ESoundPlayerType.Effect));

        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    private void OnSelect(ESoundPlayerType type)
    {
        OnSelectedSoundButton?.Invoke(type);
        CloseSelectPanel();
    }

    public void CloseSelectPanel()
    {
        OnClosed?.Invoke();
        this.gameObject.SetActive(false);
    }
}
