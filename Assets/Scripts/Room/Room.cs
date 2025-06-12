using UnityEngine;

public class Room : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Components
    [SerializeField] private GameObject _room;
    [SerializeField] private Camera _camera;
    [SerializeField] private AnomalyHandeler _anomalyHandeler;
    public AnomalyHandeler AnomalyHandeler => _anomalyHandeler;

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Camera
    private void SetCameraRender(RenderTexture pTexture)
    {
        _camera.targetTexture = pTexture;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Visibility
    public void Show() => _room.SetActive(true);

    public void Hide() => _room.SetActive(false);
}