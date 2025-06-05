using System;
using System.Collections.Generic;
using System.Drawing;

public static class EventBus
{
    // -------~~~~~~~~~~================# // Initialization
    public static Action<Room> RoomInit;
    public static Action<List<Room>> RoomListInit;

    // -------~~~~~~~~~~================# // Game
    public static Action Start;
    public static Action GameOver;
    public static Action<List<Anomaly>> GameOverGetAnomalies;

    // -------~~~~~~~~~~================# // Room
    public static Action<int> NeedChangeRoom;
    public static Action NeedCameraFade;
    public static Action CameraFadeMid;
    public static Action<Room> RoomChanged;

    // -------~~~~~~~~~~================# // Anomalies
    public static Action Report;
    public static Action<string> ReportAnomaly;
    public static Action CheckAnomalyDone;
    public static Action AnomalyFixed;
    public static Action AnomalyNotFounded;
    public static Action TooManyAnomalies;

    // -------~~~~~~~~~~================# // Warning
    public static Action<string, float, UnityEngine.Color> Warn;

    // -------~~~~~~~~~~================# // Time
    public static Action<int, int> TimeUpdated;
    public static Action<int> TimeNewHour;
    public static Action TimeEnded;

    public static void Reset()
    {
        RoomInit = null;
        RoomListInit = null;
        Start = null;
        GameOver = null;
        GameOverGetAnomalies = null;
        NeedChangeRoom = null;
        NeedCameraFade = null;
        CameraFadeMid = null;
        RoomChanged = null;
        Report = null;
        ReportAnomaly = null;
        CheckAnomalyDone = null;
        AnomalyFixed = null;
        AnomalyNotFounded = null;
        TooManyAnomalies = null;
        Warn = null;
        TimeUpdated = null;
        TimeEnded = null;
    }
}

public class RandomComparer : IComparer<object>
{
    public int Compare(object pRoomA, object pRoomB) => UnityEngine.Random.value.CompareTo(UnityEngine.Random.value);
}