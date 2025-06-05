// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 03 / 15 / 2025 #======~~~~-- //

using System;
using UnityEngine;

namespace UnBocal.TweeningSystem.Interpolations
{
    public class Interpolation
    {
        // -------~~~~~~~~~~================# // Events
        public Action OnStarted;
        public Action OnFinished;

        // -------~~~~~~~~~~================# // Time
        public bool IsFinished => Update == null;
        public float Ratio => Mathf.Clamp01((Time.time - StartTime) / (EndTime - StartTime));
        public float UnscaleRatio => Mathf.Clamp01((Time.unscaledTime - StartTime) / (EndTime - StartTime));
        public float OverallDuration => Delay + Duration;

        private bool TimeScaleDependent = true;
        private float StartTime;
        private float EndTime;
        public float Duration;
        public float Delay;

        // -------~~~~~~~~~~================# // Value
        public Action<float> InterpolationMethod;

        // -------~~~~~~~~~~================# // Interpolation
        public Action Update;

        // -------~~~~~~~~~~================# // Target
        public object target;

        // -------~~~~~~~~~~================# // Initialization
        public Interpolation() => Update = null;

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Time
        /// <summary>
        /// Reset time so the Ratio is at the right place.
        /// </summary>
        public void Start()
        {
            StartTime = (TimeScaleDependent ? Time.time : Time.unscaledTime) + Delay;
            EndTime = StartTime + Duration;

            if (Delay > 0) StartInterpolate(TimeScaleDependent);
            else StartWait(TimeScaleDependent);
        }

        public void UnScaleStart()
        {
            StartTime = Time.unscaledTime + Delay;
            EndTime = StartTime + Duration;

            if (Delay > 0) StartInterpolate(false);
            else StartWait(false);
        }

        public void Play()
        {
            if (!IsFinished) return;
            Start();
        }

        public void UnScalePlay()
        {
            if (!IsFinished) return;
            UnScaleStart();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Interpolation
        public Interpolation SetTimeScaleDependent(bool pTimeScaleDependent) { TimeScaleDependent = pTimeScaleDependent; return this; }

        public void StartWait(bool pScaled)
        {
            Update = pScaled ? UpdateInterpolation : UpdateInterpolationUnScaled;
        }

        public void StartInterpolate(bool pScaled)
        {
            Update = pScaled ? UpdateWait : UpdateWaitUnScaled;
            OnStarted?.Invoke();
        }

        public void CompleteInterpate()
        {
            Update = null;
            InterpolationMethod?.Invoke(1);
            OnFinished?.Invoke();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Interpolation Scaled
        private void UpdateWait()
        {
            if (Time.time < StartTime) return;
            Update = UpdateInterpolation;
        }

        private void UpdateInterpolation()
        {
            if (target.IsNull()) Update = null;
            else if (Ratio >= 1) CompleteInterpate();
            else InterpolationMethod?.Invoke(Ratio);
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Interpolation UnScaled
        private void UpdateWaitUnScaled()
        {
            if (Time.unscaledTime < StartTime) return;
            Update = UpdateInterpolationUnScaled;
        }

        private void UpdateInterpolationUnScaled()
        {
            if (target.IsNull()) Update = null;
            else if (UnscaleRatio >= 1) CompleteInterpate();
            else InterpolationMethod?.Invoke(UnscaleRatio);
        }
    }
}