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
        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Color
        private Color GetColor(Object pTarget)
        => pTarget switch
        {
            Material lM => lM.color,
            SpriteRenderer lM => lM.color,
            RawImage lM => lM.color,
            Image lM => lM.color,
            Text lM => lM.color,
            TextMesh lM => lM.color,
            Camera lM => lM.backgroundColor,
            _ => UnityEngine.Color.white,
        };

        public Interpolation Color(Object pTarget, Color pTargetColor, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f)
            => Color(pTarget, GetColor(pTarget), pTargetColor, pDuration, EaseFunction.GetFunction(pEasing), pDelay);

        public Interpolation Color(Object pTarget, Color pEndColor, float pDuration, AnimationCurve pCurve, float pDelay = 0f)
            => Color(pTarget, GetColor(pTarget), pEndColor, pDuration, pCurve.Evaluate, pDelay);

        public Interpolation Color(Object pTarget, Color pStartColor, Color pEndColor, float pDuration, EaseType pEasing = EaseType.Flat, float pDelay = 0f)
            => Color(pTarget, pStartColor, pEndColor, pDuration, EaseFunction.GetFunction(pEasing), pDelay);

        public Interpolation Color(Object pTarget, Color pStartColor, Color pEndColor, float pDuration, AnimationCurve pCurve, float pDelay = 0f)
            => Color(pTarget, pStartColor, pEndColor, pDuration, pCurve.Evaluate, pDelay);

        private Interpolation Color(Object pTarget, Color pStartColor, Color pEndColor, float pDuration, System.Func<float, float> pEasing, float pDelay = 0f)
        {

            System.Action<float> lInterpolationMethod = null;
            System.Func<float, Color> lInterpolation = (float pRatio) => UnityEngine.Color.LerpUnclamped(pStartColor, pEndColor, pEasing(pRatio));

            if (pStartColor != pEndColor)
            {
                switch (pTarget)
                {
                    default: return Error.InterpolationNotSupported(pTarget, nameof(Color));

                    // Material
                    case Material lM: lInterpolationMethod = (float pRatio) => lM.color = lInterpolation(pRatio); break;

                    // Images
                    case SpriteRenderer lS: lInterpolationMethod = (float pRatio) => lS.color = lInterpolation(pRatio); break;
                    case RawImage lRI: lInterpolationMethod = (float pRatio) => lRI.color = lInterpolation(pRatio); break;
                    case Image lI: lInterpolationMethod = (float pRatio) => lI.color = lInterpolation(pRatio); break;

                    // Text
                    case Text lT: lInterpolationMethod = (float pRatio) => lT.color = lInterpolation(pRatio); break;
                    case TextMesh lTM: lInterpolationMethod = (float pRatio) => lTM.color = lInterpolation(pRatio); break;
                    case TextMeshPro lTM: lInterpolationMethod = (float pRatio) => lTM.color = lInterpolation(pRatio); break;
                    case TextMeshProUGUI lTM: lInterpolationMethod = (float pRatio) => lTM.color = lInterpolation(pRatio); break;

                    // Camera
                    case Camera lC: lInterpolationMethod = (float pRatio) => lC.backgroundColor = lInterpolation(pRatio); break;
                }
            }

            return AddInterpolation(pTarget, nameof(Color), lInterpolationMethod, pDuration, pDelay);
        }
    }
}