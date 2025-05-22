using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Components
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _roomText;
    [SerializeField] private TextMeshProUGUI _timeText;

    // -------~~~~~~~~~~================# // Room
    private int _roomIndexOffset = 0;

    // -------~~~~~~~~~~================# // Pages
    [Header("Pages")]
    [SerializeField] private Transform _pageContainer;
    private int _currentPageIndex = 0;

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

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Pages
    public void TooglePages()
    {
        int lPageCount = _pageContainer.childCount;
        int lOldIndex = _currentPageIndex;
        int lNewIndex = (lOldIndex + 1) % _pageContainer.childCount;

        if (lOldIndex >= lPageCount || lNewIndex >= lPageCount) return;

        _pageContainer.GetChild(lOldIndex).gameObject.SetActive(false);
        _pageContainer.GetChild(lNewIndex).gameObject.SetActive(true);

        _currentPageIndex = lNewIndex;
    }
}
