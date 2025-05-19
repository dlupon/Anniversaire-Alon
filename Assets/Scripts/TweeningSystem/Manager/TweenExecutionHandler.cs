// --~~~~======# Author : Lupon Dylan #======~~~~~~--- //
// --~~~~======# Date   : 03 / 05 / 2025 #======~~~~-- //

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnBocal.TweeningSystem.Interpolations;
using System.Linq;
using UnityEngine.SceneManagement;

namespace UnBocal.TweeningSystem
{
	public class TweenExecutionHandler : MonoBehaviour
	{
		// -------~~~~~~~~~~================# // Host Properties
		private const string HOST_NAME = nameof(TweenExecutionHandler);

		// -------~~~~~~~~~~================# // Singleton
		public static TweenExecutionHandler Instance => GetInstance();
		private static TweenExecutionHandler _instance;

		// -------~~~~~~~~~~================# // Tween
        private HashSet<Interpolation> _interpolators = new HashSet<Interpolation>();

		// -------~~~~~~~~~~================# // Coroutine
		private Coroutine _coroutine;

		// ----------------~~~~~~~~~~~~~~~~~~~==========================# // Singleton
		public static void Init()
		{
			if (_instance != null) return;
			GetInstance();
        }

		private static TweenExecutionHandler GetInstance()
		{
            if (_instance == null) InitInstance();
			return _instance;
		}

        private static void InitInstance()
        {
            GameObject lHost = new GameObject(HOST_NAME);
			_instance = lHost.AddComponent<TweenExecutionHandler>();
			DontDestroyOnLoad(lHost);
            _instance.ConnectEvents();
        }

		// ----------------~~~~~~~~~~~~~~~~~~~==========================# // Events
		private void ConnectEvents()
		{
			SceneManager.sceneUnloaded += KillAll;
		}


        // -------~~~~~~~~~~================# // Interpolation Management
        public static void AddInterpolations(List<Interpolation> pInterpolators)
		{
            Init();
            _instance._interpolators.UnionWith(pInterpolators);
        }

        public static void RemoveInterpolations(List<Interpolation> pInterpolators)
        {
            Init();
			if (pInterpolators == null) return;
            foreach (Interpolation interpolator in pInterpolators)
				RemoveInterpolator(interpolator);
        }

		public static void RemoveAll()
		{
			Init();
            _instance._interpolators?.Clear();
        }

        private static void RemoveInterpolator(Interpolation pInterpolator)
        {
            if (!_instance._interpolators.Contains(pInterpolator)) return;
            _instance._interpolators.Remove(pInterpolator);
        }

        // -------~~~~~~~~~~================# // Coroutine
        public static void StartUpdateTween()
		{
            Init();
			_instance.StartCoroutine();
        }

		private void StartCoroutine()
		{
            if (_coroutine != null) return;
			_coroutine = StartCoroutine(nameof(LoopThroughTweens));
        }

		private IEnumerator LoopThroughTweens()
		{
			while (_interpolators.Count > 0)
			{
				UpdateTweens();
                yield return new WaitForSeconds(0);
			}

			_coroutine = null;
        }

		private void UpdateTweens()
		{
			List<Interpolation> lInterpolations = _interpolators.ToList();
			int lInterpolatorCount = lInterpolations.Count;

			Interpolation lCurrentInterpolator;
			for (int lCurrentInterpolatorIndex = lInterpolatorCount - 1; lCurrentInterpolatorIndex >= 0; lCurrentInterpolatorIndex--)
            {
				lCurrentInterpolator = lInterpolations[lCurrentInterpolatorIndex];
                lCurrentInterpolator.Update?.Invoke();
				if (!lCurrentInterpolator.IsFinished) continue;
				RemoveInterpolator(lCurrentInterpolator);
            }
        }

		// -------~~~~~~~~~~================# // Reset
		private void KillAll(Scene pOldScene)
		{
			_coroutine = null;
			StopAllCoroutines();
			Tween.KillAndClear();
		}
	}
}