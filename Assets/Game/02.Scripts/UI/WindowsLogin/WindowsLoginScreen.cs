using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WindowsLoginScreen : MonoUI
{
    [SerializeField] private SoundPanel soundPanel;

    [Header("Loading")]
    [SerializeField] private RectTransform loadingIcon;
    [SerializeField] private float loadingTurnSpeed;
    [Header("Login")]
    [SerializeField] private Button loginButton;

    private bool isLoading = false;
    private void Start()
    {
        Init();
    }
    public void LoginReset()
    {
        isLoading = false;
        SetActive(true);
        Init();
    }
    private void Init()
    {
        EventManager.StartListening(EWindowEvent.OpenWindowLockScreen, Open);
        soundPanel.Init();
        loginButton.onClick.RemoveAllListeners();
        loginButton.onClick.AddListener(SuccessLogin);
        SetActive(false);
    }

    private void Open(EventParamData data)
    {
        EventManager.StopListening(EWindowEvent.OpenWindowLockScreen, Open);
        SetActive(true);
    }

    private void SuccessLogin()
    {
        StartCoroutine(LoadingCoroutine(() =>
        {
            EventManager.TriggerEvent(EWindowEvent.WindowsSuccessLogin);
            EndLogin();
        }));
    }

    private IEnumerator LoadingCoroutine(Action callBack)
    {
        if (isLoading) yield break;

        isLoading = true;

        loginButton.gameObject.SetActive(false);
        loadingIcon.gameObject.SetActive(true);

        float delay = Random.Range(0.7f, 2f);
        while (delay > 0f)
        {
            loadingIcon.eulerAngles += Vector3.back * loadingTurnSpeed * Time.deltaTime;
            delay -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        loadingIcon.gameObject.SetActive(false);
        isLoading = false;

        callBack?.Invoke();
    }


    private void EndLogin()
    {
        // TODO: Guide
        // EventManager.TriggerEvent(EGuideEventType.ClearGuideType, new object[1] { EGuideTopicName.FirstLoginGuide });
    }
}
