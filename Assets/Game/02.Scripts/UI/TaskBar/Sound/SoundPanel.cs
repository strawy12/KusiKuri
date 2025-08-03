using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SoundVolumeData
{
    public ESoundPlayerType soundType;
    public float volume;
    public bool isMute;

    public SoundVolumeData(ESoundPlayerType soundType, float volume = 0.6f, bool isMute = false)
    {
        this.soundType = soundType;
        this.volume = volume;
        this.isMute = isMute;
    }
}

public class SoundPanel : MonoUI
{
    [SerializeField] private SoundSlider soundSlider;
    [SerializeField] private SoundChangePanel changePanel;
    [SerializeField] private SoundSelectPanel selectPanel;
    [SerializeField] private GameObject taskbarSoundImage;
    [SerializeField] private GameObject coverPanel;

    // TODO: 임시 저장 데이터
    private Dictionary<ESoundPlayerType, SoundVolumeData> soundVolumeDataDictionary;

    private bool isOpen;
    private ESoundPlayerType selectedSoundType = ESoundPlayerType.Master;

    private SoundVolumeData SelectSoundData => soundVolumeDataDictionary[selectedSoundType];

    void Start()
    {
        Init();
        SetActive(false);
    }

    public void Init()
    {
        soundVolumeDataDictionary = new();
        soundVolumeDataDictionary.Add(ESoundPlayerType.Master, new SoundVolumeData(ESoundPlayerType.Master, 1f));
        soundVolumeDataDictionary.Add(ESoundPlayerType.BGM, new SoundVolumeData(ESoundPlayerType.BGM, 0.6f));
        soundVolumeDataDictionary.Add(ESoundPlayerType.Effect, new SoundVolumeData(ESoundPlayerType.Effect, 0.6f));

        selectedSoundType = ESoundPlayerType.Master;

        //TODO: DataManager
        // if (DataManager.Inst.DefaultSaveData != null)
        // {
        //     bgm.Init(DataManager.Inst.DefaultSaveData.BGMSoundValue);
        //     effect.Init(DataManager.Inst.DefaultSaveData.EffectSoundValue);
        // }
        // else
        // {
        //    DataManager.Inst.CreateDefaultSaveData();
        //}

        soundSlider.Init();
        soundSlider.SetVolumeData(SelectSoundData);

        changePanel.Init(selectedSoundType);
        changePanel.OnClickChangeButton += OnClickChangeButton;

        selectPanel.Init();
        selectPanel.OnClosed += ClosedSelectPanel;
        selectPanel.OnSelectedSoundButton += SetSoundType;

        selectPanel.gameObject.SetActive(false);
    }



    public void SetSoundType(ESoundPlayerType soundType)
    {
        selectedSoundType = soundType;
        soundSlider.SetVolumeData(SelectSoundData);
    }

    public void Mute()
    {
        // changePanel.Mute();
    }

    public void OpenPanel()
    {
        //isOpen = true;
        EventManager.StartListening(ECoreEvent.LeftButtonClick, CheckClose);
        transform.localScale = Vector3.zero;
        SetActive(true);
        transform.DOKill();
        transform.DOScale(1f, 0.15f).SetEase(Ease.OutCubic);
    }

    private void CheckClose(EventParamData paramData)
    {
        if (!isOpen) { isOpen = true; return; }

        if (selectPanel.gameObject.activeSelf)
        {
            // TODO: Define
            // if (Define.ExistInHits(gameObject, hits[0]) == false && Define.ExistInHits(selectPanel.gameObject, hits[0]) == false
            //     && Define.ExistInHits(changePanel.gameObject, hits[0]) == false && Define.ExistInHits(taskbarSoundImage, hits[0]) == false)
            // {
            //     Close();
            // }
        }
        else
        {
            // if (Define.ExistInHits(gameObject, hits[0]) == false)
            // {
            //     Close();
            // }
        }

    }

    public void Close()
    {
        EventManager.StopListening(ECoreEvent.LeftButtonClick, CheckClose);
        selectPanel.CloseSelectPanel();
        isOpen = false;
        transform.DOKill();
        SetActive(false);
    }

    private void OnClickChangeButton()
    {
        coverPanel.SetActive(true);
        selectPanel.Open();
    }

    private void ClosedSelectPanel()
    {
        coverPanel.SetActive(false);
        changePanel.Open(selectedSoundType);
    }
}
