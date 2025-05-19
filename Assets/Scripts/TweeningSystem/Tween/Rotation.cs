// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 04 / 2025 #======~~~~-- //

using System;
using UnityEngine;
using UnBocal.TweeningSystem.Interpolations;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        private Quaternion GetRotation(Transform pTransform, Ref pRef) => pRef switch { Ref.Global => pTransform.rotation, Ref.Local => pTransform.localRotation, _ => pTransform.rotation};

        // -------~~~~~~~~~~================# // Global
        public Interpolation Rotation(Transform pTransform, Quaternion pTargetRotation, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => Rotation(pTransform, GetRotation(pTransform, pRef), pTargetRotation, pDuration, EaseFunction.GetFunction(pEasing), pDelay, pRef);

        public Interpolation Rotation(Transform pTransform, Quaternion pTargetRotation, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => Rotation(pTransform, GetRotation(pTransform, pRef), pTargetRotation, pDuration, pCurve.Evaluate, pDelay, pRef);

        public Interpolation Rotation(Transform pTransform, Quaternion pStartRotation, Quaternion pEndRotation, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => Rotation(pTransform, pStartRotation, pEndRotation, pDuration, EaseFunction.GetFunction(pEasing), pDelay, pRef);

        public Interpolation Rotation(Transform pTransform, Quaternion pStartRotation, Quaternion pEndRotation, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => Rotation(pTransform, pStartRotation, pEndRotation, pDuration, pCurve.Evaluate, pDelay, pRef);

        private Interpolation Rotation(Transform pTransform, Quaternion pStartRotation, Quaternion pEndRotation, float pDuration, Func<float, float> pEasing, float pDelay, Ref pRef)
        {
            if (pStartRotation == pEndRotation) return null;

            Action<float> lInterpolationMethod = null;

            switch (pRef)
            {
                case Ref.Global: lInterpolationMethod = (float pRatio) => pTransform.rotation = Quaternion.LerpUnclamped(pStartRotation, pEndRotation, pEasing(pRatio)); break;

                case Ref.Local: lInterpolationMethod = (float pRatio) => pTransform.rotation = Quaternion.LerpUnclamped(pStartRotation, pEndRotation, pEasing(pRatio)); break;
            }

            return AddInterpolation(pTransform, nameof(pTransform.rotation), lInterpolationMethod, pDuration, pDelay);
        }

        // -------~~~~~~~~~~================# // Global Angle Axis
        public Interpolation RotationAngleAxis(Transform pTransform, float pAngle, Vector3 pAxis, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => RotationAngleAxis(pTransform, GetRotation(pTransform, pRef), pAngle, pAxis, pDuration, EaseFunction.GetFunction(pEasing), pDelay, pRef);

        public Interpolation RotationAngleAxis(Transform pTransform, float pAngle, Vector3 pAxis, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => RotationAngleAxis(pTransform, GetRotation(pTransform, pRef), pAngle, pAxis, pDuration, pCurve.Evaluate, pDelay, pRef);

        public Interpolation RotationAngleAxis(Transform pTransform, Quaternion pStartRotation, float pAngle, Vector3 pAxis, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => RotationAngleAxis(pTransform, pStartRotation, pAngle, pAxis, pDuration, EaseFunction.GetFunction(pEasing), pDelay, pRef);

        public Interpolation RotationAngleAxis(Transform pTransform, Quaternion pStartRotation, float pAngle, Vector3 pAxis, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => RotationAngleAxis(pTransform, pStartRotation, pAngle, pAxis, pDuration, pCurve.Evaluate, pDelay, pRef);

        public Interpolation RotationAngleAxis(Transform pTransform, float pStartAngle, float pEndAngle, Vector3 pAxis, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => RotationAngleAxis(pTransform, Quaternion.AngleAxis(pStartAngle, pAxis), pEndAngle, pAxis, pDuration, EaseFunction.GetFunction(pEasing), pDelay, pRef);

        public Interpolation RotationAngleAxis(Transform pTransform, float pStartAngle, float pAngle, Vector3 pAxis, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => RotationAngleAxis(pTransform, Quaternion.AngleAxis(pStartAngle, pAxis), pAngle, pAxis, pDuration, pCurve.Evaluate, pDelay, pRef);

        private Interpolation RotationAngleAxis(Transform pTransform, Quaternion pStartRotation, float pAngle, Vector3 pAxis, float pDuration, Func<float, float> pEasing, float pDelay, Ref pRef)
        {
            if (pAngle == 0) return null;

            Action<float> lInterpolationMethod = null;

            switch (pRef)
            {
                case Ref.Global: lInterpolationMethod = (float pRatio) => pTransform.rotation = pStartRotation * Quaternion.AngleAxis(pAngle * pEasing(pRatio), pAxis); break;

                case Ref.Local: lInterpolationMethod = (float pRatio) => pTransform.localRotation = pStartRotation * Quaternion.AngleAxis(pAngle * pEasing(pRatio), pAxis); break;
            }

            return AddInterpolation(pTransform, nameof(pTransform.rotation), lInterpolationMethod, pDuration, pDelay);
        }
    }
}