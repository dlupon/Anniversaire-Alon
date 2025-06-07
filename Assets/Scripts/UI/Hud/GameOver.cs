using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnBocal.TweeningSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _gameOverText;

    [SerializeField] private Transform _anomalyContainer;
    [SerializeField] private TextMeshProUGUI _anomalyTextFactory;
    private List<TextMeshProUGUI> _anomalies = new List<TextMeshProUGUI>();

    private Tween _animator = new Tween();

    private void Awake()
    {
        EventBus.GameOver += Show;
        EventBus.GameOverGetAnomalies += SetAnomalies;
    }

    private void OnDestroy()
    {
        EventBus.GameOver -= Show;
        EventBus.GameOverGetAnomalies -= SetAnomalies;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        
        _animator.CompleteAndClear();
        _animator.Color(_background, new Color(0, 0, 0, 0), Color.black, 1f);
        _animator.Whrite(_gameOverText, 1f, pDelay: 1f);

        foreach (TextMeshProUGUI lTextAnomaly in _anomalies)
            ShowAnomaly(lTextAnomaly);

        _animator.Start();

        _gameOverText.text = "";
    }

    private void ShowAnomaly(TextMeshProUGUI pTextAnomaly)
    {
        _animator.Whrite(pTextAnomaly, 1f, pDelay: 1f + _anomalies.IndexOf(pTextAnomaly) * .5f).OnStarted += () => pTextAnomaly.gameObject.SetActive(true);
        pTextAnomaly.text = "";
        pTextAnomaly.gameObject.SetActive(false);
    }

    private void SetAnomalies(List<Anomaly> pAnomalies)
    {
        int lAnomalyCount = pAnomalies.Count;

        if (lAnomalyCount <= 0) return;


        string[] lAnomaliesType = Enum.GetNames(typeof(Anomaly));
        string lType;

        for (int lAnomalyIndex = 0; lAnomalyIndex < lAnomalyCount; lAnomalyIndex++)
        {
            lType = lAnomaliesType.Contains(pAnomalies[lAnomalyIndex].Type) ? pAnomalies[lAnomalyIndex].Type : "???";

            _anomalyTextFactory.text = $"{lType} in {pAnomalies[lAnomalyIndex].Room}";
            _anomalies.Add(_anomalyTextFactory);

            if (lAnomalyIndex >= lAnomalyCount - 1) break;
            _anomalyTextFactory = Instantiate(_anomalyTextFactory, _anomalyContainer);
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }
}
