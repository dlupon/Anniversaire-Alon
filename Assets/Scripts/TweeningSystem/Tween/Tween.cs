// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 03 / 05 / 2025 #======~~~~-- //

using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnBocal.TweeningSystem.Interpolations;
using UnityEngine;

namespace UnBocal.TweeningSystem
{
	public partial class Tween
	{
        // -------~~~~~~~~~~================# // Tween
        private static Dictionary<object, List<Tween>> _objectAndTweens = new Dictionary<object, List<Tween>>();

        // -------~~~~~~~~~~================# // Interpolation
        public int ObjectCount => Objects.Count;
        private List<object> Objects => _objectsAndInterpolators.Keys.ToList();
        private Dictionary<object, Dictionary<string, List<Interpolation>>> _objectsAndInterpolators = new Dictionary<object, Dictionary<string, List<Interpolation>>>();

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# //  Tween Management

        #region // -------~~~~~~~~~~================# // Kill
        /// <summary>
        /// Stop all animations.
        /// </summary>
        public static void Kill()
        {
            foreach (object lCurrentObject in _objectAndTweens.Keys)
                foreach (Tween lCurrentTween in _objectAndTweens[lCurrentObject])
                    lCurrentTween.Stop();
        }

        /// <summary>
        /// Stop all animations on this object.
        /// </summary>
        public static void Kill(object pObject)
        {
            if (!_objectAndTweens.ContainsKey(pObject)) return;
            int lTweenCount = _objectAndTweens[pObject].Count;

            for (int lTweenIndex = lTweenCount - 1; lTweenIndex >= 0; lTweenIndex--)
                _objectAndTweens[pObject][lTweenIndex].Stop(pObject);
        }

        /// <summary>
        /// Stop pTween's animations.
        /// </summary>
        public static void Kill(Tween pTween) => pTween.Stop();
        #endregion

        #region // -------~~~~~~~~~~================# // Kill And Clear
        /// <summary>
        /// Stop all animations and clear all tweens.
        /// </summary>
        public static void KillAndClear()
        {      
            _objectAndTweens.Clear();
            TweenExecutionHandler.RemoveAll();
        }

        /// <summary>
        /// Stop all animations on this object and clear all related tweens.
        /// </summary>
        public static void KillAndClear(object pObject)
        {
            if (!_objectAndTweens.ContainsKey(pObject)) return;
            int lTweenCount = _objectAndTweens[pObject].Count;

            for (int lTweenIndex = lTweenCount - 1; lTweenIndex >= 0; lTweenIndex--)
                _objectAndTweens[pObject][lTweenIndex].StopAndClear(pObject);

            _objectAndTweens[pObject].Clear();
            _objectAndTweens.Remove(pObject);
        }

        /// <summary>
        /// Stop pTween's animations and clear it.
        /// </summary>
        public static void KillAndClear(Tween pTween) => pTween.StopAndClear();            
        #endregion

