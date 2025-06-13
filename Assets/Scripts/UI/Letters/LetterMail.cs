using System.Collections.Generic;
using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;

public class LetterMail : Letter
{
    [SerializeField] private Transform _mail;

    [SerializeField] private TextMeshProUGUI _mailText;
    [SerializeField] private TextMeshProUGUI _objetText;
    [SerializeField] private TextMeshProUGUI _whoText;

    [SerializeField] private RectTransform _cursor;
    [SerializeField] private RectTransform _cursorPointContainer;
    private List<Transform> _points = new List<Transform>();

    
    private Tween _animator = new Tween();

    protected override void Start()
    {
        base.Start();

        _cursorPointContainer.GetComponentsInChildren(_points);
        if (_points.Contains(_cursorPointContainer)) _points.Remove(_cursorPointContainer);
    }

    public override void Show()
    {
        base.Show();

        Vector3 lCurrentPosition;

        _animator.CompleteAndClear();
        _animator.Position(_mail, _mail.position + Vector3.down * 1000, _mail.position, 1, EaseType.OutBack);
        _animator.Position(_cursor, lCurrentPosition = GetNextPosition(), .5f, EaseType.InOutCirc, 1f);
        _animator.Whrite(_whoText, 1.3f, pDelay: 1.5f).Apply();
        _animator.Position(_cursor, lCurrentPosition, lCurrentPosition = GetNextPosition(), 1f, EaseType.InOutCirc, 2.8f);
        _animator.Whrite(_mailText, 7.5f, pDelay: 4.7f).Apply();
        _animator.Position(_cursor, lCurrentPosition, lCurrentPosition = GetNextPosition(), 1f, EaseType.InOutCirc, 12.5f);
        _animator.Whrite(_objetText, .7f, pDelay: 13.4f).Apply();
        _animator.Position(_cursor, lCurrentPosition, lCurrentPosition = GetNextPosition(), 1f, EaseType.InOutCirc, 14f);
        _animator.Start();
    }

    private Vector3 GetNextPosition()
    {
        Transform lCurrentTransform = _points[0];

        _points.Remove(lCurrentTransform);
        _points.Add(lCurrentTransform);

        return lCurrentTransform.position;
    }

    public override void Hide()
    {
        base.Hide();

        _animator.CompleteAndClear();
    }
}