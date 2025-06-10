using System;
using System.Collections.Generic;
using UnityEngine;

public class Replacement : Anomaly
{
    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private MeshRenderer _target;
    private List<Transform> _otherObjects = new List<Transform>();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    protected override void Start()
    {
        base.Start();
        Type = $"{AnomalyType.Replacement}";

        _target = _target == null ? GetComponent<MeshRenderer>() : _target;
        
        Debug.Log(name);

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

    public override void Trigger()
    {
        base.Trigger();

        Transform lObject = _otherObjects[0];
        _otherObjects.Remove(lObject);
        _otherObjects.Add(lObject);
        
        _target.enabled = false;
        Do((x) => x.gameObject.SetActive(false));
        lObject.gameObject.SetActive(true);
    }

    public override void Fix()
    {
        base.Fix();

        Do((x) => x.gameObject.SetActive(false));
        _target.enabled = true;
    }
}
