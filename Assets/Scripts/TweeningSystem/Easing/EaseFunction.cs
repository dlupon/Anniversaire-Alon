// --~~~~======# Author : Gallot Valentin #======~~~~~~--- //
// --~~~~======# Date   : 03 / 05 / 2025  #======~~~~~~--- //

using System;
using UnityEngine;

namespace UnBocal.TweeningSystem
{
    public enum EaseType
    {
        Flat,
        InFlat,
        InSin,
        InCubic,
        InQuad,
        InQuart,
        InQuint,
        InCirc,
        InElastic,
        InBack,
        InBounce,
        InExpo,
        OutFlat,
        OutSin,
        OutCubic,
        OutQuad,
        OutQuart,
        OutQuint,
        OutCirc,
        OutElastic,
        OutBack,
        OutBounce,
        OutExpo,
        InOutFlat,
        InOutSin,
        InOutCubic,
        InOutQuad,
        InOutQuart,
        InOutQuint,
        InOutCirc,
        InOutElastic,
        InOutBack,
        InOutBounce,
        InOutExpo,
    }

    public static class EaseFunction
    {
        private const float c1 = 1.70158f;
        private const float c2 = c1 * 1.525f;
        private const float c3 = c1 + 1;
        private const float c4 = 2 * Mathf.PI / 3;
        private const float c5 = 2 * Mathf.PI / 4.5f;
        private const float n1 = 7.5625f;
        private const float d1 = 2.75f;

        /// <summary>
        /// Eases <paramref name="pLi"/> according to <paramref name="pEaseType"/>'s easing function.
        /// Returns <paramref name="pLi"/> by default.
        /// </summary>
        /// <param name="pLi">Linear interpolation value, supposed between 0 and 1</param>
        /// <param name="pEaseType"></param>
        /// <returns>Eased value</returns>
        public static float Ease(float pLi, EaseType pEaseType = EaseType.Flat)
        {
            if (pLi < 0 || pLi > 1) Debug.LogWarning("interpolation value parameter outside [0;1] range");

            return pEaseType switch
            {
                EaseType.InSin => InSin(pLi),
                EaseType.InCubic => InCubic(pLi),
                EaseType.InQuad => InQuad(pLi),
                EaseType.InQuart => InQuart(pLi),
                EaseType.InQuint => InQuint(pLi),
                EaseType.InCirc => InCirc(pLi),
                EaseType.InElastic => InElastic(pLi),
                EaseType.InBack => InBack(pLi),
                EaseType.InBounce => InBounce(pLi),
                EaseType.InExpo => InExpo(pLi),
                EaseType.OutSin => OutSin(pLi),
                EaseType.OutCubic => OutCubic(pLi),
                EaseType.OutQuad => OutQuad(pLi),
                EaseType.OutQuart => OutQuart(pLi),
                EaseType.OutQuint => OutQuint(pLi),
                EaseType.OutCirc => OutCirc(pLi),
                EaseType.OutElastic => OutElastic(pLi),
                EaseType.OutBack => OutBack(pLi),
                EaseType.OutBounce => OutBounce(pLi),
                EaseType.OutExpo => OutExpo(pLi),
                EaseType.InOutSin => InOutSin(pLi),
                EaseType.InOutCubic => InOutCubic(pLi),
                EaseType.InOutQuad => InOutQuad(pLi),
                EaseType.InOutQuart => InOutQuart(pLi),
                EaseType.InOutQuint => InOutQuint(pLi),
                EaseType.InOutCirc => InOutCirc(pLi),
                EaseType.InOutElastic => InOutElastic(pLi),
                EaseType.InOutBack => InOutBack(pLi),
                EaseType.InOutBounce => InOutBounce(pLi),
                EaseType.InOutExpo => InOutExpo(pLi),
                _ => pLi,
            };
        }