        private void Store(object pObject)
        {
            if (!_objectAndTweens.ContainsKey(pObject)) _objectAndTweens[pObject] = new List<Tween>();

            if (_objectAndTweens[pObject].Contains(this)) return;

            _objectAndTweens[pObject].Add(this);
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Control
        public void Start()
		{
            DoOnInterpolations(StartInterpolation);
            TweenExecutionHandler.AddInterpolations(GetInterpolations());
			TweenExecutionHandler.StartUpdateTween();
		}

        public void UnScaleStart()
        {
            DoOnInterpolations(UnScaleStartInterpolation);
            TweenExecutionHandler.AddInterpolations(GetInterpolations());
            TweenExecutionHandler.StartUpdateTween();
        }

        public void Play()
        {
            DoOnInterpolations(StartInterpolation);
            TweenExecutionHandler.AddInterpolations(GetInterpolations());
            TweenExecutionHandler.StartUpdateTween();
        }

        public void UnScalePlay()
        {
            DoOnInterpolations(UnScaleStartInterpolation);
            TweenExecutionHandler.AddInterpolations(GetInterpolations());
            TweenExecutionHandler.StartUpdateTween();
        }

        #region // -------~~~~~~~~~~================# // Stop
        /// <summary>
        /// Stop all interpolations but DON'T complete them.
        /// </summary>
        public void Stop() => TweenExecutionHandler.RemoveInterpolations(GetInterpolations());

        /// <summary>
        /// Stop all interpolations for this object but DON'T complete them.
        /// </summary>
        public void Stop(object pObject) => TweenExecutionHandler.RemoveInterpolations(GetInterpolations(pObject));

        /// <summary>
        /// Stop all interpolations for this object property but DON'T complete them.
        /// </summary>
        public void Stop(object pObject, string pProperty) => TweenExecutionHandler.RemoveInterpolations(GetInterpolations(pObject, pProperty));
        #endregion

        #region // -------~~~~~~~~~~================# // Clear
        /// <summary>
        /// Remove all stored interpolations.
        /// </summary>
        public void Clear()
        {
            foreach (object lObject in Objects)
                if (_objectAndTweens[lObject].Contains(this))
                    _objectAndTweens[lObject].Remove(this);

            _objectsAndInterpolators.Clear();
        }

        /// <summary>
        /// Remove all stored interpolations for this object.
        /// </summary>
        public void Clear(object pObject)
        {
            if (!_objectsAndInterpolators.ContainsKey(pObject)) return;
            
            _objectsAndInterpolators[pObject].Clear();
            _objectsAndInterpolators.Remove(pObject);
        }

        /// <summary>
        /// Remove all stored interpolations for this object's property.
        /// </summary>
        public void Clear(object pObject, string pProperty)
        {
            if (!_objectsAndInterpolators.ContainsKey(pObject)) return;
            if (!_objectsAndInterpolators[pObject].ContainsKey(pProperty)) return;

            TweenExecutionHandler.RemoveInterpolations(_objectsAndInterpolators[pObject][pProperty]);
            _objectsAndInterpolators[pObject][pProperty].Clear();
            _objectsAndInterpolators[pObject].Remove(pProperty);
        }
        #endregion

        #region // -------~~~~~~~~~~================# // Stop And Clear
        /// <summary>
        /// Stop all running interpolations and remove all stored interpolation.
        /// </summary>
        public void StopAndClear() { Stop(); Clear(); }

        /// <summary>
        /// Stop all running interpolations and remove all stored interpolation for this object.
        /// </summary>
        public void StopAndClear(object pObject) { Stop(pObject); Clear(pObject); }

        /// <summary>
        /// Stop all running interpolations and remove all stored interpolation for this object's property.
        /// </summary>
        public void StopAndClear(object pObject, string pProperty) { Stop(pObject, pProperty); Clear(pObject, pProperty); }
        #endregion

        #region // -------~~~~~~~~~~================# // Complete

        /// <summary>
        /// Complete All interpolations.
        /// </summary>
        public void Complete() => Complete(GetInterpolations());

        /// <summary>
        /// Complete All interpolations for this object.
        /// </summary>
        public void Complete(object pObject) => Complete(GetInterpolations(pObject));

        /// <summary>
        /// Complete all interpolations for this object property.
        /// </summary>
        public void Complete(object pObject, string pProperty) => Complete(GetInterpolations(pObject, pProperty));

        private void Complete(List<Interpolation> pInterpolations)
        {
            if (pInterpolations == null) return;

            List<Interpolation> lInterpolations = pInterpolations;
            int lInterpolationCount = lInterpolations.Count;

            Interpolation lCurrentInterpolation;
            for (int lCurrentInterpolationIndex = lInterpolationCount - 1; lCurrentInterpolationIndex >= 0; lCurrentInterpolationIndex--)
            {
                lCurrentInterpolation = lInterpolations[lCurrentInterpolationIndex];
                lCurrentInterpolation.CompleteInterpate();
            }

            Stop();
        }
        #endregion

        #region // -------~~~~~~~~~~================# // Complete And Clear
        /// <summary>
        /// Complete and remove all interpolations.
        /// </summary>
        public void CompleteAndClear() { Complete(); Clear(); }

        /// <summary>
        /// Complete and remove all interpolations for this object.
        /// </summary>
        public void CompleteAndClear(object pObject) { Complete(pObject); Tween.Kill(pObject); }

        /// <summary>
        /// Complete and remove all interpolations for this object property.
        /// </summary>
        public void CompleteAndClear(object pObject, string pProperty) { Complete(pObject, pProperty); Clear(pObject, pProperty); }
        #endregion

        #region // -------~~~~~~~~~~================# // Reset
        public void Reset()
        {
            Stop();
            DoOnInterpolations((x) => x.InterpolationMethod(0));
        }
        #endregion

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Interpolator
        private void DoOnInterpolations(System.Action<Interpolation> pFunc)
        {
            foreach (object lObject in _objectsAndInterpolators.Keys)
                foreach (string lPropertyName in _objectsAndInterpolators[lObject].Keys)
                    foreach (Interpolation lInterpolation in _objectsAndInterpolators[lObject][lPropertyName])
                        pFunc(lInterpolation);
        }

		private void StartInterpolation(Interpolation pInterpolator) => pInterpolator.Start();

		private void UnScaleStartInterpolation(Interpolation pInterpolator) => pInterpolator.UnScaleStart();

		private void PlayInterpolation(Interpolation pInterpolator) => pInterpolator.Play();

		private void UnScalePlayInterpolation(Interpolation pInterpolator) => pInterpolator.UnScalePlay();

        private List<Interpolation> GetInterpolations()
        {
            List<Interpolation> lInterpolators = new List<Interpolation>();
            DoOnInterpolations(lInterpolators.Add);

            return lInterpolators;
        }

        private List<Interpolation> GetInterpolations(object pObject)
        {
            if (!_objectsAndInterpolators.Keys.Contains(pObject)) return null;

            List<Interpolation> lInterpolators = new List<Interpolation>();
            foreach (string lProperty in _objectsAndInterpolators[pObject].Keys)
                foreach (Interpolation lInterpolation in _objectsAndInterpolators[pObject][lProperty])
                    lInterpolators.Add(lInterpolation);

            return lInterpolators;
        }

        private List<Interpolation> GetInterpolations(object pObject, string pProperty)
        {
            if (!_objectsAndInterpolators.Keys.Contains(pObject)) return null;

            List<Interpolation> lInterpolators = new List<Interpolation>();
            foreach (Interpolation lInterpolation in _objectsAndInterpolators[pObject][pProperty])
                lInterpolators.Add(lInterpolation);

            return lInterpolators;
        }

		private List<Interpolation> GetOrCreateInterpolationsList(object pObject, string pPropertyName)
        {
            // No Object (key) Found Then Create One
            if (!_objectsAndInterpolators.Keys.Contains(pObject))
                _objectsAndInterpolators[pObject] = new Dictionary<string, List<Interpolation>>();

            // No List Of Interpolations Found Then Create One
            if (!_objectsAndInterpolators[pObject].ContainsKey(pPropertyName))
                _objectsAndInterpolators[pObject][pPropertyName] = new List<Interpolation>();

            return _objectsAndInterpolators[pObject][pPropertyName];
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Interpolations
        private Interpolation AddInterpolation(object pObject, string pPropertyName, System.Action<float> pInterpolationMethod, float pDuration, float pDelay)
        {
            Interpolation lInterpolation = new Interpolation();
            lInterpolation.InterpolationMethod = pInterpolationMethod;
            lInterpolation.Duration = pDuration;
            lInterpolation.Delay = pDelay;
            lInterpolation.target = pObject;

            GetOrCreateInterpolationsList(pObject, pPropertyName).Add(lInterpolation);
            Store(pObject);

            return lInterpolation;
        }
    }
}