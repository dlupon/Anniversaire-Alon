// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 04 / 2025 #======~~~~-- //

using System;
using UnityEngine;
using UnBocal.TweeningSystem.Interpolations;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        public Interpolation ShakeScale(Transform pTransform, float pOffset = 1f, float pDuration = 1f, EaseType pEasing = EaseType.Flat, float pDelay = 0f, bool pGetBackToTarget = true)
            => ShakeScale(pTransform, pTransform.localScale, pOffset, pDuration, EaseFunction.GetShakeFunction(pEasing), pDelay, pGetBackToTarget);

        public Interpolation ShakeScale(Transform pTransform, float pOffset, float pDuration, AnimationCurve pCurve, float pDelay = 0f, bool pGetBackToTarget = true)
            => ShakeScale(pTransform, pTransform.localScale, pOffset, pDuration, pCurve.Evaluate, pDelay, pGetBackToTarget);

        public Interpolation ShakeScale(Transform pTransform, Vector3 pTargetScale, float pOffset = 1f, float pDuration = 1f, EaseType pEasing = EaseType.Flat, float pDelay = 0f, bool pGetBackToTarget = true)
            => ShakeScale(pTransform, pTargetScale, pOffset, pDuration, EaseFunction.GetShakeFunction(pEasing), pDelay, pGetBackToTarget);

        public Interpolation ShakeScale(Transform pTransform, Vector3 pTargetScale, float pOffset, float pDuration, AnimationCurve pCurve, float pDelay = 0f, bool pGetBackToTarget = true)
            => ShakeScale(pTransform, pTargetScale, pOffset, pDuration, pCurve.Evaluate, pDelay, pGetBackToTarget);

        private Interpolation ShakeScale(Transform pTransform, Vector3 pTargetScale, float pOffset, float pDuration, Func<float, float> pEasing, float pDelay, bool pGetBackToTarget = true)
        {
            Action<float> lInterpolationMethod = lInterpolationMethod = (float pRatio) => pTransform.localScale = Lerp.ShakeVector(pTargetScale, pOffset, pRatio, pEasing, pGetBackToTarget); ;

            return AddInterpolation(pTransform, nameof(pTransform.position), lInterpolationMethod, pDuration, pDelay);
        }
    }
}