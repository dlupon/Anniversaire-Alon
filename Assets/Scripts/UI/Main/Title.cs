using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Game";
    [SerializeField] private TextMeshProUGUI _version;

    private void Start()
    {
        _version.text = _version.text.Replace("{v}", Application.version); ;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
