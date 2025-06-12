using TMPro;
using UnBocal.TweeningSystem;
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
    private string _defaultPage;

    // -------~~~~~~~~~~================# // Animation
    [SerializeField] private CanvasGroup _transition;
    private Tween _transitionAnimator = new Tween();
    private Tween _textAnimator = new Tween();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Awake()
    {
        EventBus.CameraFadeMid += AskChange;
        EventBus.RoomChanged += UpdateRoom;
        EventBus.CheckAnomalyDone += ShowDefaultPage;
        EventBus.TimeUpdated += UpdateTime;
        EventBus.Start += OnStart;
    }

    private void OnDestroy()
    {
        EventBus.CameraFadeMid -= AskChange;
        EventBus.RoomChanged -= UpdateRoom;
        EventBus.CheckAnomalyDone -= ShowDefaultPage;
        EventBus.TimeUpdated -= UpdateTime;
        EventBus.Start -= OnStart;
    }

    private void Start()
    {
        if (_pageContainer.childCount > 0)
        {
            _defaultPage = _pageContainer.GetChild(0).name;
            ShowDefaultPage();
        }
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Change Room
    private void OnStart()
    {
        Tween.KillAndClear(_transition);
        _transitionAnimator.Interpolate<float>(_transition, (x) => _transition.alpha = x, 0f, 1f, 1f);
        _transitionAnimator.Start();
    }

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
        Tween.Kill(_roomText);
        _textAnimator.Clear();
        _textAnimator.Whrite(_roomText, pRoom.name, .5f);
        _textAnimator.Start();
    }

    private void UpdateTime(int pHours, int pMinutes) => _timeText.text = $"{FormatTime(pHours)} : {FormatTime(pMinutes)}";

    private string FormatTime(int pTime)
    {
        string lTime = $"{pTime}";
        return lTime.Length <= 1 ? $"0{lTime}" : $"{lTime}";
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Pages
    public void ShowPage(string pPageName)
    {
        int lPageCount = _pageContainer.childCount;
        for (int lPageIndex = 0; lPageIndex < lPageCount; lPageIndex++)
            Show(_pageContainer.GetChild(lPageIndex), pPageName);
    }

    private void ShowDefaultPage() => ShowPage(_defaultPage);

    private void Show(Transform pPage, string pName) => pPage.gameObject.SetActive(pPage.name == pName);
}
