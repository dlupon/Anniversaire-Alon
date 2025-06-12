using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Room
    [SerializeField] private Transform _roomContainer;
    private List<Room> _rooms = new List<Room>();
    private int _currentRoomIndex;
    private Room _currentRoom;

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Awake()
    {
        EventBus.RoomInit += _rooms.Add;
        EventBus.NeedChangeRoom += ShowRoomIndexOffset;
        EventBus.Start += OnStart;
    }

    private void OnDestroy()
    {
        EventBus.RoomInit -= _rooms.Add;
        EventBus.NeedChangeRoom -= ShowRoomIndexOffset;
        EventBus.Start -= OnStart;
    }

    private void Start()
    {
        transform.GetComponentsInChildren(_rooms);
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // StartGame
    private void OnStart()
    {
        EventBus.RoomListInit?.Invoke(_rooms);

        Do(HideRoom);
        ShowRoom(0);
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Room
    private void Do(Action<Room> pMethod)
    {
        for (int pIndex = _rooms.Count - 1; pIndex >= 0; pIndex--)
            pMethod(_rooms[pIndex]);
    }

    private void HideRoom(Room pRoom) => pRoom.Hide();

    private void ShowRoom(int pIndex)
    {
        if (pIndex >= _rooms.Count) return;
        
        _currentRoomIndex = pIndex;

        _currentRoom?.Hide();
        _currentRoom = _rooms[pIndex];
        _currentRoom.Show();

        EventBus.RoomChanged?.Invoke(_currentRoom);
    }

    private void ShowRoomIndexOffset(int pOffset)
    {
        int lCurrentRoomIndex = _currentRoomIndex + pOffset;

        if (lCurrentRoomIndex < 0) _currentRoomIndex = _roomContainer.childCount - 1;
        else if (lCurrentRoomIndex >= _roomContainer.childCount) _currentRoomIndex = 0;
        else _currentRoomIndex = lCurrentRoomIndex;

        ShowRoom(_currentRoomIndex);
    }
}
