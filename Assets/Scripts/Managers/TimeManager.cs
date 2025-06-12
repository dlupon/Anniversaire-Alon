using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private const float MAX_MINUTE = 60f;

    [SerializeField] private int _hourCount = 6;
    [SerializeField] private float _maxTimeInMinutes = 20f;
    private float _hourLength;
    private float _minuteLength;

    private Coroutine _coroutine;
    private int _currentHour;
    private int _currentMinute;

    private void Awake()
    {
        EventBus.StopTime += StopTime;
        EventBus.Start += StartTime;
    }

    private void OnDestroy()
    {
        EventBus.StopTime -= StopTime;
        EventBus.Start -= StartTime;
    }

    private void Start()
    {
        _hourLength = _maxTimeInMinutes * MAX_MINUTE / ((float)_hourCount);
        _minuteLength = _hourLength / MAX_MINUTE;
    }

    private void StartTime() => _coroutine = StartCoroutine(LoopTime());

    private void StopTime() => StopCoroutine(_coroutine);

    private IEnumerator LoopTime()
    {
        while (_currentHour < _hourCount)
        {
            EventBus.TimeUpdated?.Invoke(_currentHour, _currentMinute);
            yield return new WaitForSeconds(_minuteLength);

            if (++_currentMinute >= MAX_MINUTE)
            {
                _currentMinute = 0;
                _currentHour++;

                EventBus.TimeNewHour?.Invoke(_currentHour);
            }
        }

        EventBus.TimeUpdated?.Invoke(_currentHour, _currentMinute);
        EventBus.TimeEnded?.Invoke();
    }

}