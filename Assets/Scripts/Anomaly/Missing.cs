using System;
using System.Collections.Generic;
using UnBocal.TweeningSystem;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Missing : Anomaly
{
    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private Transform _target = null;

    // -------~~~~~~~~~~================# // Animation
    private Tween _animator = new Tween();
    private Vector3 _baseScale;

    protected override void Start()
    {
        base.Start();
        Type = $"{AnomalyType.Missing}";

        _target = _target == null ? transform : _target;
        _baseScale = _target.localScale;
    }


    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public override void Trigger()
    {
        base.Trigger();
        _animator.StopAndClear();
        _animator.Scale(_target, 0, 1, EaseType.InBack);
        _animator.Start();
    }

    public override void Fix()
    {
        base.Fix();
        _animator.StopAndClear();
        _target.localScale = _baseScale;
    }
}
