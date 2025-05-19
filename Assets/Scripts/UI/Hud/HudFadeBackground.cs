using UnBocal.TweeningSystem;
using UnityEngine;
using UnityEngine.UI;

public class HudFadeBackground : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Color _endColor;
    [SerializeField] private float _fadeTime;
    private Color _startColor => new Color(_endColor.r, _endColor.g, _endColor.b, 0);
    private Tween _backgroundAnimator = new Tween();

    private void Awake()
    {
        _backgroundAnimator.Color(_background, _startColor, _endColor, _fadeTime * .5f).OnFinished += OnFadeMid;
        _backgroundAnimator.Color(_background, _endColor, _startColor, _fadeTime * .5f, pDelay : _fadeTime * .5f);

        EventBus.NeedCameraFade += StartFade;
    }

    private void StartFade()
    {
        _backgroundAnimator.Stop();
        _backgroundAnimator.Start();
    }

    private void OnFadeMid()
    {
        _background.color = _startColor;
        EventBus.CameraFadeMid?.Invoke();
    }
}
