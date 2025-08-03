using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundChangePanel : MonoBehaviour
{
    [SerializeField] private Button changeButton;
    [SerializeField] private TMP_Text soundTypeText;

    public Action OnClickChangeButton;


    public void Init(ESoundPlayerType currentType)
    {
        changeButton.onClick.AddListener(OnClick);
        Open(currentType);
    }

    public void Open(ESoundPlayerType currentType)
    {
        soundTypeText.text = currentType.ToString();
        gameObject.SetActive(true);
    }

    private void OnClick()
    {
        OnClickChangeButton?.Invoke();
        this.gameObject.SetActive(false);
    }


    // public void ChangeSlider()
    // {
    //     // TODO: DataManager
    //     if (currentText.text == BGMSlider.nameStr)
    //     {
    //         BGMSlider.gameObject.SetActive(true);
    //         EffectSlider.gameObject.SetActive(false);
    //         // BGMSlider.Setting(DataManager.Inst.DefaultSaveData.BGMSoundValue);
    //     }
    //     if(currentText.text == EffectSlider.nameStr)
    //     {
    //         BGMSlider.gameObject.SetActive(false);
    //         EffectSlider.gameObject.SetActive(true);
    //         // EffectSlider.Setting(DataManager.Inst.DefaultSaveData.EffectSoundValue);
    //     }
    //     coverPanel.SetActive(false);
    // }

    // public void Mute()
    // {
    //     if (currentText.text == BGMSlider.nameStr)
    //     {
    //         BGMSlider.Mute();
    //     }
    //     if (currentText.text == EffectSlider.nameStr)
    //     {
    //         EffectSlider.Mute();
    //     }
    // }
}
