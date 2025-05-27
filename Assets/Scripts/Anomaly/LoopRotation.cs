using System.Collections.Generic;
using UnBocal.TweeningSystem;
using UnityEngine;

public class LoopRotation : Anomaly
{
    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private Transform _target = null;

    Quaternion _baseRotation;

    // -------~~~~~~~~~~================# // Animation
    private Tween _animator = new Tween();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        Type = $"{AnomalyType.Movement}";

        _target = _target == null ? transform : _target;
        _baseRotation = _target.rotation;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public override void Trigger()
    {
        base.Trigger();

        _animator.StopAndClear();
        _animator.RotationAngleAxis(_target, 360f, Vector3.up, 1f, EaseType.OutExpo).OnFinished += _animator.Start;
        _animator.Clear();
        _animator.RotationAngleAxis(_target, 360f, Vector3.up, .5f).OnFinished += _animator.Start;
        _animator.Start();
    }

    public override void Fix()
    {
        base.Fix();

        _animator.StopAndClear();

        _target.rotation = _baseRotation;
    }
}
