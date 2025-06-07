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
        transform.GetComponentsInChildren(_anomalies);
        _anomalies.Sort(new RandomComparer());

        foreach (Anomaly lAnomalyOrHeart in _anomalies)
            lAnomalyOrHeart.Room = name;
        
        Anomaly lAnomaly;

        for (int lAnomalyIndex = _anomalies.Count - 1; lAnomalyIndex  >= 0; lAnomalyIndex--)
        {
            lAnomaly = _anomalies[lAnomalyIndex];

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
            ActiveAnomaly = _anomalies[0];
            _anomalies.Remove(ActiveAnomaly);

            if (_anomalies.Count <= 0) return null;
        }

        ActiveAnomaly.Trigger();

        return ActiveAnomaly;
    }

    public Anomaly Fix(string pAnomaly)
    {
        if (ActiveAnomaly == null || ActiveAnomaly.Type != pAnomaly) return null;

        Anomaly lFixedAnomaly = ActiveAnomaly;
        ActiveAnomaly = null;

        lFixedAnomaly.Fix();

        if (lFixedAnomaly.Type != nameof(Heart)) _anomalies.Add(lFixedAnomaly);
        else _heart.Add(lFixedAnomaly);

        return lFixedAnomaly;
    }
}

public enum AnomalyType { Missing, Extra, Movement, Replacement, Lighting }