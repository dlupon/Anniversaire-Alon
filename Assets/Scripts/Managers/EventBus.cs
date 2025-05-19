using System;
using System.Collections.Generic;

public static class EventBus
{
    public static Action<Room> RoomInit;
    public static Action<List<Room>> RoomListInit;
    
    public static Action<int> NeedChangeRoom;
    public static Action NeedCameraFade;
    public static Action CameraFadeMid;
    public static Action<Room> RoomChanged;

}
