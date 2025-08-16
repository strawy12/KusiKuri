using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using System;

public class NoticePanel : MonoUI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private static readonly Vector2 NOTICE_POS = new Vector2(-17.5f, -470f);
    private static readonly float NOTICE_DELAYTIME = 5f;
    private static readonly float NOTICE_DURATION = 0.3f;
    private static readonly float NOTICE_SIZE_DURATION = 0.13f;

    private float noticeAlphalightly = 1f;

    [SerializeField] private TMP_Text headText;
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private TMP_Text dateText;

    [SerializeField] private Image headIconImage;
    [SerializeField] private Image headIconBG;
    [SerializeField] private Image bodyIconImage;
    [SerializeField] private Image bodyIconBG;

    private RectTransform rectTransform;

    private bool isEnter;
    private bool isOpen;
    private bool isPlayAnim;

    [SerializeField] NoticeData testData;

    private IEnumerator Start()
    {
        Init();
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        Notice(testData);
    }

    public void Init()
    {
        canvasGroup ??= GetComponent<CanvasGroup>();
        rectTransform ??= GetComponent<RectTransform>();
        Close();
    }

    public void Notice(NoticeData noticeData)
    {
        NoticeSetting(noticeData);

        Vector2 pos = new Vector2(rectTransform.rect.width, NOTICE_POS.y);
        rectTransform.anchoredPosition = pos;
        rectTransform.localScale = Vector3.one;

        SetActive(true);

        //Sound.OnPlaySound?.Invoke(Sound.EAudioType.Notice);
        // EventManager.TriggerEvent(ENoticeEvent.GeneratedNotice);

        NoticeAnimation();
    }


    private void NoticeSetting(NoticeData noticeData)
    {
        headText.SetText(noticeData.head);
        bodyText.SetText(noticeData.body);

        headIconBG.gameObject.SetActive(noticeData.headIcon != null);
        headIconImage.sprite = noticeData.headIcon;
        headIconImage.color = noticeData.headColor;
        headIconBG.color = noticeData.headBGColor;

        bodyIconBG.gameObject.SetActive(noticeData.bodyIcon != null);
        bodyIconImage.sprite = noticeData.bodyIcon;
        bodyIconImage.color = noticeData.bodyColor;
        bodyIconBG.color = noticeData.bodyBGColor;

        // 우하단 정렬
        rectTransform.anchorMin = new Vector2(1f, 0.5f);
        rectTransform.anchorMax = new Vector2(1f, 0.5f);
        rectTransform.pivot = new Vector2(1f, 0f);
        rectTransform.localScale = Vector3.one;

        isOpen = false;
    }

    public void NoticeAnimation()
    {
        Sequence seq = DOTween.Sequence();

        isPlayAnim = true;

        seq.Append(rectTransform.DOScale(1.25f, NOTICE_DURATION + NOTICE_SIZE_DURATION));
        seq.Join(rectTransform.DOAnchorPosX(NOTICE_POS.x, NOTICE_DURATION));
        seq.Append(rectTransform.DOScale(1f, NOTICE_DURATION));
        seq.AppendCallback(() =>
        {
            isPlayAnim = false;
            isOpen = true;
        });
    }

    public void ImmediatelyStop()
    {
        rectTransform.DOKill();
        // EventManager.StopListening(ENoticeEvent.OpenNoticeSystem, NoticeStopEvent);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPlayAnim)
        {
            return;
        }

        if (!isEnter && isOpen)
        {
            rectTransform.DOKill();
            rectTransform.DOScale(Vector3.one * 1.05f, 0.1f);
            isEnter = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isEnter && isOpen)
        {
            rectTransform.DOKill();
            rectTransform.DOScale(Vector3.one, 0.1f);
            isEnter = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isEnter && isOpen)
        {
            rectTransform.DOKill();
            rectTransform.DOScale(Vector3.one * 0.85f, 0.1f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isEnter && isOpen)
        {
            // 여기서 클릭 이벤트 발생
            rectTransform.DOKill();
            Sequence seq = DOTween.Sequence();
            seq.Append(rectTransform.DOScale(Vector3.one, 0.05f));
            seq.Append(rectTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InCubic));
            seq.AppendCallback(Close);
        }
    }

    private void Close()
    {
        SetActive(false);
    }
}
