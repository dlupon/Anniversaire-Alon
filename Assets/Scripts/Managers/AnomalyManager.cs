using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AnomalyManager : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Cooldown
    [Header("Cooldown")]
    [SerializeField] private float _startCooldown = 1;
    [SerializeField] private float _baseCooldown = 2f;
    [SerializeField] private float _maxCooldownOffset = 1f;

    // -------~~~~~~~~~~================# // Active
    private bool _isActive;

    // -------~~~~~~~~~~================# // Rooms
    private List<Room> _rooms;
    private List<Room> _activeRooms = new List<Room>();
    private Room _currentRoom;

    // -------~~~~~~~~~~================# // Anomalies
    [Header("Anomalies")]
    [SerializeField] private int _maxAnomalyCount = 3;
    [SerializeField] private float _reportCooldDown = 5f;
    private List<IAnomaly> _anomalies = new List<IAnomaly>();

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
        StartCoroutine(LoopAnomalies());
    }

    private IEnumerator LoopAnomalies()
    {
        float lCurrentCooldown = _startCooldown;

        while (_isActive)
        {
            yield return new WaitUntil(() => _anomalies.Count < _maxAnomalyCount);
            yield return new WaitForSeconds(lCurrentCooldown);
            TriggerAnomaly();
            lCurrentCooldown = _baseCooldown + _maxCooldownOffset - Random.value * _maxCooldownOffset * 2;
        }
    }

    private void TriggerAnomaly()
    {
        int lRoomCount = _rooms.Count;
        if (lRoomCount <= 0) return;

        int lRandomIndex = Random.Range(0, lRoomCount);
        int lRoomIndex = 0;
        Room lRoom;
        IAnomaly lNewAnomaly;

        do
        {
            lRoom = _rooms[(lRandomIndex + lRoomIndex) % (lRoomCount - 1)];
            lNewAnomaly = lRoom.AnomalyHandeler.Trigger();

            if (lNewAnomaly == null) continue;            
            _anomalies.Add(lNewAnomaly);
            _rooms.Remove(lRoom);
            _activeRooms.Add(lRoom);

        } while (++lRoomIndex < lRoomCount);
    }

    private void TryFix(string pAnomaly)
    {
        StartCoroutine(Fix(pAnomaly));
    }

    private IEnumerator Fix(string pAnomaly)
    {
        yield return new WaitForSeconds(_reportCooldDown);

        IAnomaly lAnomaly = _currentRoom.AnomalyHandeler.Fix(pAnomaly);

        if (lAnomaly == null) FixFailed();
        else FixedSuccessfully(lAnomaly);

        EventBus.CheckAnomalyDone?.Invoke();
    }

    private void FixFailed() => EventBus.AnomalyNotFounded?.Invoke();

    private void FixedSuccessfully(IAnomaly pAnomaly)
    {
        _anomalies.Remove(pAnomaly);
        _activeRooms.Remove(_currentRoom);
        _rooms.Add(_currentRoom);

        EventBus.AnomalyFixed?.Invoke();
    }
}