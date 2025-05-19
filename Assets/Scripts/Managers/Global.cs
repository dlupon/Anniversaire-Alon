using System;
using System.Collections.Generic;

public static class EventBus
{
    public static Action<Room> RoomInit;
    public static Action<List<Room>> RoomListInit;

    public static Action Start;

    public static Action<int> NeedChangeRoom;
    public static Action NeedCameraFade;
    public static Action CameraFadeMid;
    public static Action<Room> RoomChanged;

}

public class RandomComparer : IComparer<object>
{
    public int Compare(object pRoomA, object pRoomB) => UnityEngine.Random.value.CompareTo(UnityEngine.Random.value);
}