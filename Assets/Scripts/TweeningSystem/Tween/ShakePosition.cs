// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 04 / 2025 #======~~~~-- //

using System;
using UnityEngine;
using UnBocal.TweeningSystem.Interpolations;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        public Interpolation ShakePosition(Transform pTransform, float pDistance = 1f, float pDuration = 1f, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global, bool pGetBackToTarget = true)
            => ShakePosition(pTransform, GetPosition(pTransform, pRef), pDistance, pDuration, EaseFunction.GetShakeFunction(pEasing), pDelay, pRef, pGetBackToTarget);

        public Interpolation ShakePosition(Transform pTransform, float pDistance, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global, bool pGetBackToTarget = true)
            => ShakePosition(pTransform, GetPosition(pTransform, pRef), pDistance, pDuration, pCurve.Evaluate, pDelay, pRef, pGetBackToTarget);

        public Interpolation ShakePosition(Transform pTransform, Vector3 pTargetPosition, float pDistance = 1f, float pDuration = 1f, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global, bool pGetBackToTarget = true)
            => ShakePosition(pTransform, pTargetPosition, pDistance, pDuration, EaseFunction.GetShakeFunction(pEasing), pDelay, pRef, pGetBackToTarget);

        public Interpolation ShakePosition(Transform pTransform, Vector3 pTargetPosition, float pDistance, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global, bool pGetBackToTarget = true)
            => ShakePosition(pTransform, pTargetPosition, pDistance, pDuration, pCurve.Evaluate, pDelay, pRef, pGetBackToTarget);

        private Interpolation ShakePosition(Transform pTransform, Vector3 pTargetPosition, float pDistance, float pDuration, Func<float, float> pEasing, float pDelay, Ref pRef, bool pGetBackToTarget = true)
        {
            Action<float> lInterpolationMethod = null;

            if (pDistance != 0f)
            {
                switch (pRef)
                {
                    case Ref.Global: lInterpolationMethod = (float pRatio) => pTransform.position = Lerp.ShakeVector(pTargetPosition, pDistance, pRatio, pEasing, pGetBackToTarget); break;
                    case Ref.Local: lInterpolationMethod = (float pRatio) => pTransform.localPosition = Lerp.ShakeVector(pTargetPosition, pDistance, pRatio, pEasing, pGetBackToTarget); break; ;
                }
            }

            return AddInterpolation(pTransform, nameof(pTransform.position), lInterpolationMethod, pDuration, pDelay);
        }
    }
}