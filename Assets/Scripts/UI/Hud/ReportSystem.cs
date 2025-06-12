using System;
using System.Linq;
using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;
using UnityEngine.UI;

public class ReportSystem : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Components
    [SerializeField] private Transform _buttonContainer;
    [SerializeField] private Button _buttonFactory;
    [SerializeField] private GameObject _inputBlock;

    // -------~~~~~~~~~~================# // Input
    private Button _additionnalButton;
    private bool _isInputReactive = true;

    // -------~~~~~~~~~~================# // Animations
    private Tween _animator = new Tween();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Awake()
    {
        EventBus.CheckAnomalyDone += EnableInput;
        EventBus.GetAnomalyHandeler += UpdateAnomalies;
    }

    private void OnDestroy()
    {
        EventBus.CheckAnomalyDone -= EnableInput;
        EventBus.GetAnomalyHandeler -= UpdateAnomalies;
    }

    private void Start()
    {
        CreateButtons();
        EnableInput();
    }

    private void OnEnable()
    {
        PlayFadeAnimation();
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Anomalies
    private void UpdateAnomalies(AnomalyHandeler pAnomalyHandeler)
    {
        Anomaly lCurrentAnomaly = pAnomalyHandeler.ActiveAnomaly;

        bool pEnable = lCurrentAnomaly != null && !Enum.GetNames(typeof(AnomalyType)).Contains(lCurrentAnomaly.Type) ?
            lCurrentAnomaly.Type != "None" : false;

        if (pEnable) _additionnalButton = CreateButton(_additionnalButton, lCurrentAnomaly.Type);

        _additionnalButton?.gameObject.SetActive(pEnable);
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Buttons
    private void CreateButtons()
    {
        string[] lAllNames = Enum.GetNames(typeof(AnomalyType));

        foreach (string lName in lAllNames)
            CreateButton(lName);

        _additionnalButton = CreateButton("None");
        _additionnalButton.gameObject.SetActive(false);
    }

    private Button CreateButton(string pName = "None") => CreateButton(Instantiate(_buttonFactory, _buttonContainer), pName);

    private Button CreateButton(Button pButton, string pName)
    {
        pButton.GetComponent<TextMeshProUGUI>().text = pButton.name = pName;
        pButton.onClick.RemoveAllListeners();
        pButton.onClick.AddListener(() => Report(pName));

        return pButton;
    }


    private void Report(string pName)
    {
        DisableInput();
        
        EventBus.Report?.Invoke();
        EventBus.ReportAnomaly?.Invoke(pName);
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Input
    private void EnableInput() => _inputBlock.SetActive(!(_isInputReactive = true));

    private void DisableInput() => _inputBlock.SetActive(!(_isInputReactive = false));

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Animations
    private void PlayFadeAnimation()
    {
        _animator.CompleteAndClear();

        int lButtonCount = _buttonContainer.childCount;

        for (int lButtonIndex = 0; lButtonIndex < lButtonCount; lButtonIndex++)
            AddFade(lButtonIndex);

        _animator.Start();
    }

    private void AddFade(int pIndex)
    {
        CanvasGroup lCanva = _buttonContainer.GetChild(pIndex).GetComponent<CanvasGroup>();
        TextMeshProUGUI lText = lCanva.GetComponentInChildren<TextMeshProUGUI>();

        float lDuration = .25f;
        float lDelay = .1f * pIndex;

        _animator.Interpolate<float>(lCanva, (x) => lCanva.alpha = x, 0f, 1f, lDuration, EaseType.Flat, lDelay);
        _animator.Whrite(lText, lCanva.name, lDuration, pDelay : lDelay);

        lCanva.alpha = 0;
        lText.text = "";
    }
}
