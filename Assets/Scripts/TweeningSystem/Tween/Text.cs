// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 04 / 2025 #======~~~~-- //

using UnityEngine;
using UnBocal.TweeningSystem.Interpolations;
using UnityEngine.UI;
using TMPro;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        private string GetText(Object pTarget)
        => pTarget switch
        { 
            Text lText => lText.text,
            TextMesh lTextMesh => lTextMesh.text,
            TMP_Text TMP_Text => TMP_Text.text,
            _ => "" };

        public Interpolation Whrite(Object pTarget, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => Whrite(pTarget, GetText(pTarget), pDuration, EaseFunction.GetFunction(pEasing), pDelay, pRef);

        public Interpolation Whrite(Object pTarget, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => Whrite(pTarget, GetText(pTarget), pDuration, pCurve.Evaluate, pDelay, pRef);

        public Interpolation Whrite(Object pTarget, string pText, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f, Ref pRef = Ref.Global)
            => Whrite(pTarget, pText, pDuration, EaseFunction.GetFunction(pEasing), pDelay, pRef);

        public Interpolation Whrite(Object pTarget, string pText, float pDuration, AnimationCurve pCurve, float pDelay = 0f, Ref pRef = Ref.Global)
            => Whrite(pTarget, pText, pDuration, pCurve.Evaluate, pDelay, pRef);

        private Interpolation Whrite(Object pTarget, string pText, float pDuration, System.Func<float, float> pEasing, float pDelay, Ref pRef)
        {
            System.Action<float> lInterpolationMethod = null;

            switch (pTarget)
            {
                case Text lT: lInterpolationMethod  = (float pRatio) => lT.text = Lerp.Whrite(pText, pEasing(pRatio)); break;
                case TextMesh lTM: lInterpolationMethod  = (float pRatio) => lTM.text = Lerp.Whrite(pText, pEasing(pRatio)); break;
                case TextMeshPro lTMP: lInterpolationMethod  = (float pRatio) => lTMP.text = Lerp.Whrite(pText,pEasing(pRatio)); break;
                case TextMeshProUGUI lTMPGUI: lInterpolationMethod  = (float pRatio) => lTMPGUI.text = Lerp.Whrite(pText, pEasing(pRatio)); break;

                default: return Error.InterpolationNotSupported(pTarget, nameof(Whrite));
            }

            return AddInterpolation(pTarget, nameof(Whrite), lInterpolationMethod, pDuration, pDelay);
        }
    }
}