// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 04 / 2025 #======~~~~-- //

using UnityEngine;
using System;
using UnBocal.TweeningSystem.Interpolations;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        public Interpolation Interpolate<Type>(object pContainer, Action<Type> pSetter, object pStartValue, object pEndValue, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f)
            => Interpolate(pContainer, pSetter, pStartValue, pEndValue, pDuration, EaseFunction.GetFunction(pEasing), pDelay);

        public Interpolation Interpolate<Type>(object pContainer, Action<Type> pSetter, object pStartValue, object pEndValue, float pDuration, AnimationCurve pCurve, float pDelay = 0f)
           => Interpolate(pContainer, pSetter, pStartValue, pEndValue, pDuration, pCurve.Evaluate, pDelay);

        private Interpolation Interpolate<Type>(object pContainer, Action<Type> pSetter, object pStartValue, object pEndValue, float pDuration, Func<float, float> pEasing, float pDelay)
        {
            Action<float> lInterpolationMethod = null;

            switch (pSetter)
            {
                case Action<int> lInt: lInterpolationMethod = (float pRatio) => lInt((int)Mathf.LerpUnclamped((int)pStartValue, (int)pEndValue, pEasing(pRatio))); break;
                case Action<float> lFloat: lInterpolationMethod = (float pRatio) => lFloat(Mathf.LerpUnclamped((float)pStartValue, (float)pEndValue, pEasing(pRatio))); break;
                case Action<Color> lColor: lInterpolationMethod = (float pRatio) => lColor(UnityEngine.Color.LerpUnclamped((Color)pStartValue, (Color)pEndValue, pEasing(pRatio))); break;
                default: return Error.InterpolationNotSupported(pStartValue, typeof(Type).Name);
            }

            return AddInterpolation(pContainer, typeof(Type).Name, lInterpolationMethod, pDuration, pDelay);
        }
    }
}