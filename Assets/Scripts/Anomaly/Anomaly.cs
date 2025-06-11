using UnityEngine;

public class Anomaly : MonoBehaviour
{
    public bool IsActive { private set; get; }

    public string Type { protected set; get; }
    [HideInInspector] public string Room;

    protected virtual void Start() {}

    public virtual void Trigger()
    {
        IsActive = true;
        Debug.Log($"<color=#fcba03>{nameof(Anomaly)}</color> :" +
            $" <color=#46e0e0>{Room} -> {Type} {name}</color> ->" +
            $" <color=#d17ffa>Triggered</color>");
    }

    public virtual void Fix()
    {
        IsActive = false;
        Debug.Log($"<color=#fcba03>{nameof(Anomaly)}</color> :" +
            $" <color=#46e0e0>{Room} -> {Type} {name}</color> ->" +
            $" <color=#d17ffa>Fixed</color>");
    }
}
