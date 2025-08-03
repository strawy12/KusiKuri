using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Audio;
using UnityEditor.Rendering;

public class SoundSlider : MonoBehaviour
{
    // TODO: 추후에는 필요 없지않을까 예상
            [SerializeField] private SoundPanel soundPanel;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text valueText;

    [SerializeField] private SoundPanelImage soundImage;
    [SerializeField] private SoundPanelImage soundTaskbarImage;

    private SoundVolumeData volumeData;

    public void Init()
    {
        soundImage.OnClick += Mute;
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolumeData(SoundVolumeData volumeData)
    {
        this.volumeData = volumeData;
        UpdateSlider();
        SetMixGroup();
    }

    public void SetVolume(float volume)
    {
        if (volumeData.isMute)
        {
            volumeData.isMute = false;
        }

        volumeData.volume = volume;

        UpdateSlider();
        SetMixGroup();

        // TODO: 추후 저장 기능도 고려
    }

    public void SetMixGroup()
    { // -20 - 10
        return;
        string typeString = volumeData.soundType.ToString();

        if (slider.value == 0)
        {
            audioMixer.SetFloat(typeString, -80);
            return;
        }
        audioMixer.SetFloat(typeString, (slider.value * 35) - 25);
    }

    private void UpdateSlider()
    {
        slider.value = volumeData.volume;
        SetValueText();

        soundImage.SetSoundImage(volumeData.volume);
        if (soundTaskbarImage != null)
        {
            soundTaskbarImage.SetSoundImage(volumeData.volume);
        }
    }

    private void SetValueText()
    {
        int value = (int)(volumeData.volume * 100f);
        valueText.text = value.ToString();
    }

    public void Mute()
    {
        if (!volumeData.isMute)
        {
            volumeData.isMute = true;
            SetMuteSoundImage();

            audioMixer.SetFloat(volumeData.soundType.ToString(), -80);
        }
        else
        {
            volumeData.isMute = false;
            UpdateSlider();
            SetMixGroup();
        }

    }

    private void SetMuteSoundImage()
    {
        soundImage.ChangeCondition(ESoundCondition.X);

        if (soundTaskbarImage != null)
        {
            soundTaskbarImage.ChangeCondition(ESoundCondition.X);
        }
    }

}
