using System.Collections.Generic;
using UnityEngine;

public class AnomalyHandeler : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Active
    public bool IsActive => ActiveAnomaly != null;

    // -------~~~~~~~~~~================# // Anomaly
    private List<Anomaly> _anomalies = new List<Anomaly>();
    private List<Anomaly> _heart = new List<Anomaly>();
    public Anomaly ActiveAnomaly { get; private set; }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        GetAnomalies();
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Anomaly
    private void GetAnomalies()
    {
        _anomalies.Clear();
        transform.GetComponentsInChildren(_anomalies);
        _anomalies.Sort(new RandomComparer());
        
        Anomaly lAnomaly;

        for (int lAnomalyIndex = _anomalies.Count - 1; lAnomalyIndex  >= 0; lAnomalyIndex--)
        {
            lAnomaly = _anomalies[lAnomalyIndex];
            lAnomaly.Room = name;

            if (lAnomaly.Type != nameof(Heart)) continue;

            _anomalies.Remove(lAnomaly);
            _heart.Add(lAnomaly);
        }
    }

    public string GetAnomaly() => ActiveAnomaly == null ? "None" : ActiveAnomaly.Type;

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public Anomaly Trigger(bool pHeart = false)
    {
        if (pHeart && _heart.Count > 0)
        {
            ActiveAnomaly = _heart[0];
            _heart.Remove(ActiveAnomaly);
        }
        else
        {
            if (_anomalies.Count <= 0) return null;

            ActiveAnomaly = _anomalies[0];
            _anomalies.Remove(ActiveAnomaly);

        }

        ActiveAnomaly.Trigger();

        return ActiveAnomaly;
    }

    public void Fix()
    {
        if (ActiveAnomaly == null) return;
        
        ActiveAnomaly.Fix();

        if (ActiveAnomaly.Type != nameof(Heart)) _anomalies.Add(ActiveAnomaly);
        else _heart.Add(ActiveAnomaly);

        ActiveAnomaly = null;
    }

    public Anomaly Fix(string pAnomaly)
    {
        if (ActiveAnomaly == null || ActiveAnomaly.Type != pAnomaly) return null;

        Anomaly lActiveAnomaly = ActiveAnomaly;

        Fix();

        return lActiveAnomaly;
    }
}

public enum AnomalyType { Missing, Extra, Movement, Replacement, Lighting }