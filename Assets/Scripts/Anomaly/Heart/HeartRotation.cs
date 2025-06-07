using UnBocal.TweeningSystem;
using UnityEngine;

public class HeartRotation : Heart
{
    [SerializeField] private float _rotationDuration = 1f;

    [ContextMenu(nameof(Trigger))]
    public override void Trigger()
    {
        base.Trigger();

        m_animator.Clear();
        m_animator.RotationAngleAxis(m_target, 360f, Vector3.up, _rotationDuration, EaseType.InOutBack).OnFinished += m_animator.Start;
        m_animator.Start();
    }
}
