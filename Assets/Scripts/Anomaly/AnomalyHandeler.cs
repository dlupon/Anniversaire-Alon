using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnomalyHandeler : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Active
    public bool IsActive => _activeAnomaly != null;

    // -------~~~~~~~~~~================# // Anomaly
    private List<Anomaly> _anomalies = new List<Anomaly>();
    private Anomaly _activeAnomaly;

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        transform.GetComponentsInChildren(_anomalies);
        _anomalies.Sort(new RandomComparer());
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public Anomaly Trigger()
    {
        if (_anomalies.Count <= 0 || _activeAnomaly != null) return null;

        _activeAnomaly = _anomalies[0];
        _anomalies.Remove(_activeAnomaly);

        _activeAnomaly.Trigger();

        return _activeAnomaly;
    }

    public Anomaly Fix(string pAnomaly)
    {
        if (_activeAnomaly == null || _activeAnomaly.Type != pAnomaly) return null;

        Anomaly lFixedAnomaly = _activeAnomaly;
        lFixedAnomaly.Fix();
        _anomalies.Add(lFixedAnomaly);
        _activeAnomaly = null;

        return lFixedAnomaly;
    }
}

public enum AnomalyType { Missing, Extra, Movement, Replacement}