        /// <summary>
        /// Returns <paramref name="pEaseType"/> Easing function.
        /// </summary>
        /// <param name="pEaseType"></param>
        /// <returns>Easing function</returns>
        public static Func<float, float> GetFunction(EaseType pEaseType = EaseType.Flat)
        {
            return pEaseType switch
            {
                EaseType.InSin => InSin,
                EaseType.InCubic => InCubic,
                EaseType.InQuad => InQuad,
                EaseType.InQuart => InQuart,
                EaseType.InQuint => InQuint,
                EaseType.InCirc => InCirc,
                EaseType.InElastic => InElastic,
                EaseType.InBack => InBack,
                EaseType.InBounce => InBounce,
                EaseType.InExpo => InExpo,

                EaseType.OutSin => OutSin,
                EaseType.OutCubic => OutCubic,
                EaseType.OutQuad => OutQuad,
                EaseType.OutQuart => OutQuart,
                EaseType.OutQuint => OutQuint,
                EaseType.OutCirc => OutCirc,
                EaseType.OutElastic => OutElastic,
                EaseType.OutBack => OutBack,
                EaseType.OutBounce => OutBounce,
                EaseType.OutExpo => OutExpo,

                EaseType.InOutSin => InOutSin,
                EaseType.InOutCubic => InOutCubic,
                EaseType.InOutQuad => InOutQuad,
                EaseType.InOutQuart => InOutQuart,
                EaseType.InOutQuint => InOutQuint,
                EaseType.InOutCirc => InOutCirc,
                EaseType.InOutElastic => InOutElastic,
                EaseType.InOutBack => InOutBack,
                EaseType.InOutBounce => InOutBounce,
                EaseType.InOutExpo => InOutExpo,
                _ => Flat,
            };
        }

        public static Func<float, float> GetShakeFunction(EaseType pEaseType = EaseType.Flat)
        {
            return pEaseType switch
            {
                EaseType.Flat => (x) => 1f,

                EaseType.InFlat => Flat,
                EaseType.InSin => InSin,
                EaseType.InCubic => InCubic,
                EaseType.InQuad => InQuad,
                EaseType.InQuart => InQuart,
                EaseType.InQuint => InQuint,
                EaseType.InCirc => InCirc,
                EaseType.InElastic => InElastic,
                EaseType.InBack => InBack,
                EaseType.InBounce => InBounce,
                EaseType.InExpo => InExpo,

                EaseType.OutFlat => (x) => 1 - Flat(x),
                EaseType.OutSin => (x) => 1 - OutSin(x),
                EaseType.OutCubic => (x) => 1 - OutCubic(x),
                EaseType.OutQuad => (x) => 1 - OutQuad(x),
                EaseType.OutQuart => (x) => 1 - OutQuart(x),
                EaseType.OutQuint => (x) => 1 - OutQuint(x),
                EaseType.OutCirc => (x) => 1 - OutCirc(x),
                EaseType.OutElastic => (x) => 1 - OutElastic(x),
                EaseType.OutBack => (x) => 1 - OutBack(x),
                EaseType.OutBounce => (x) => 1 - OutBounce(x),
                EaseType.OutExpo => (x) => 1 - OutExpo(x),

                EaseType.InOutFlat => (x) => UpDown(x),
                EaseType.InOutSin => (x) => UpDown(InOutSin(x)),
                EaseType.InOutCubic => (x) => UpDown(InOutCubic(x)),
                EaseType.InOutQuad => (x) => UpDown(InOutQuad(x)),
                EaseType.InOutQuart => (x) => UpDown(InOutQuart(x)),
                EaseType.InOutQuint => (x) => UpDown(InOutQuint(x)),
                EaseType.InOutCirc => (x) => UpDown(InOutCirc(x)),
                EaseType.InOutElastic => (x) => UpDown(InOutElastic(x)),
                EaseType.InOutBack => (x) => UpDown(InOutBack(x)),
                EaseType.InOutBounce => (x) => UpDown(InOutBounce(x)),
                EaseType.InOutExpo => (x) => UpDown(InOutExpo(x)),
                _ => Flat,
            };
        }

