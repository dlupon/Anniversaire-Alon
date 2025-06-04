using UnBocal.TweeningSystem;
using UnityEngine;

public class Ama : Letter
{
    [SerializeField] private Transform _image;
    
    private Tween _animator = new Tween();

    public override void Show()
    {
        gameObject.SetActive(true);

        _animator.Scale(_image, 0, 1, 10f);
        _animator.ShakePosition(_image, 130f, 10f, EaseType.OutFlat);
        _animator.RotationAngleAxis(_image, 360f * 3f, Vector3.up, 10f);
        _animator.Start();
    }

    public override void Hide()
    {
        // _animator.Rotatio(transform, 0, 1, 10f);
        _animator.CompleteAndClear();
        gameObject.SetActive(false);
    }
}