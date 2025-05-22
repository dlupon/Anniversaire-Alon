using System;
using System.Collections.Generic;

public static class EventBus
{
    // -------~~~~~~~~~~================# // Initialization
    public static Action<Room> RoomInit;
    public static Action<List<Room>> RoomListInit;

    // -------~~~~~~~~~~================# // Game
    public static Action Start;

    // -------~~~~~~~~~~================# // Room
    public static Action<int> NeedChangeRoom;
    public static Action NeedCameraFade;
    public static Action CameraFadeMid;
    public static Action<Room> RoomChanged;

    // -------~~~~~~~~~~================# // Anomalies
    public static Action Report;

}

public class RandomComparer : IComparer<object>
{
    public int Compare(object pRoomA, object pRoomB) => UnityEngine.Random.value.CompareTo(UnityEngine.Random.value);
}