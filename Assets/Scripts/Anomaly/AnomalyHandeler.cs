using System.Collections.Generic;
using UnityEngine;

public class AnomalyHandeler : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Active
    public bool IsActive => _current != null;

    // -------~~~~~~~~~~================# // Anomaly
    private List<IAnomaly> anomalies = new List<IAnomaly>();
    private IAnomaly _current;

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        transform.GetComponentsInChildren(anomalies);
        anomalies.Sort(new RandomComparer());
        Debug.Log($"{name} -> {anomalies.Count}");
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public IAnomaly Trigger()
    {
        if (anomalies.Count <= 0) return null;

        _current = anomalies[0];
        anomalies.Remove(_current);
        anomalies.Add(_current);

        _current.Trigger();

        return _current;
    }

    public IAnomaly Fix()
    {
        if (_current  == null) return null;

        IAnomaly lFixedAnomaly = _current;
        lFixedAnomaly.Fix();

        _current = null;

        return lFixedAnomaly;
    }
}