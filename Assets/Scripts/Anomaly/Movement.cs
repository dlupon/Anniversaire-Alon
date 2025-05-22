using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IAnomaly
{
    // -------~~~~~~~~~~================# // Anomaly
    public bool IsActive { get; set; } = false;
    public string Type => $"{Anomaly.Movement}";

    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private Transform _target;
    private List<Transform> _points = new List<Transform>();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        transform.GetComponentsInChildren(_points);
        if (_points.Contains(transform)) _points.Remove(transform);
        _points.Sort(new RandomComparer());
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    public void Trigger()
    {
        IsActive = true;

        Transform lPoint = _points[0];
        _points.Remove(lPoint);
        _points.Add(lPoint);
        
        _target.position = lPoint.position;
    }

    public void Fix()
    {
        IsActive = false;
        _target.localPosition = Vector3.zero;
    }
}
