// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 04 / 2025 #======~~~~-- //

using System;
using UnityEngine;
using UnBocal.TweeningSystem.Interpolations;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        public Interpolation Scale(Transform pTransform, Vector3 pTargetScale, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f)
            => Scale(pTransform, pTransform.localScale, pTargetScale, pDuration, EaseFunction.GetFunction(pEasing), pDelay);

        public Interpolation Scale(Transform pTransform, Vector3 pTargetScale, float pDuration, AnimationCurve pCurve, float pDelay = 0f)
            => Scale(pTransform, pTransform.localScale, pTargetScale, pDuration, pCurve.Evaluate, pDelay);

        public Interpolation Scale(Transform pTransform, Vector3 pStartScale, Vector3 pEndScale, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f)
            => Scale(pTransform, pStartScale, pEndScale, pDuration, EaseFunction.GetFunction(pEasing), pDelay);

        public Interpolation Scale(Transform pTransform, Vector3 pStartScale, Vector3 pEndScale, float pDuration, AnimationCurve pCurve, float pDelay = 0f)
            => Scale(pTransform, pStartScale, pEndScale, pDuration, pCurve.Evaluate, pDelay);

        public Interpolation Scale(Transform pTransform, float pTargetScale, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f)
            => Scale(pTransform, pTransform.localScale, Vector3.one * pTargetScale, pDuration, EaseFunction.GetFunction(pEasing), pDelay);

        public Interpolation Scale(Transform pTransform, float pTargetScale, float pDuration, AnimationCurve pCurve, float pDelay = 0f)
            => Scale(pTransform, pTransform.localScale, Vector3.one * pTargetScale, pDuration, pCurve.Evaluate, pDelay);

        public Interpolation Scale(Transform pTransform, float pStartScale, float pEndScale, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f)
            => Scale(pTransform, Vector3.one * pStartScale, Vector3.one * pEndScale, pDuration, EaseFunction.GetFunction(pEasing), pDelay);

        public Interpolation Scale(Transform pTransform, float pStartScale, float pEndScale, float pDuration, AnimationCurve pCurve, float pDelay = 0f)
            => Scale(pTransform, Vector3.one * pStartScale, Vector3.one * pEndScale, pDuration, pCurve.Evaluate, pDelay);

        private Interpolation Scale(Transform pTransform, Vector3 pStartScale, Vector3 pEndScale, float pDuration, Func<float, float> pEasing, float pDelay)
        {
            Action<float> lInterpolationMethod = null;

            if (pStartScale != pEndScale)
            lInterpolationMethod = (float pRatio) => pTransform.localScale = Vector3.LerpUnclamped(pStartScale, pEndScale, pEasing(pRatio));

            return AddInterpolation(pTransform, nameof(pTransform.localScale), lInterpolationMethod, pDuration, pDelay);
        }
    }
}