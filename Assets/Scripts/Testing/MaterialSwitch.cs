using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitch : MonoBehaviour
{
    [SerializeField] MeshRenderer _MR;

    [SerializeField] Material[] _materials;

    void Start()
    {
        _MR.materials = _materials;
    }
}