        public static Func<float, float> GetJumpFunction(EaseType pEaseType = EaseType.Flat)
        {
            return pEaseType switch
            {
                EaseType.Flat => (x) => SmoothUpDown(x),

                EaseType.InFlat => (x) => SmoothUpDown(Flat(x)),
                EaseType.InSin => (x) => SmoothUpDown(InSin(x)),
                EaseType.InCubic => (x) => SmoothUpDown(InCubic(x)),
                EaseType.InQuad => (x) => SmoothUpDown(InQuad(x)),
                EaseType.InQuart => (x) => SmoothUpDown(InQuart(x)),
                EaseType.InQuint => (x) => SmoothUpDown(InQuint(x)),
                EaseType.InCirc => (x) => SmoothUpDown(InCirc(x)),
                EaseType.InElastic => (x) => SmoothUpDown(InElastic(x)),
                EaseType.InBack => (x) => SmoothUpDown(InBack(x)),
                EaseType.InBounce => (x) => SmoothUpDown(InBounce(x)),
                EaseType.InExpo => (x) => SmoothUpDown(InExpo(x)),

                EaseType.OutFlat => (x) => SmoothUpDown(Flat(x)),
                EaseType.OutSin => (x) => SmoothUpDown(OutSin(x)),
                EaseType.OutCubic => (x) => SmoothUpDown(OutCubic(x)),
                EaseType.OutQuad => (x) => SmoothUpDown(OutQuad(x)),
                EaseType.OutQuart => (x) => SmoothUpDown(OutQuart(x)),
                EaseType.OutQuint => (x) => SmoothUpDown(OutQuint(x)),
                EaseType.OutCirc => (x) => SmoothUpDown(OutCirc(x)),
                EaseType.OutElastic => (x) => SmoothUpDown(OutElastic(x)),
                EaseType.OutBack => (x) => SmoothUpDown(OutBack(x)),
                EaseType.OutBounce => (x) => SmoothUpDown(OutBounce(x)),
                EaseType.OutExpo => (x) => SmoothUpDown(OutExpo(x)),

                EaseType.InOutFlat => (x) => SmoothUpDown(x),
                EaseType.InOutSin => (x) => SmoothUpDown(InOutSin(x)),
                EaseType.InOutCubic => (x) => SmoothUpDown(InOutCubic(x)),
                EaseType.InOutQuad => (x) => SmoothUpDown(InOutQuad(x)),
                EaseType.InOutQuart => (x) => SmoothUpDown(InOutQuart(x)),
                EaseType.InOutQuint => (x) => SmoothUpDown(InOutQuint(x)),
                EaseType.InOutCirc => (x) => SmoothUpDown(InOutCirc(x)),
                EaseType.InOutElastic => (x) => SmoothUpDown(InOutElastic(x)),
                EaseType.InOutBack => (x) => SmoothUpDown(InOutBack(x)),
                EaseType.InOutBounce => (x) => SmoothUpDown(InOutBounce(x)),
                EaseType.InOutExpo => (x) => SmoothUpDown(InOutExpo(x)),
                _ => Flat,
            };
        }

        public static float UpDown(float pRatio) => -2 * Mathf.Abs(pRatio - .5f) + 1;

        public static float SmoothUpDown(float pRatio) => Mathf.Sin(pRatio * Mathf.PI);

        public static float Flat(float pRatio) => pRatio;

        #region easeIns
        public static float InSin(float pX)
        {
            return 1 - Mathf.Cos(pX * Mathf.PI / 2);
        }

        public static float InCubic(float pX)
        {
            return pX * pX * pX;
        }

        public static float InCirc(float pX)
        {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(pX, 2));
        }

        public static float InQuint(float pX)
        {
            return pX * pX * pX * pX * pX;
        }

        /// <summary>
        /// values returned can be lower than 0
        /// </summary>
        /// <param name="pX"></param>
        /// <returns></returns>
        public static float InElastic(float pX)
        {
            return pX == 0 ? 0 : pX == 1 ? 1 : -Mathf.Pow(2, 10 * pX - 10) * Mathf.Sin((pX * 10f - 10.75f) * c4);
        }

        public static float InQuad(float pX)
        {
            return pX * pX;
        }

        public static float InQuart(float pX)
        {
            return pX * pX * pX * pX;
        }

        public static float InExpo(float pX)
        {
            return pX == 0f ? 0f : Mathf.Pow(2, 10 * pX - 10);
        }

        /// <summary>
        /// values returned can be lower than 0
        /// </summary>
        /// <param name="pX"></param>
        /// <returns></returns>
        public static float InBack(float pX)
        {
            return c3 * pX * pX * pX - c1 * pX * pX;
        }

        public static float InBounce(float pX)
        {
            return 1 - OutBounce(1 - pX);
        }
        #endregion

        #region EaseOuts
        public static float OutSin(float pX)
        {
            return Mathf.Sin(pX * Mathf.PI / 2);
        }

        public static float OutCubic(float pX)
        {
            return 1 - Mathf.Pow(1 - pX, 3);
        }

        public static float OutCirc(float pX)
        {
            return Mathf.Sqrt(1 - Mathf.Pow(pX - 1, 2));
        }

        public static float OutQuint(float pX)
        {
            return 1 - Mathf.Pow(1 - pX, 5);
        }

