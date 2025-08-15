using DG.Tweening;
using UnityEngine;

public class WindowsLoginSystem : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration;

    private void Awake()
    {
        canvasGroup ??= GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        EventManager.StartListening(EWindowEvent.WindowsSuccessLogin, FadeOut);
    }

    private void FadeOut(EventParamData d)
    {
        EventManager.StopListening(EWindowEvent.WindowsSuccessLogin, FadeOut);
        canvasGroup.DOFade(0f, fadeDuration).OnComplete(() => gameObject.SetActive(false));
    }
}
