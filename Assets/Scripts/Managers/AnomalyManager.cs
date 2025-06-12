using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
    private List<Room> _disabledRooms = new List<Room>();
    private Room _currentRoom;

    // -------~~~~~~~~~~================# // Anomalies
    [Header("Anomalies")]
    [SerializeField] private int _maxAnomalyCount = 3;
    [SerializeField] private float _reportCooldDown = 5f;
    [SerializeField] private float _timeBeforeWarning = 5f;
    [SerializeField] private float _timeBeforeDeath = 5f;
    private List<Anomaly> _anomalies = new List<Anomaly>();
    private Coroutine _coroutine;

    // -------~~~~~~~~~~================# // Anomalies
    [Header("Heart")]
    [SerializeField] private Vector2Int _heartMinMaxCooldDown = new Vector2Int(5, 7);
    private int _currentHeartCooldown = -1;

    private bool _tooManyAnomalies => _anomalies.Count >= _maxAnomalyCount || (_rooms.Count <= 0 && _disabledRooms.Count <= 0);

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Awake()
    {
        EventBus.RoomListInit += SetRooms;
        EventBus.Start += StartLooping;
        EventBus.RoomChanged += UpdateRoom;
        EventBus.ReportAnomaly += TryFix;
        EventBus.TimeEnded += StopLooping;
        EventBus.ToMain += ClearAllAnomalies;
    }

    private void OnDestroy()
    {
        EventBus.RoomListInit -= SetRooms;
        EventBus.Start -= StartLooping;
        EventBus.RoomChanged -= UpdateRoom;
        EventBus.ReportAnomaly -= TryFix;
        EventBus.TimeEnded -= StopLooping;
        EventBus.ToMain -= ClearAllAnomalies;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Start Exit
    private void OnStart()
    {
        _currentHeartCooldown = -1;
    }

    private void ClearAllAnomalies()
    {
        foreach (Room pRoom in _activeRooms)
            pRoom.AnomalyHandeler.Fix();

        _anomalies.Clear();
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Room
    private void SetRooms(List<Room> pRooms)
    {
        _rooms = pRooms.ToList();
        _disabledRooms.Clear();
        _activeRooms.Clear();
        Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : Init Room -> {_rooms.Count}");
    }

        private void UpdateRoom(Room pNewRoom)
    {
        _currentRoom = pNewRoom;
        EventBus.GetAnomalyHandeler?.Invoke(_currentRoom.AnomalyHandeler);
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Anomalies
    private void StartLooping()
    {
        Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : Start Looping");

        _isActive = true;
        _coroutine = StartCoroutine(LoopAnomalies());
    }

    private void StopLooping()
    {
        Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : Stop Looping");

        _isActive = false;
        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    // -------~~~~~~~~~~================# // Looping
    private IEnumerator LoopAnomalies()
    {
        float lCurrentCooldown = _startCooldown;

        while (_isActive)
        {
            yield return new WaitForSeconds(lCurrentCooldown);
            Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : Try Trigger Anomaly");
            TryTriggerAnomaly();
            EnableNextRoom();
            yield return CheckTooManyAnomalies();
            lCurrentCooldown = _baseCooldown + _maxCooldownOffset - Random.value * _maxCooldownOffset * 2;
        }
    }

    // -------~~~~~~~~~~================# // Triggering / Rooms
    private void TryTriggerAnomaly()
    {
        Debug.Log(_rooms.Count);

        int lRandomIndex = Random.Range(0, _rooms.Count);
        int lRoomIndex = 0;
        Room lRoom;

        do
        {
            if (_rooms.Count <= 0) break;

            lRoom = _rooms[(lRandomIndex - lRoomIndex + _rooms.Count) % _rooms.Count];

            if (TriggerIfNotNull(lRoom)) return;

        } while (++lRoomIndex < _rooms.Count);

        Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : Can't Trigger Anomaly");
    }

    private bool TriggerIfNotNull(Room pRoom)
    {
        Anomaly lNewAnomaly = pRoom.AnomalyHandeler.Trigger(_currentHeartCooldown == 0);

        if (lNewAnomaly == null)
        {
            _rooms.Remove(pRoom);
            return false;
        }

        _anomalies.Add(lNewAnomaly);
        _rooms.Remove(pRoom);
        _activeRooms.Add(pRoom);

        EventBus.GetAnomalyHandeler?.Invoke(_currentRoom.AnomalyHandeler);

        Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : New Anomaly Successfully Triggered");

        if (PlayerPrefs.GetInt(nameof(Heart), 0) >= GlobalVariables.LetterCount) return true;

        if (--_currentHeartCooldown < 0) _currentHeartCooldown = Random.Range(_heartMinMaxCooldDown.x, _heartMinMaxCooldDown.y);

        return true;
    }

    private void EnableNextRoom()
    {
        if (_disabledRooms.Count <= 0) return;

        Room lRoom = _disabledRooms[0];
        _disabledRooms.Remove(lRoom);
        _rooms.Add(lRoom);
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
        _disabledRooms.Add(_currentRoom);

        EventBus.AnomalyFixed?.Invoke();
    }

    // -------~~~~~~~~~~================# // Too Many Anomalies
    private IEnumerator CheckTooManyAnomalies()
    {
        Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : Too Many Anomalies : {_tooManyAnomalies}");
        if (_tooManyAnomalies)
        {
            Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : Wait For Checking Game Over / Warning");
            yield return new WaitForSeconds(_timeBeforeWarning);
            yield return new WaitUntil(() => !_isSearching);
            TryWarn();
            yield return new WaitForSeconds(_timeBeforeDeath);
            yield return new WaitUntil(() => !_isSearching);
            CheckGameOver();
        }
    }

    private void TryWarn()
    {
        if (!_tooManyAnomalies) return;
        Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : Warning");
        Warn();
    }

    private void Warn()
    {
        EventBus.TooManyAnomalies?.Invoke();
        EventBus.Warn?.Invoke("/!\\ WARNING THERE IS TOO MANY ANOMALIES /!\\ ", 5, Color.red);
    }

    private void CheckGameOver()
    {
        if (!_tooManyAnomalies) return;
        Debug.Log($"<color=#ff0000>{nameof(AnomalyManager)}</color> : Game Over");
        GameOver();
    }

    private void GameOver()
    {
        List<Anomaly> lAnomalies = new List<Anomaly>();
        foreach (Room lRoom in _activeRooms)
            lAnomalies.Add(lRoom.AnomalyHandeler.ActiveAnomaly);

        StopLooping();

        EventBus.StopTime?.Invoke();
        EventBus.GameOverGetAnomalies?.Invoke(lAnomalies);
        EventBus.GameOver?.Invoke();
    }
}