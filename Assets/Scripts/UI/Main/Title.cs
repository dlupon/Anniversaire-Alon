using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Game";
    [SerializeField] private TextMeshProUGUI _version;

    [SerializeField] private Image _transition;
    private Tween _animator = new Tween();

    private void Awake()
    {
        EventBus.ToMain += ToMain;
    }

    private void OnDestroy()
    {
        EventBus.ToMain -= ToMain;
    }

    private void Start()
    {
        _version.text = _version.text.Replace("{v}", Application.version);
    }

    public void ToGame()
    {
        _transition.gameObject.SetActive(true);
        
        Tween.KillAndClear(_transition);
        _animator.Color(_transition, new Color(0, 0, 0, 0), Color.black, 1.5f).OnFinished += () => EventBus.ToGame?.Invoke();
        _animator.Start();
    }

    public void ToMain()
    {
        Tween.KillAndClear(_transition);
        _animator.Color(_transition, Color.black, new Color(0, 0, 0, 0), .1f).OnFinished += () => _transition.gameObject.SetActive(false);
        _animator.Start();
    }
}