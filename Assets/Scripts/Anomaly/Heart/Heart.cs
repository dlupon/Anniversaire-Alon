using UnBocal.TweeningSystem;
using UnityEngine;

public class Heart : Anomaly
{
    [Header("Target")]
    [SerializeField] protected Transform m_target = null;
    private Vector3 _baseScale;

    [Header("Animations")]
    [SerializeField] private float _spawnDuration = 1f;
    protected Tween m_animator = new Tween();

    protected override void Start()
    {
        base.Start();
        Type = nameof(Heart);

        m_target = m_target == null ? transform : m_target;
        _baseScale = m_target.localScale;
        m_target.localScale = Vector3.zero;
    }

    public override void Trigger()
    {
        base.Trigger();

        m_animator.Scale(m_target, new Vector3(1.5f * _baseScale.x, 0 * _baseScale.y, 1.5f * _baseScale.z), _baseScale, _spawnDuration, EaseType.OutBack);
        m_animator.Start();
    }

    public override void Fix()
    {
        base.Fix();
        PlayerPrefs.SetInt(nameof(Heart), PlayerPrefs.GetInt(nameof(Heart), 0) + 1);
        Tween.KillAndClear(m_target);
        m_animator.CompleteAndClear();
        m_target.localScale = Vector3.zero;
    }
}
