using System.Linq;
using System.Reflection;
using UnBocal.TweeningSystem;
using UnityEngine;

public class NenouilleJumpScare : Anomaly
{
    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _distanceFromCamera = 1f;
    [SerializeField] private float _duration = .2f;
    private Room _currentRoom;

    // -------~~~~~~~~~~================# // Animation
    private Tween _animator = new Tween();
    private Vector3 _baseScale;
    private Vector3 _basePosition;
    private Quaternion _baseRotation;

    protected override void Start()
    {
        base.Start();
        Type = $"Nenouille ?";

        _target = _target == null ? transform : _target;

        _baseScale = _target.localScale;
        _basePosition = _target.position;
        _baseRotation = _target.rotation;

        _target.localScale = Vector3.zero;
    }

    private void OnDestroy()
    {
        EventBus.RoomChanged -= UpdateRoom;
        EventBus.ReportAnomaly -= JumpScare;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public void UpdateRoom(Room pRoom) => _currentRoom = pRoom;

    public void JumpScare(string pAnomaly)
    {
        if (!((_currentRoom == null || _currentRoom.name == Room) && pAnomaly == Type)) return;

        Transform lCamera = Camera.main.transform;

        _animator.Position(_target, lCamera.position + lCamera.forward * _distanceFromCamera, _duration, EaseType.OutExpo);
        _animator.Rotation(_target, Quaternion.FromToRotation(_target.forward, -lCamera.forward) * _target.rotation, _duration, EaseType.OutExpo);
        _animator.Play();
    }

    [ContextMenu(nameof(Trigger))]
    public override void Trigger()
    {
        base.Trigger();
        _animator.StopAndClear();
        _animator.Scale(_target, _baseScale, 1, EaseType.OutBack);
        _animator.Start();

        EventBus.RoomChanged += UpdateRoom;
        EventBus.ReportAnomaly += JumpScare;
    }

    [ContextMenu(nameof(Fix))]
    public override void Fix()
    {
        base.Fix();
        Tween.KillAndClear(_target);
        _animator.StopAndClear();
        
        _target.localScale = Vector3.zero;
        _target.position = _basePosition;
        _target.rotation = _baseRotation;

        EventBus.RoomChanged -= UpdateRoom;
        EventBus.ReportAnomaly -= JumpScare;
    }
}
