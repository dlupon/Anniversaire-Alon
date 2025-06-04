using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnomalyHandeler : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Active
    public bool IsActive => ActiveAnomaly != null;

    // -------~~~~~~~~~~================# // Anomaly
    private List<Anomaly> _anomalies = new List<Anomaly>();
    public Anomaly ActiveAnomaly { get; private set; }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        transform.GetComponentsInChildren(_anomalies);
        _anomalies.Sort(new RandomComparer());

        foreach (Anomaly lAnomaly in _anomalies)
            lAnomaly.Room = name;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public Anomaly Trigger()
    {
        if (_anomalies.Count <= 0 || ActiveAnomaly != null) return null;

        ActiveAnomaly = _anomalies[0];
        _anomalies.Remove(ActiveAnomaly);

        ActiveAnomaly.Trigger();

        return ActiveAnomaly;
    }

    public Anomaly Fix(string pAnomaly)
    {
        if (ActiveAnomaly == null || ActiveAnomaly.Type != pAnomaly) return null;

        Anomaly lFixedAnomaly = ActiveAnomaly;
        lFixedAnomaly.Fix();
        _anomalies.Add(lFixedAnomaly);
        ActiveAnomaly = null;

        return lFixedAnomaly;
    }
}

public enum AnomalyType { Missing, Extra, Movement, Replacement, Lighting }