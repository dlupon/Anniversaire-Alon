using System.Collections.Generic;
using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _houre;
    [SerializeField] private TextMeshProUGUI _winMessage;

    private Tween _animator = new Tween();

    private void Awake()
    {
        EventBus.TimeEnded += Show;
    }

    private void OnDestroy()
    {
        EventBus.TimeEnded -= Show;
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
        _animator.Whrite(_houre, 2.5f, pDelay: 1f);
        _animator.Whrite(_winMessage, 2f, pDelay: 3f);

        _animator.Start();

        _winMessage.text = "";
        _houre.text = "";
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }
}
