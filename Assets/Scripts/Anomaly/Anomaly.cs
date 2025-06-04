using UnityEngine;

public class Anomaly : MonoBehaviour
{
    public bool IsActive { private set; get; }

    public string Type { protected set; get; }
    public string Room;

    public virtual void Trigger()
    {
        IsActive = true;
        Debug.Log($"{Type} {name} Triggered");
    }

    public virtual void Fix()
    {
        IsActive = false;
        Debug.Log($"{Type} {name} Fixed");
    }
}
