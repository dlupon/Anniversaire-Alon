using UnBocal.TweeningSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private RawImage _view;
    [SerializeField] private Color _startColor = Color.white;
    [SerializeField] private Color _endColor = Color.black;
    [SerializeField] private float _fadeIn = .1f;
    [SerializeField] private float _wait = .1f;
    [SerializeField] private float _fadeOut = .2f;
    private Tween _gameview = new Tween();

    private void Awake()
    {
        EventBus.NeedCameraFade += StartFade;
    }

    private void StartFade()
    {
        _gameview.StopAndClear();
        _gameview.Color(_view, _endColor, _fadeIn).OnFinished += OnFadeMid;
        _gameview.Color(_view, _endColor, _startColor, _fadeOut * .5f, pDelay: _fadeIn + _wait);
        _gameview.Start();
    }

    private void OnFadeMid()
    {
        EventBus.CameraFadeMid?.Invoke();
    }
}
