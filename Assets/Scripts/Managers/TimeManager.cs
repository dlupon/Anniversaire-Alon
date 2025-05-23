using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private const float MAX_MINUTE = 60f;

    [SerializeField] private int _hourCount = 6;
    [SerializeField] private float _maxTimeInMinutes = 20f;
    private float _hourLength;
    private float _minuteLength;

    private int CurrentHour;
    private int CurrentMinute;

    private void Start()
    {
        _hourLength = _maxTimeInMinutes * MAX_MINUTE / ((float)_hourCount);
        _minuteLength = _hourLength / MAX_MINUTE;

        StartCoroutine(LoopTime());
    }

    private IEnumerator LoopTime()
    {
        while (CurrentHour < _hourCount)
        {
            EventBus.TimeUpdated?.Invoke(CurrentHour, CurrentMinute);
            yield return new WaitForSeconds(_minuteLength);

            if (++CurrentMinute >= MAX_MINUTE)
            {
                CurrentMinute = 0;
                CurrentHour++;

                EventBus.TimeNewHour?.Invoke(CurrentHour);
            }
        }

        EventBus.TimeUpdated?.Invoke(CurrentHour, CurrentMinute);
        EventBus.TimeEnded?.Invoke();
    }
}