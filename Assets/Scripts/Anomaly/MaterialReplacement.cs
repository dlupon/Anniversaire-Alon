using System.Collections.Generic;
using System.Linq;
using UnBocal.TweeningSystem;
using UnityEngine;

public class MaterialReplacement : Anomaly
{
    // -------~~~~~~~~~~================# // Movement
    [SerializeField] private MeshRenderer _target;
    [SerializeField] private List<Material> _materials = new List<Material>();
    private Material[] _baseMaterials;
    private int _materialCount;

    // -------~~~~~~~~~~================# // Animation
    private Tween _animator = new Tween();
    [SerializeField] private float _duration = 2f;
    [SerializeField] private EaseType _ease = EaseType.Flat;

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    protected override void Start()
    {
        base.Start();
        Type = $"{AnomalyType.Replacement}";

        _target = _target == null ? GetComponent<MeshRenderer>() : _target;

        _materialCount = _target.materials.Length;
        _baseMaterials = new Material[_materialCount];

        for (int lIndex = 0; lIndex < _materialCount; lIndex++) _baseMaterials[lIndex] = new Material(_target.materials[lIndex]);
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Behaviour
    private Material[] GetMaterials()
    {
        Material[] lMaterials = new Material[_materialCount];
        Material lMaterial;

        int lMaterialIndex = 0;
        do
        {
            lMaterial = lMaterials[lMaterialIndex] = _materials[0];
            _target.materials[lMaterialIndex] = lMaterial;
            _materials.Remove(lMaterial);
            _materials.Add(lMaterial);
        } while (++lMaterialIndex < _materialCount);

        return lMaterials;
    }

    [ContextMenu(nameof(Trigger))]
    public override void Trigger()
    {
        base.Trigger();

        Material[] lTargetMaterials = GetMaterials();
        Material lMaterial;

        _animator.StopAndClear();
        for (int lMaterialIndex = 0; lMaterialIndex < _materialCount; lMaterialIndex++)
        {
            lMaterial = _target.materials[lMaterialIndex];
            lMaterial.mainTexture = lTargetMaterials[lMaterialIndex].mainTexture;
            _animator.Material(lMaterial, _baseMaterials[lMaterialIndex], lTargetMaterials[lMaterialIndex], _duration, _ease);

        }
        _animator.Start();
    }

    [ContextMenu(nameof(Fix))]
    public override void Fix()
    {
        base.Fix();

        _animator.Reset();

        for (int lMaterialIndex = 0; lMaterialIndex < _materialCount; lMaterialIndex++)
            _target.materials[lMaterialIndex].mainTexture = _baseMaterials[lMaterialIndex].mainTexture;
    }
}
