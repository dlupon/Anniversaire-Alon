using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenue : MonoBehaviour
{
    private void Awake()
    {
        EventBus.Start += Hide;
        EventBus.ToMain += Show;
    }

    private void OnDestroy()
    {
        EventBus.Start -= Hide;
        EventBus.ToMain -= Show;
    }

    private void Hide() => gameObject.SetActive(false);

    private void Show() => gameObject.SetActive(true);
}
