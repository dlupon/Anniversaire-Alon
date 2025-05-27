using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiAnomaly : Anomaly
{
    // -------~~~~~~~~~~================# // Anomaly
    [SerializeField] private bool _isCustomType = false;
    [SerializeField] private string _customType = "Custom";
    [SerializeField] private AnomalyType _type = AnomalyType.Movement;

    [SerializeField] private List<Anomaly> _anomalies = new List<Anomaly>();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        Type = _isCustomType ? _customType : $"{_type}";
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public override void Trigger()
    {
        base.Trigger();
        foreach (Anomaly lAnomaly in _anomalies) lAnomaly.Trigger();
    }

    public override void Fix()
    {
        base.Fix();
        foreach (Anomaly lAnomaly in _anomalies) lAnomaly.Fix();
    }
}
