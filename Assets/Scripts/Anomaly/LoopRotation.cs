using System.Collections.Generic;
using UnBocal.TweeningSystem;
using UnityEngine;

public class LoopRotation : Anomaly
{
    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private Transform _target = null;
    [SerializeField] private Transform _targetAxis = null;
    private Vector3 _axis;

    Quaternion _baseRotation;

    // -------~~~~~~~~~~================# // Animation
    [SerializeField] private float _rotationDuration = 1f;
    private Tween _animator = new Tween();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        Type = $"{AnomalyType.Movement}";

        _target = _target == null ? transform : _target;
        _baseRotation = _target.rotation;
        _axis = _targetAxis == null ? Vector3.up : (_targetAxis.position - _target.position).normalized;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    [ContextMenu(nameof(Trigger))]
    public override void Trigger()
    {
        base.Trigger();

        Tween.Kill(_target);
        _animator.RotationAngleAxis(_target, 360f, _axis, _rotationDuration * 1.5f, EaseType.InSin).OnFinished += _animator.Start;
        _animator.Start();
        _animator.Clear();
        _animator.RotationAngleAxis(_target, 360f, _axis, _rotationDuration).OnFinished += _animator.Start;
    }

    [ContextMenu(nameof(Fix))]
    public override void Fix()
    {
        base.Fix();

        _animator.StopAndClear();

        _target.rotation = _baseRotation;
    }
}
