using System.Collections.Generic;
using UnBocal.TweeningSystem;
using UnityEngine;

public class Movement : Anomaly
{
    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private Transform _target = null;
    [SerializeField] private Transform _pointContainer;
    private List<Transform> _points = new List<Transform>();

    Vector3 _baseScale;
    Vector3 _basePosition;
    Quaternion _baseRotation;

    // -------~~~~~~~~~~================# // Animation
    private Tween _animator = new Tween();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        Type = $"{AnomalyType.Movement}";

        _target = _target == null ? transform : _target;

        _baseScale = _target.localScale;
        _basePosition = _target.position;
        _baseRotation = _target.rotation;

        Debug.Log(name);

        _pointContainer.GetComponentsInChildren(_points);
        if (_points.Count > 1 && _points.Contains(_pointContainer)) _points.Remove(_pointContainer);
        _points.Sort(new RandomComparer());
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    [ContextMenu(nameof(Trigger))]
    public override void Trigger()
    {
        base.Trigger();

        Transform lPoint = _points[0];
        _points.Remove(lPoint);
        _points.Add(lPoint);

        _animator.StopAndClear();
        _animator.Scale(_target, lPoint.localScale, 1f, EaseType.InOutExpo);
        _animator.Position(_target, lPoint.position, 1f, EaseType.InOutExpo);
        _animator.Rotation(_target, lPoint.rotation, 1f, EaseType.InOutExpo);
        _animator.Start();
    }

    [ContextMenu(nameof(Fix))]
    public override void Fix()
    {
        base.Fix();

        _animator.StopAndClear();

        _target.localScale = _baseScale;
        _target.position = _basePosition;
        _target.rotation = _baseRotation;
    }
}
