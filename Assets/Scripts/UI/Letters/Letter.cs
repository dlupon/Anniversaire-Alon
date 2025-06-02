using UnityEngine;

public class Letter : MonoBehaviour
{
    public string Name;

    public virtual void Show() => gameObject.SetActive(true);

    public virtual void Hide() => gameObject.SetActive(false);
}