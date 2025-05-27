using System.Collections;
using System.Collections.Generic;
using UnBocal.TweeningSystem;
using UnityEngine;

public class TweenTesting : MonoBehaviour
{
    Tween _animator = new Tween();
    void Start()
    {
        _animator.ShakeScale(transform, .5f, 1f, EaseType.InExpo, pGetBackToTarget : false);
        _animator.ShakeScale(transform, .5f, 1f, EaseType.Flat, 1f, pGetBackToTarget: false);
        _animator.ShakeScale(transform, .5f, 1f, EaseType.OutExpo, 2f, pGetBackToTarget: false);
        _animator.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
