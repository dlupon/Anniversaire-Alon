using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private RawImage _view;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _white = Color.white;
    [SerializeField] private Color _black = Color.black;
    [SerializeField] private float _fadeIn = .1f;
    [SerializeField] private float _wait = .1f;
    [SerializeField] private float _fadeOut = .2f;
    private Tween _animator = new Tween();

    private void Awake()
    {
        EventBus.NeedCameraFade += StartFade;
        EventBus.Report += Searching;
        EventBus.AnomalyFixed += AnomalyRemoved;
        EventBus.AnomalyNotFounded += AnomalyNotFound;
    }
    
    private void OnDestroy()
    {
        EventBus.NeedCameraFade -= StartFade;
        EventBus.Report -= Searching;
        EventBus.AnomalyFixed -= AnomalyRemoved;
        EventBus.AnomalyNotFounded -= AnomalyNotFound;
    }

    private void StartFade()
    {
        Tween.Kill(_view);
        _animator.Clear();
        _animator.Color(_view, _black, _fadeIn).OnFinished += OnFadeMid;
        _animator.Color(_view, _black, _white, _fadeOut, pDelay: _fadeIn + _wait);
        _animator.Start();
    }

    private void OnFadeMid()
    {
        EventBus.CameraFadeMid?.Invoke();
    }

    private void Searching()
    {
        _text.color = _white;
        _animator.StopAndClear();
        _animator.Whrite(_text, "Searching...", 1);
        _animator.Color(_view, _black, _fadeOut, pDelay: _fadeIn + _wait);
        _animator.Start();
    }

    private void AnomalyRemoved()
    {
        _animator.StopAndClear();
        _animator.Whrite(_text, "Anomaly Removed", 1);
        _animator.Color(_view, _black, _white, _fadeOut, pDelay: _fadeIn + _wait);
        _animator.Color(_view, _black, _white, _fadeOut, pDelay: _fadeIn + _wait);
        _animator.Color(_text, _white, new Color(1, 1, 1, 0), _fadeOut, pDelay: 3f);
        _animator.Start();
    }

    private void AnomalyNotFound()
    {
        _animator.StopAndClear();
        _animator.Whrite(_text, "Anomaly Not Found", 1);
        _animator.Color(_view, _black, _white, _fadeOut, pDelay: _fadeIn + _wait);
        _animator.Color(_text, _white, new Color(1, 1, 1, 0), _fadeOut, pDelay: 3f);
        _animator.Start();
    }
}
