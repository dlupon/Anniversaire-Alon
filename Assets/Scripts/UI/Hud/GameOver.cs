using System.Collections.Generic;
using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Transform _anomalyContainer;
    [SerializeField] private TextMeshProUGUI _anomalyTextFactory;

    private Tween _animator = new Tween();

    private void Awake()
    {
        EventBus.GameOver += Show;
        EventBus.GameOverGetAnomalies += SetAnomalies;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void SetAnomalies(List<Anomaly> pAnomalies)
    {
        int lAnomalyCount = pAnomalies.Count;

        if (lAnomalyCount <= 0) return;

        for (int lAnomalyIndex = 0; lAnomalyIndex < lAnomalyCount; lAnomalyIndex++)
        {
            Debug.LogError($"{lAnomalyIndex} -> {pAnomalies.Count}");

            _anomalyTextFactory.text = $"{pAnomalies[lAnomalyIndex].Type} in {pAnomalies[lAnomalyIndex].Room}";

            if (lAnomalyIndex >= lAnomalyCount - 1) break;
            _anomalyTextFactory = Instantiate(_anomalyTextFactory, _anomalyContainer);
        }
    }
}
