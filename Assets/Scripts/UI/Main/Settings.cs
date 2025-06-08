using UnBocal.TweeningSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Components
    [SerializeField] private Transform _container;

    // -------~~~~~~~~~~================# // Input
    [SerializeField] private Image _backGround;
    private bool _active = false;

    // -------~~~~~~~~~~================# // Animations
    [SerializeField] private float _showDuration = 1f;
    [SerializeField] private float _hideDuration = 1f;

    private Tween _animator = new Tween();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Start()
    {
        _container.localScale = Vector3.zero;
        _backGround.enabled = false;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Visibility
    public void ToggleShowHide()
    {
        _active = !_active;

        if (_active) Show();
        else Hide();
    }

    private void Show()
    {
        // gameObject.SetActive(true);
        _backGround.enabled = true;

        _animator.StopAndClear();

        _animator.Scale(_container, 1, _showDuration, EaseType.OutExpo);
        _animator.Start();
    }

    private void Hide()
    {
        _backGround.enabled = false;

        _animator.StopAndClear();

        _animator.Scale(_container, 0, _showDuration, EaseType.OutExpo);
        _animator.Start();
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Reste
    public void ResetGame()
    {
        PlayerPrefs.SetInt(nameof(Heart), 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
