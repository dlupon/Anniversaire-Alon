using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnomalyManager : MonoBehaviour
{
    private List<Room> _rooms;

    private void Awake()
    {
        EventBus.RoomListInit += SetRoom;
    }

    private void SetRoom(List<Room> pRooms) => _rooms = pRooms;

    private void GetAnomaly()
    {

    }
}