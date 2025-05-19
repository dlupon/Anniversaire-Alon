using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Components
    [SerializeField] GameObject _room;
    [SerializeField] Camera _camera;

    // -------~~~~~~~~~~================# // Anomalies
    private List<Anomaly> anomalies = new List<Anomaly>();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        transform.GetComponentsInChildren(anomalies);
        anomalies.Sort(new AnomalyShuffleComparer());

        Hide();

        EventBus.RoomInit?.Invoke(this);

        Debug.Log(name);
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Visibility
    public void Show() => _room.SetActive(true);

    public void Hide() => _room.SetActive(false);
}