using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WindowsLockScreen : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private float targetMovementY = 1000f;
    [SerializeField] private float offset = 300;

    private float beginPosY;
    private Vector3 originPos;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private bool isDrag;
    private bool anyKeyUp;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = transform as RectTransform;
        originPos = rectTransform.anchoredPosition;
    }
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        gameObject.SetActive(true);
        rectTransform.anchoredPosition = originPos;
        anyKeyUp = false;
        StartCoroutine(KeyUpCor());
    }

    IEnumerator KeyUpCor()
    {
        yield return new WaitForSeconds(1f);
        InputManager.Inst.AddAnyKeyInput(onKeyUp: AnyKeyUp);
    }

    private void AnyKeyUp()
    {
        if (anyKeyUp || isDrag)
        {
            isDrag = false;
            return;
        }

        anyKeyUp = true;
        rectTransform.DOAnchorPos(originPos + Vector3.up * targetMovementY, 0.1f).OnComplete(OpenLoginScreen);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        beginPosY = eventData.position.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (anyKeyUp)
        {
            return;
        }

        float movementY = eventData.position.y - beginPosY;
        if (movementY < 0f)
        {
            return;
        }

        rectTransform.anchoredPosition = originPos + Vector3.up * movementY;
        canvasGroup.alpha = (targetMovementY - movementY) / targetMovementY;
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float movementY = eventData.position.y - beginPosY;
        if (movementY + offset >= targetMovementY)
        {
            rectTransform.DOAnchorPos(originPos + Vector3.up * targetMovementY, 0.1f).OnComplete(OpenLoginScreen);
        }
        else
        {
            rectTransform.DOAnchorPos(originPos, 0.2f);
            canvasGroup.alpha = 1f;
        }
    }

    private void OpenLoginScreen()
    {
        EventManager.TriggerEvent(EWindowEvent.OpenWindowLockScreen);
        //MonologSystem.OnStartMonolog?.Invoke(Constant.MonologKey.WINDOWS_LOGIN_SCREEN_OPEN, true);
        gameObject.SetActive(false);
    }
}
