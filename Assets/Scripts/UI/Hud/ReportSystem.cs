using System;
using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;
using UnityEngine.UI;

public class ReportSystem : MonoBehaviour
{
    [SerializeField] private Transform _buttonContainer;
    [SerializeField] private Button _buttonFactory;

    private Tween _animator = new Tween();

    private void Start()
    {
        UpdateAnomalies();
    }

    private void OnEnable()
    {
        PlayFadeAnimation();
    }

    private void UpdateAnomalies()
    {
        string[] lAllNames = Enum.GetNames(typeof(Anomaly));

        foreach (string lName in lAllNames)
            CreateButton(lName);
    }

    private void CreateButton(string pName)
    {
        Button lCurrentButton = Instantiate(_buttonFactory, _buttonContainer);
        lCurrentButton.GetComponentInChildren<TextMeshProUGUI>().text = lCurrentButton.name = pName;
        lCurrentButton.onClick.AddListener(() => Report(pName));
    }

    private void Report(string pName)
    {
        EventBus.Report?.Invoke();
        EventBus.ReportAnomaly?.Invoke(pName);
    }

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

        float lDuration = .4f;
        float lDelay = .1f * pIndex;

        _animator.Interpolate<float>(lCanva, (x) => lCanva.alpha = x, 0f, 1f, lDuration, EaseType.Flat, lDelay);
        _animator.Whrite(lText, lCanva.name, lDuration, pDelay : lDelay);

        lCanva.alpha = 0;
        lText.text = "";
    }
}
