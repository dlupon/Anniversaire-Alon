using UnBocal.TweeningSystem;
using UnityEngine;

public class LetterScaleSpinning : Letter
{
    [SerializeField] private Transform _image;
    
    private Tween _animator = new Tween();

    public override void Show()
    {
        base.Show();

        _animator.Scale(_image, 0, 1, 10f);
        _animator.ShakePosition(_image, 130f, 10f, EaseType.OutFlat);
        _animator.RotationAngleAxis(_image, 360f * 3f, Random.onUnitSphere, 10f);
        _animator.Start();
    }

    public override void Hide()
    {
        base.Hide();

        _animator.CompleteAndClear();
    }
}