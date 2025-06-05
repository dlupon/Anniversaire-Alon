// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 04 / 2025 #======~~~~-- //

using UnityEngine;
using UnBocal.TweeningSystem.Interpolations;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Material
        public Interpolation Material(Material pMaterial, Material pTargetMaterial, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f)
            => Material(pMaterial, pMaterial, pTargetMaterial, pDuration, EaseFunction.GetFunction(pEasing), pDelay);

        public Interpolation Material(Material pMaterial, Material pEndMaterial, float pDuration, AnimationCurve pCurve, float pDelay = 0f)
            => Material(pMaterial, pMaterial, pEndMaterial, pDuration, pCurve.Evaluate, pDelay);

        public Interpolation Material(Material pMaterial, Material pStartMaterial, Material pEndMaterial, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f)
            => Material(pMaterial, pStartMaterial, pEndMaterial, pDuration, EaseFunction.GetFunction(pEasing), pDelay);

        public Interpolation Material(Material pMaterial, Material pStartMaterial, Material pEndMaterial, float pDuration, AnimationCurve pCurve, float pDelay = 0f)
            => Material(pMaterial, pStartMaterial, pEndMaterial, pDuration, pCurve.Evaluate, pDelay);

        private Interpolation Material(Material  pMaterial, Material pStartMaterial, Material pEndMaterial, float pDuration, System.Func<float, float> pEasing, float pDelay = 0f)
        {
            System.Action<float> lInterpolationMethod = null;

            if (pStartMaterial != pEndMaterial)
            {
                pStartMaterial = new Material(pStartMaterial);
                pEndMaterial = new Material(pEndMaterial);

                lInterpolationMethod = (float pRatio) => pMaterial.Lerp(pStartMaterial, pEndMaterial, pEasing(pRatio));
            }

            return AddInterpolation(pMaterial, nameof(Color), lInterpolationMethod, pDuration, pDelay);
        }
    }
}