using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Cooldown
    [SerializeField] private float _startCooldown = 1;
    [SerializeField] private float _baseCooldown = 2f;
    [SerializeField] private float _maxCooldownOffset = 1f;

    // -------~~~~~~~~~~================# // Active
    private bool _isActive;

    // -------~~~~~~~~~~================# // Rooms
    private List<Room> _rooms;
    private Room _currentRoom;
    
    // -------~~~~~~~~~~================# // Anomalies
    private List<IAnomaly> _anomalies = new List<IAnomaly>();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Awake()
    {
        EventBus.RoomListInit += SetRooms;
        EventBus.Start += StartAnomalies;
        EventBus.RoomChanged += UpdateRoom;
    }

    private void OnDestroy()
    {
        EventBus.RoomListInit -= SetRooms;
        EventBus.Start -= StartAnomalies;
        EventBus.RoomChanged -= UpdateRoom;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Room
    private void SetRooms(List<Room> pRooms) => _rooms = pRooms;

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
            yield return new WaitForSeconds(lCurrentCooldown);
            TriggerAnomaly();
            lCurrentCooldown = _baseCooldown + _maxCooldownOffset - Random.value * _maxCooldownOffset * 2;
        }
    }

    private void TriggerAnomaly()
    {
        int lRandomIndex = Random.Range(0, _rooms.Count);

        IAnomaly lNewAnomaly = _rooms[lRandomIndex].AnomalyHandeler.Trigger();

        if (lNewAnomaly == null) return;

        _anomalies.Add(lNewAnomaly);

        Debug.Log("ANOMATY TRIGGERED");
    }
}