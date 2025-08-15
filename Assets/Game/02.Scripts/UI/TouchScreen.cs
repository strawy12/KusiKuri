// TouchScreen.cs
using System;
using System.Linq.Expressions;
using System.Numerics;
using ExtenstionMethod;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TouchScreen : MonoBehaviour, IPointerDownHandler
{
    private ConfigDataObject ConfigData => GameManager.Inst.ConfigData;

    [Header("이벤트")]
    public Action<BigInteger> OnUpdatedAmount;
    public Action<PointerEventData> OnTouch;

    // 외부에서 연결할 간단한 지갑(예시)
    public CurrencyWallet wallet;

    private float _touchLimitTimer;
    private int _touchCount;

    void Awake()
    {
        // 초당 제한용 윈도우 시작 시각 초기화
        _touchLimitTimer = Time.unscaledTime;
        _touchCount = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 초당 탭 제한
        if (IsLimitTouch())
        {
            return;
        }

        BigInteger amount = CalcAmount();

        // 지갑 반영
        if (wallet != null)
        {
            wallet.Add(amount);
        }

        // 이벤트 통지(UI 갱신 등)
        OnUpdatedAmount?.Invoke(amount);

        // 피드백
        OnTouch?.Invoke(eventData);
        Debug.Log($"Touch - {wallet.amount}");
    }

    private BigInteger CalcAmount()
    {
        // 추후 TPM은 여기가 아닌 따로 수치 설정한 곳에서 가져오도록
        BigInteger result = ConfigData.baseTouchPerMoney;

        // 배수 적용
        decimal multiplier = 0m;
        result.Multiply(multiplier);

        // 크리티컬
        bool isCritical = Random.Range(0f, 1f) <= ConfigData.criticalChance;
        if (isCritical)
        {
            decimal criticalMul = Math.Max(1m, ConfigData.criticalMultiplier);
            result.Multiply(criticalMul);
        }

        return result;
    }

    private bool IsLimitTouch()
    {
        if (ConfigData.maxTapsPerSecond > 0)
        {
            float now = Time.unscaledTime;
            if (now - _touchLimitTimer >= 1f)
            {
                _touchLimitTimer = now;
                _touchCount = 0;
            }
            if (_touchCount >= ConfigData.maxTapsPerSecond)
            {
                return true;
            }

            _touchCount++;
        }

        return false;
    }
}

[Serializable]
public class CurrencyWallet
{
    [Tooltip("현재 재화 보유량")]
    public BigInteger amount;


    public void Add(BigInteger delta)
    {
        if (delta <= 0) return;
        amount += delta;
    }
}
