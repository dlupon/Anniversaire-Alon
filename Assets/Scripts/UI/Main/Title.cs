using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Game";

    public void LoadGame()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
