using UnityEngine;
using System;

public class Resourse : MonoBehaviour, ITarget
{
    public event Action<Resourse> OnDisabled;

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    public void RaiseDestroy()
    {
        OnDisabled?.Invoke(this);
    }
}
