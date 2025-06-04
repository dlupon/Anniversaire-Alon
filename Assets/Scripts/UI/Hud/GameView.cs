using TMPro;
using UnBocal.TweeningSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Components
    [Header("Components")]
    [SerializeField] private RawImage _view;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _warning;

    private string _warningText;
    
    // -------~~~~~~~~~~================# // Animations
    [Header("Animations")]
    [SerializeField] private Color _white = Color.white;
    [SerializeField] private Color _black = Color.black;
    [SerializeField] private float _fadeIn = .1f;
    [SerializeField] private float _wait = .1f;
    [SerializeField] private float _fadeOut = .2f;
    private Tween _animator = new Tween();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Awake()
    {
        EventBus.NeedCameraFade += StartFade;
        EventBus.Report += Searching;
        EventBus.AnomalyFixed += AnomalyRemoved;
        EventBus.AnomalyNotFounded += AnomalyNotFound;
        EventBus.TooManyAnomalies += Warn;
    }
    
    private void OnDestroy()
    {
        EventBus.NeedCameraFade -= StartFade;
        EventBus.Report -= Searching;
        EventBus.AnomalyFixed -= AnomalyRemoved;
        EventBus.AnomalyNotFounded -= AnomalyNotFound;
        EventBus.TooManyAnomalies -= Warn;
    }

    private void Start()
    {
        _warningText = _warning.text;
        _warning.text = "";
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Animations
    private void StartFade()
    {
        Tween.Kill(_view);

        _animator.Clear();
        _animator.Color(_view, _black, _fadeIn).OnFinished += OnFadeMid;
        _animator.Color(_view, _black, _white, _fadeOut, pDelay: _fadeIn + _wait);
        _animator.Start();
    }

    private void OnFadeMid() => EventBus.CameraFadeMid?.Invoke();

    private void Searching()
    {
        Tween.KillAndClear(_text);
        Tween.KillAndClear(_view);

        _text.color = _white;
        _animator.Whrite(_text, "Searching...", 1);
        _animator.Start();
    }

    private void AnomalyRemoved()
    {
        Tween.KillAndClear(_text);
        Tween.KillAndClear(_view);

        _animator.Whrite(_text, "Anomaly Removed", 1);
        AnomalyDone();
        _animator.Start();
    }

    private void AnomalyNotFound()
    {
        Tween.KillAndClear(_text);
        Tween.KillAndClear(_view);

        _animator.Whrite(_text, "Anomaly Not Found", 1);
        AnomalyDone();
        _animator.Start();
    }

    private void AnomalyDone()
    {
        _view.color = _black;
        _animator.Color(_view, _black, _white, .3f, pDelay: 1f);
        _animator.Color(_text, _white, new Color(1, 1, 1, 0), _fadeOut, pDelay: 3f);
    }

    private void Warn()
    {
        _animator.Clear(_warning);
        _animator.Whrite(_warning, _warningText, 2f);
        _animator.Whrite(_warning, "", 0f, pDelay:5f);
        _animator.Play();
        _animator.Clear(_warning);
    }
}
