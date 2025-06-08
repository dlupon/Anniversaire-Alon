using UnBocal.TweeningSystem;
using UnityEngine;

public class NenouilleStading : Anomaly
{
    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private Transform _target = null;

    // -------~~~~~~~~~~================# // Animation
    private Tween _animator = new Tween();
    private Vector3 _baseScale;

    private void Start()
    {
        Type = $"Nenouille ?";

        _target = _target == null ? transform : _target;
        _baseScale = _target.localScale;
        _target.localScale = Vector3.zero;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    [ContextMenu(nameof(Trigger))]
    public override void Trigger()
    {
        base.Trigger();
        _animator.StopAndClear();
        _animator.Scale(_target, _baseScale, 1, EaseType.OutBack);
        _animator.Start();
    }

    [ContextMenu(nameof(Fix))]
    public override void Fix()
    {
        base.Fix();
        Tween.KillAndClear(_target);
        _animator.StopAndClear();
        _target.localScale = Vector3.zero;
    }
}
