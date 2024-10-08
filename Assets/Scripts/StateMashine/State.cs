using UnityEngine;

public abstract class State : MonoBehaviour
{
    private void Awake()
    {
        enabled = false;
        OnAwake();
    }

    public virtual void Enter() =>
        enabled = true;

    public virtual void Exit() =>
        enabled = false;

    protected virtual void OnAwake() { }
}