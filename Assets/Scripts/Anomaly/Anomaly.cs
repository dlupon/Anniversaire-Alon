using System.Collections.Generic;
using UnityEngine;

public class Anomaly : MonoBehaviour
{
    public bool IsActive { get; protected set; }

    public virtual void Trigger() => IsActive = true;

    public virtual void Fix() => IsActive = false;
}

public class AnomalyShuffleComparer : IComparer<Anomaly>
{
    public int Compare(Anomaly pRoomA, Anomaly pRoomB) => Random.value.CompareTo(Random.value);
}