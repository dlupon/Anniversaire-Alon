using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HUD : UserInterface
{
    // -------~~~~~~~~~~================# // Components
    [SerializeField] private TextMeshProUGUI _roomText;
    [SerializeField] private TextMeshProUGUI _timeText;

    // -------~~~~~~~~~~================# // Room
    private int _roomIndexOffset = 0;

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Awake()
    {
        EventBus.CameraFadeMid += AskChange;
        EventBus.RoomChanged += UpdateRoom;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Change Room
    public void OnPrevious()
    {
        if (_roomIndexOffset == 0) EventBus.NeedCameraFade?.Invoke();
        _roomIndexOffset -= 1;
    }

    public void OnNext()
    {
        if (_roomIndexOffset == 0) EventBus.NeedCameraFade?.Invoke();
        _roomIndexOffset += 1;
    }

    private void AskChange()
    {
        EventBus.NeedChangeRoom?.Invoke(_roomIndexOffset);
        _roomIndexOffset = 0;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Update

    private void UpdateRoom(Room pRoom)
    {
        _roomText.text = pRoom.name;
    }

    private void UpdateTime(Room pRoom)
    {
        
    }
}
