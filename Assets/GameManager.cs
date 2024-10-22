using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    [SerializeField] private float pulseScale = 1.2f;
    [SerializeField] private float pulseDuration = 0.2f;
    [SerializeField] private Vector3 slideDownOffset = new Vector3(0, -5, 0);
    [SerializeField] private Vector3 scaleSmallerFactor = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Vector3 flyUpOffset = new Vector3(0, 5, 0);
    [SerializeField] private Vector3 fadeUpOffset = new Vector3(0, 5, 0);
    [SerializeField] private Vector3 zoomFactor = new Vector3(2f, 2f, 2f);
    [SerializeField] private float duration = 1f;

    private void Start()
    {
        if (target != null)
        {
            originalPosition = target.transform.position;
            originalScale = target.transform.localScale;
        }

        Image image = target.GetComponent<Image>();
    }

    public void SlideDown()
    {
        target.transform.DOMove(originalPosition + slideDownOffset, duration)
            .OnComplete(() => target.transform.DOMove(originalPosition, duration));
    }

    public void ScaleSmaller()
    {
        Image image = target.GetComponent<Image>();
        if (image != null)
        {
            target.transform.DOScale(originalScale - scaleSmallerFactor, duration);
            image.DOFade(0, duration).OnComplete(() =>
            {
                target.transform.DOScale(originalScale, duration);
                image.DOFade(1, duration);
            });
        }
    }

    public void PulseOnce()
    {
        Image image = target.GetComponent<Image>();
        if (image != null)
        {
            target.transform.DOScale(originalScale * pulseScale, pulseDuration)
                .OnComplete(() => target.transform.DOScale(originalScale, pulseDuration));
            image.DOFade(0.8f, pulseDuration)
                .OnComplete(() => image.DOFade(1f, pulseDuration));
        }
    }

    public void FlyUp()
    {
        target.transform.DOMove(originalPosition + flyUpOffset, duration)
            .OnComplete(() => target.transform.DOMove(originalPosition, duration));
    }

    public void FadeUp()
    {
        Image image = target.GetComponent<Image>();
        if (image != null)
        {
            image.DOFade(0, duration);
            target.transform.DOMove(originalPosition + fadeUpOffset, duration)
                .OnComplete(() =>
                {
                    image.DOFade(1, duration);
                    target.transform.DOMove(originalPosition, duration);
                });
        }
    }

    public void Zoom()
    {
        Vector3 targetScale = Vector3.Scale(originalScale, zoomFactor);
        target.transform.DOScale(targetScale, duration)
            .OnComplete(() => target.transform.DOScale(originalScale, duration));
    }
}
