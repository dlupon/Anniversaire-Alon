using UnBocal.TweeningSystem;
using UnityEngine;

public class Lighting : Anomaly
{
    // -------~~~~~~~~~~================# // Light
    [SerializeField] private Light _target = null;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _targetIntencity = -1f;
    [SerializeField] private Color _targetColor = default;

    // -------~~~~~~~~~~================# // Animation
    private Tween _animator = new Tween();
    private float _baseIntencity;
    private Color _baseColor;

    private void Start()
    {
        Type = $"{AnomalyType.Lighting}";

        _target = _target == null ? GetComponent<Light>() : _target;
        _baseIntencity = _target.intensity;
        _baseColor = _target.color;
    }


    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    [ContextMenu(nameof(Trigger))]
    public override void Trigger()
    {
        base.Trigger();
        _animator.StopAndClear();
        if (_targetColor != default) _animator.Color(_target, _targetColor, _duration, EaseType.InOutElastic);
        if (_targetIntencity != -1f) _animator.Interpolate<float>(_target, (x) => _target.intensity = x, _target.intensity, _targetIntencity, _duration, EaseType.InOutElastic);
        _animator.Start();
    }

    [ContextMenu(nameof(Fix))]
    public override void Fix()
    {
        base.Fix();
        _animator.StopAndClear();
        _target.intensity = _baseIntencity;
        _target.color = _baseColor;
    }
}
