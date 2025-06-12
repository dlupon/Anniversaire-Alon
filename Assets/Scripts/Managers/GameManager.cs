using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        EventBus.Start += Show;
        EventBus.ToMain += Hide;
    }

    private void OnDestroy()
    {
        EventBus.Start -= Show;
        EventBus.ToMain -= Hide;
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        StartCoroutine(nameof(HideAfterInit));
    }

    private void Hide() => gameObject.SetActive(false);

    private void Show() => gameObject.SetActive(true);

    private IEnumerator HideAfterInit()
    {
        yield return new WaitForEndOfFrame();
        EventBus.ToMain.Invoke();
        Debug.Log("To Main");
    }
}