        /// <summary>
        /// values returned can be higher than 1
        /// </summary>
        /// <param name="pX"></param>
        /// <returns></returns>
        public static float OutElastic(float pX)
        {
            return pX == 0 ? 0 : pX == 1 ? 1 : Mathf.Pow(2, -10 * pX) * Mathf.Sin((pX * 10 - 0.75f) * c4) + 1;
        }

        public static float OutQuad(float pX)
        {
            return 1 - Mathf.Pow(1 - pX, 2);
        }

        public static float OutQuart(float pX)
        {
            return 1 - Mathf.Pow(1 - pX, 4);
        }

        public static float OutExpo(float pX)
        {
            return pX == 1 ? 1 : 1 - Mathf.Pow(2, -10 * pX);
        }

        /// <summary>
        /// values returned can be higher than 1
        /// </summary>
        /// <param name="pX"></param>
        /// <returns></returns>
        public static float OutBack(float pX)
        {
            return 1 + c3 * Mathf.Pow(pX - 1, 3) + c1 * Mathf.Pow(pX - 1, 2);
        }

        public static float OutBounce(float pX)
        {
            if (pX < 1 / d1)
            {
                return n1 * pX * pX;
            }
            else if (pX < 2 / d1)
            {
                return n1 * (pX -= 1.5f / d1) * pX + 0.75f;
            }
            else if (pX < 2.5f / d1)
            {
                return n1 * (pX -= 2.25f / d1) * pX + 0.9375f;
            }
            else
            {
                return n1 * (pX -= 2.625f / d1) * pX + 0.984375f;
            }
        }
        #endregion

        #region EaseInOuts
        public static float InOutSin(float pX)
        {
            return -(Mathf.Cos(Mathf.PI * pX) - 1) / 2;
        }

        public static float InOutCubic(float pX)
        {
            return pX < 0.5 ? 4 * pX * pX * pX : 1 - Mathf.Pow(-2 * pX + 2, 3) / 2;
        }

        public static float InOutCirc(float pX)
        {
            return pX < 0.5f ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * pX, 2))) / 2 : (Mathf.Sqrt(1 - Mathf.Pow(-2 * pX + 2, 2)) + 1) / 2;
        }

        public static float InOutQuint(float pX)
        {
            return pX < 0.5f ? 16 * pX * pX * pX * pX * pX : 1 - Mathf.Pow(-2 * pX + 2, 5) / 2;
        }

        /// <summary>
        /// values returned can be lower than 0 or higher than 1
        /// </summary>
        /// <param name="pX"></param>
        /// <returns></returns>
        public static float InOutElastic(float pX)
        {
            return pX == 0 ? 0 : pX == 1 ? 1 : pX < 0.5f ? -(Mathf.Pow(2, 20 * pX - 10) * Mathf.Sin((20 * pX - 11.125f) * c5)) / 2 : Mathf.Pow(2, -20 * pX + 10) * Mathf.Sin((20 * pX - 11.125f) * c5) / 2 + 1;
        }

        public static float InOutQuad(float pX)
        {
            return pX < 0.5f ? 2 * pX * pX : 1 - Mathf.Pow(-2 * pX + 2, 2) / 2;
        }

        public static float InOutQuart(float pX)
        {
            return pX < 0.5f ? 8 * pX * pX * pX * pX : 1 - Mathf.Pow(-2 * pX + 2, 4) / 2;
        }

        public static float InOutExpo(float pX)
        {
            return pX == 0 ? 0 : pX == 1 ? 1 : pX < 0.5f ? Mathf.Pow(2, 20 * pX - 10) / 2 : (2 - Mathf.Pow(2, -20 * pX + 10)) / 2;
        }

        /// <summary>
        /// values returned can be lower than 0 or higher than 1
        /// </summary>
        /// <param name="pX"></param>
        /// <returns></returns>
        public static float InOutBack(float pX)
        {
            return pX < 0.5f ? Mathf.Pow(2 * pX, 2) * ((c2 + 1) * 2 * pX - c2) / 2 : (Mathf.Pow(2 * pX - 2, 2) * ((c2 + 1) * (pX * 2 - 2) + c2) + 2) / 2;
        }

        public static float InOutBounce(float pX)
        {
            return pX < 0.5f ? (1 - OutBounce(1 - 2 * pX)) / 2 : (1 + OutBounce(2 * pX - 1)) / 2;
        }
        #endregion
    }
}