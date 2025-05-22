using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _cameraTransform;
    private Vector3 _baseForward;

    private void Start()
    {
        _baseForward = _cameraTransform.forward;
    }

    private void Update()
    {
        
    }
}
