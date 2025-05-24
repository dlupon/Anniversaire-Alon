using System;
using System.Collections.Generic;
using UnityEngine;

public class Replacement : MonoBehaviour, IAnomaly
{
    // -------~~~~~~~~~~================# // Anomaly
    public bool IsActive { get; set; } = false;
    public string Type => $"{Anomaly.Replacement}";

    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private MeshRenderer _target;
    private List<Transform> _otherObjects = new List<Transform>();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        transform.GetComponentsInChildren(_otherObjects);
        if (_otherObjects.Contains(transform)) _otherObjects.Remove(transform);
        _otherObjects.Sort(new RandomComparer());

        Do((x) => x.gameObject.SetActive(false));
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    private void Do(Action<Transform> pMethod)
    {
        foreach (Transform pObjet in _otherObjects) pMethod(pObjet);
    }

    public void Trigger()
    {
        Debug.Log(name);
        IsActive = true;

        Transform lObject = _otherObjects[0];
        _otherObjects.Remove(lObject);
        _otherObjects.Add(lObject);
        
        _target.enabled = false;
        Do((x) => x.gameObject.SetActive(false));
        lObject.gameObject.SetActive(true);
    }

    public void Fix()
    {
        IsActive = false;

        Do((x) => x.gameObject.SetActive(false));
        _target.enabled = true;
    }
}
