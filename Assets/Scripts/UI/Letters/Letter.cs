using UnBocal.TweeningSystem;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = .5f;
    private Tween _animator = new Tween();
    private CanvasGroup _canvas;

    protected virtual void Start()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        _animator.Interpolate<float>(this, (x) => _canvas.alpha = x, 0f, 1f, _fadeDuration);
        _animator.Start();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}