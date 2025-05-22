using System.Collections.Generic;
using UnityEngine;

public class AnomalyHandeler : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Active
    public bool IsActive => _activeAnomalies.Count > 0;

    // -------~~~~~~~~~~================# // Anomaly
    private List<IAnomaly> _anomalies = new List<IAnomaly>();
    private List<IAnomaly> _activeAnomalies = new List<IAnomaly>();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        transform.GetComponentsInChildren(_anomalies);
        _anomalies.Sort(new RandomComparer());
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public IAnomaly Trigger()
    {
        if (_anomalies.Count <= 0) return null;

        IAnomaly lCurrent = _anomalies[0];
        _anomalies.Remove(lCurrent);
        _activeAnomalies.Add(lCurrent);

        lCurrent.Trigger();

        return lCurrent;
    }

    public bool Fix(IAnomaly pAnomaly)
    {
        if (pAnomaly == null || !_activeAnomalies.Contains(pAnomaly)) return false;

        pAnomaly.Fix();

        _activeAnomalies.Remove(pAnomaly);
        _anomalies.Add(pAnomaly);

        return true;
    }
}

public enum Anomaly { Movement, Floating, Picture, Intruder, Other }