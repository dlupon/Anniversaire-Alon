// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 04 / 06 / 2025 #======~~~~-- //

using UnBocal.TweeningSystem.Interpolations;
using UnityEngine;

namespace UnBocal.TweeningSystem
{
    public partial class Tween
    {
        private static class Error
        {
            public static Interpolation InterpolationNotSupported(object pTarget, string pInterpolationName)
            {
                Debug.LogError($"{pInterpolationName} interpolation are not supported on {pTarget.GetType()}.");
                return null;
            }
        }
    }
}