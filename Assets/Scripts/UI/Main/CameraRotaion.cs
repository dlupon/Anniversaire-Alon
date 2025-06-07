using System.Collections;
using System.Collections.Generic;
using UnBocal.TweeningSystem;
using UnityEngine;

public class CameraRotaion : MonoBehaviour
{
    private void Start()
    {
        Tween lAnimator = new Tween();
        lAnimator.RotationAngleAxis(transform, 45f, Vector3.up, 10f, EaseType.InOutSin);
        lAnimator.RotationAngleAxis(transform, transform.rotation * Quaternion.AngleAxis(45f, Vector3.up), -45f, Vector3.up, 10f, EaseType.InOutSin, 10f).OnFinished += lAnimator.Start;
        lAnimator.Start();
    }
}
