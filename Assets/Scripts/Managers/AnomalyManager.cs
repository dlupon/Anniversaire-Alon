using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Cooldown
    [Header("Cooldown")]
    [SerializeField] private float _startCooldown = 1;
    [SerializeField] private float _baseCooldown = 2f;
    [SerializeField] private float _maxCooldownOffset = 1f;

    // -------~~~~~~~~~~================# // Active
    private bool _isActive;
    private bool _isSearching;

    // -------~~~~~~~~~~================# // Rooms
    private List<Room> _rooms;
    private List<Room> _activeRooms = new List<Room>();
    private Room _currentRoom;

    // -------~~~~~~~~~~================# // Anomalies
    [Header("Anomalies")]
    [SerializeField] private int _maxAnomalyCount = 3;
    [SerializeField] private float _reportCooldDown = 5f;
    [SerializeField] private float _timeBeforeWarning = 5f;
    [SerializeField] private float _timeBeforeDeath = 5f;
    private List<Anomaly> _anomalies = new List<Anomaly>();
    private bool _warned = false;
    private Coroutine _coroutine;

    private bool _tooManyAnomalies => _anomalies.Count >= _maxAnomalyCount || _rooms.Count <= 0;

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Awake()
    {
        EventBus.RoomListInit += SetRooms;
        EventBus.Start += StartAnomalies;
        EventBus.RoomChanged += UpdateRoom;
        EventBus.ReportAnomaly += TryFix;
    }

    private void OnDestroy()
    {
        EventBus.RoomListInit -= SetRooms;
        EventBus.Start -= StartAnomalies;
        EventBus.RoomChanged -= UpdateRoom;
        EventBus.ReportAnomaly -= TryFix;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Room
    private void SetRooms(List<Room> pRooms) => _rooms = pRooms.ToList();

    private void UpdateRoom(Room pNewRoom) => _currentRoom = pNewRoom;

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Anomalies
    private void StartAnomalies()
    {
        _isActive = true;
        _coroutine = StartCoroutine(LoopAnomalies());
    }

    // -------~~~~~~~~~~================# // Looping
    private IEnumerator LoopAnomalies()
    {
        float lCurrentCooldown = _startCooldown;

        while (_isActive)
        {
            yield return new WaitForSeconds(lCurrentCooldown);
            Debug.LogError("Try Trigger Anomaly");
            TryTriggerAnomaly();
            yield return CheckTooManyAnomalies();
            lCurrentCooldown = _baseCooldown + _maxCooldownOffset - Random.value * _maxCooldownOffset * 2;
        }
    }

    // -------~~~~~~~~~~================# // Triggering
    private void TryTriggerAnomaly()
    {
        int lRandomIndex = Random.Range(0, _rooms.Count);
        int lRoomIndex = 0;
        Room lRoom;
        Anomaly lNewAnomaly;

        do
        {
            if (_rooms.Count <= 0) break;

            lRoom = _rooms[(lRandomIndex - lRoomIndex + _rooms.Count) % _rooms.Count];
            lNewAnomaly = lRoom.AnomalyHandeler.Trigger();

            if (lNewAnomaly == null)
            {
                _rooms.Remove(lRoom);
                continue;
            }

            _anomalies.Add(lNewAnomaly);
            _rooms.Remove(lRoom);
            _activeRooms.Add(lRoom);
            
            Debug.LogError("New Anomaly Triggered");

            return;

        } while (++lRoomIndex < _rooms.Count);

        Debug.LogError("Can't Trigger Anomaly");

        return;
    }

    // -------~~~~~~~~~~================# // Fixing
    private void TryFix(string pAnomaly)
    {
        if (_isSearching) return;
        StartCoroutine(Fix(pAnomaly));
    }

    private IEnumerator Fix(string pAnomaly)
    {
        _isSearching = true;
            
        yield return new WaitForSeconds(_reportCooldDown);

        Anomaly lAnomaly = _currentRoom.AnomalyHandeler.Fix(pAnomaly);

        if (lAnomaly == null) FixFailed();
        else FixedSuccessfully(lAnomaly);

        _isSearching = false;

        EventBus.CheckAnomalyDone?.Invoke();
    }

    private void FixFailed() => EventBus.AnomalyNotFounded?.Invoke();

    private void FixedSuccessfully(Anomaly pAnomaly)
    {
        _anomalies.Remove(pAnomaly);
        _activeRooms.Remove(_currentRoom);
        _rooms.Add(_currentRoom);

        EventBus.AnomalyFixed?.Invoke();
    }

    // -------~~~~~~~~~~================# // Too Many Anomalies
    private IEnumerator CheckTooManyAnomalies()
    {
        Debug.LogError($"Too Many Anomalies : {_tooManyAnomalies}");
        if (_tooManyAnomalies)
        {
            Debug.LogError("WAIT FOR CHECKING");
            yield return new WaitForSeconds(_warned ? _timeBeforeDeath : _timeBeforeWarning);
            yield return new WaitUntil(() => !_isSearching);
            CheckGameOver();
            TryWarn();
        }

    }

    private void CheckGameOver()
    {
        if (!_warned || !_tooManyAnomalies) return;
        Debug.LogError("GameOver");
        GameOver();
    }

    private void GameOver()
    {
        List<Anomaly> lAnomalies = new List<Anomaly>();
        foreach (Room lRoom in _activeRooms)
            lAnomalies.Add(lRoom.AnomalyHandeler.ActiveAnomaly);

        StopCoroutine(_coroutine);

        EventBus.GameOverGetAnomalies?.Invoke(lAnomalies);
        EventBus.GameOver?.Invoke();
    }

    private void TryWarn()
    {
        if (_warned || !_tooManyAnomalies) return;
        Debug.LogError("Warning");
        Warn();
    }

    private void Warn()
    {
        _warned = true;

        EventBus.TooManyAnomalies?.Invoke();
    }
}