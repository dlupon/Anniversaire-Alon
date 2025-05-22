// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 04 / 2025 #======~~~~-- //

using System;
using UnityEngine;
using UnBocal.TweeningSystem.Interpolations;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        // -------~~~~~~~~~~================# // Position
        private Vector3 GetPosition(Transform pTransform, Ref pRef) => pRef switch { Ref.Global => pTransform.position, Ref.Local => pTransform.localPosition, _ => pTransform.position };

        public Interpolation Position(Transform pTransform, Vector3 pTargetPosition, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => Position(pTransform, GetPosition(pTransform, pRef), pTargetPosition, pDuration, EaseFunction.GetFunction(pEasing), pDelay, pRef);

        public Interpolation Position(Transform pTransform, Vector3 pTargetPosition, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => Position(pTransform, GetPosition(pTransform, pRef), pTargetPosition, pDuration, pCurve.Evaluate, pDelay, pRef);

        public Interpolation Position(Transform pTransform, Vector3 pStartPosition, Vector3 pEndPosition, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => Position(pTransform, pStartPosition, pEndPosition, pDuration, EaseFunction.GetFunction(pEasing), pDelay, pRef);

        public Interpolation Position(Transform pTransform, Vector3 pStartPosition, Vector3 pEndPosition, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => Position(pTransform, pStartPosition, pEndPosition, pDuration, pCurve.Evaluate, pDelay, pRef);

        private Interpolation Position(Transform pTransform, Vector3 pStartPosition, Vector3 pEndPosition, float pDuration, Func<float, float> pEasing, float pDelay, Ref pRef)
        {
            Action<float> lInterpolationMethod = null;

            if (pStartPosition == pEndPosition)
            {
                switch (pRef)
                {
                    case Ref.Global: lInterpolationMethod = (float pRatio) => pTransform.position = Vector3.LerpUnclamped(pStartPosition, pEndPosition, pEasing(pRatio)); break;
                    case Ref.Local: lInterpolationMethod = (float pRatio) => pTransform.localPosition = Vector3.LerpUnclamped(pStartPosition, pEndPosition, pEasing(pRatio)); break; ;
                }
            }

            return AddInterpolation(pTransform, nameof(pTransform.position), lInterpolationMethod, pDuration, pDelay);
        }
    }
}