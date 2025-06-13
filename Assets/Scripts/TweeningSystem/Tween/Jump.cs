// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 04 / 2025 #======~~~~-- //

using System;
using UnityEngine;
using UnBocal.TweeningSystem.Interpolations;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        // -------~~~~~~~~~~================# // Only Height
        public Interpolation Jump(Transform pTransform, float pHeight = 1f, float pDuration = 1, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => Jump(pTransform, Vector3.up * pHeight, pDuration, EaseFunction.GetJumpFunction(pEasing), pDelay, pRef);

        public Interpolation Jump(Transform pTransform, float pHeight, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => Jump(pTransform, Vector3.up * pHeight, pDuration, pCurve.Evaluate, pDelay, pRef);

        private Interpolation Jump(Transform pTransform, Vector3 pHeight, float pDuration, Func<float, float> pEasing, float pDelay, Ref pRef)
        {
            Action<float> lInterpolationMethod = null;

            if (pHeight.magnitude > 0)
            {
                Vector3 lBasePosition = GetPosition(pTransform, pRef);

                switch (pRef)
                {
                    case Ref.Global: lInterpolationMethod = (float pRatio) => pTransform.position = lBasePosition + pHeight * pEasing(pRatio); break;
                    case Ref.Local: lInterpolationMethod = (float pRatio) => pTransform.localPosition = lBasePosition + pHeight * pEasing(pRatio); break; ;
                }
            }

            Debug.Log(lInterpolationMethod == null);

            return AddInterpolation(pTransform, nameof(pTransform.position), lInterpolationMethod, pDuration, pDelay);
        }

        // -------~~~~~~~~~~================# // With Motion
        public Interpolation Jump(Transform pTransform, Vector3 pTargetPosition, float pHeight = 1f, float pDuration = 1, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => Jump(pTransform, GetPosition(pTransform, pRef), pTargetPosition, Vector3.up * pHeight, pDuration, EaseFunction.GetJumpFunction(pEasing), pDelay, pRef);

        public Interpolation Jump(Transform pTransform, Vector3 pTargetPosition, float pHeight, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => Jump(pTransform, GetPosition(pTransform, pRef), pTargetPosition, Vector3.up * pHeight, pDuration, pCurve.Evaluate, pDelay, pRef);

        private Interpolation Jump(Transform pTransform, Vector3 pStartPosition, Vector3 pEndPosition, Vector3 pHeight, float pDuration, Func<float, float> pEasing, float pDelay, Ref pRef)
        {
            Action<float> lInterpolationMethod = null;

            if (!(pStartPosition == pEndPosition && pHeight.magnitude <= 0))
            {

                pHeight = pRef == Ref.Global ? pHeight : pTransform.rotation * pHeight;

                switch (pRef)
                {
                    case Ref.Global: lInterpolationMethod = (float pRatio) => pTransform.position = Vector3.LerpUnclamped(pStartPosition, pEndPosition, pEasing(pRatio)) + pHeight * pEasing(pRatio); break;
                    case Ref.Local: lInterpolationMethod = (float pRatio) => pTransform.localPosition = Vector3.LerpUnclamped(pStartPosition, pEndPosition, pEasing(pRatio)) + pHeight * pEasing(pRatio); break; ;
                }
            }

            return AddInterpolation(pTransform, nameof(pTransform.position), lInterpolationMethod, pDuration, pDelay);
        }
    }
}