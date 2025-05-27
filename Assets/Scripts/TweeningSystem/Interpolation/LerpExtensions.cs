using System;
using System.Linq;
using UnityEngine;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        public static class Lerp
        {
            public static string Whrite(string pTarget, float pRatio)
            {
                int lLength = (int)(pTarget.Count() * pRatio);
                char[] lChars = pTarget.ToCharArray();
                string lResult = "";

                for (int lcharIndex = 0; lcharIndex < lLength; lcharIndex++)
                    lResult += lChars[lcharIndex];

                return lResult;
            }

            public static Vector3 ShakeVector(Vector3 pTarget, float pDistance, float pRatio, Func<float, float> pEasing, bool pGetBackToTarget = true)
                => pGetBackToTarget switch
                {
                    false => pTarget + UnityEngine.Random.onUnitSphere * pDistance * pEasing(pRatio),
                    _ => pRatio >= 1 ? pTarget : pTarget + UnityEngine.Random.onUnitSphere * pDistance * pEasing(pRatio)
                };

            public static Vector3 ShakeVectorAxis(Vector3 pTarget, float pOffset, float pRatio, Func<float, float> pEasing, bool pGetBackToTarget = true)
            {
                Vector3 lNewVector = pTarget;
                lNewVector.x += - pOffset * .5f + pOffset * UnityEngine.Random.value;
                lNewVector.y += - pOffset * .5f + pOffset * UnityEngine.Random.value;
                lNewVector.z += - pOffset * .5f + pOffset * UnityEngine.Random.value;

                return pGetBackToTarget ? Vector3.Lerp(pTarget, lNewVector, pEasing(pRatio)) : pRatio >= 1 ? pTarget : Vector3.Lerp(pTarget, lNewVector, pEasing(pRatio));
            }
        }
    }
}