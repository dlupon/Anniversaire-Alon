using UnBocal.TweeningSystem;
using UnityEngine;

public class LetterJumping : Letter
{
    [SerializeField] private Transform _image;
    
    private Tween _animator = new Tween();

    public override void Show()
    {
        base.Show();


        _animator.Jump(_image, 500, 1.5f, EaseType.OutBounce);
        _animator.Scale(_image, 0, 1, 1.5f, EaseType.OutBounce);
        _animator.RotationAngleAxis(_image, 360f, Vector2.right, 1.5f, EaseType.OutBack);
        _animator.Start();
    }

    public override void Hide()
    {
        base.Hide();

        _animator.CompleteAndClear();
    }
}