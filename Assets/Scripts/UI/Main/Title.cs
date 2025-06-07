using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Game";
    [SerializeField] private TextMeshProUGUI _version;

    [SerializeField] private Image _transition;

    private void Start()
    {
        _version.text = _version.text.Replace("{v}", Application.version);
        _transition.gameObject.SetActive(false);
    }

    public void LoadGame()
    {
        _transition.gameObject.SetActive(true);
        Tween lAnimator = new Tween();

        lAnimator.Color(_transition, new Color(0, 0, 0, 0), Color.black, 1.5f).OnFinished += () => SceneManager.LoadScene(_sceneName);
        lAnimator.Start();
    }
}
