using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;

public class LetterFade : Letter
{
    [SerializeField] private TextMeshProUGUI _wordFade;
    [SerializeField] private Color _inColor = new Color(0, 0, 0, 1);
    [SerializeField] private Color _outColor = Color.white;
    [SerializeField] private float _delay = 5f;
    [SerializeField] private float _duration = 10f;
    
    private Tween _animator = new Tween();

    public override void Show()
    {
        base.Show();

        _animator.CompleteAndClear();
        _animator.Color(_wordFade, _inColor, _outColor, _duration, pDelay: _delay).Apply();
        _animator.Start();
    }

    public override void Hide()
    {
        base.Hide();

        _animator.CompleteAndClear();
    }
}