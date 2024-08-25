using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Scaler))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _currentState;

    private Dictionary<Type, State> _states = new Dictionary<Type, State>();

    private void Awake()
    {
        _states.Add(typeof(Rotator), GetComponent<Rotator>());
        _states.Add(typeof(Mover), GetComponent<Mover>());
        _states.Add(typeof(Scaler), GetComponent<Scaler>());
    }

    private void Start()
    {
        _currentState.Enter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            StartRotate();
        else if (Input.GetKeyDown(KeyCode.Q))
            StartMove();
        else if (Input.GetKeyDown(KeyCode.W))
            StartScale();
    }

    public void StartRotate()
    {
        ChangeState(typeof(Rotator));
    }

    public void StartMove()
    {
        ChangeState(typeof(Mover));
    }

    public void StartScale()
    {
        ChangeState(typeof(Scaler));
    }

    private void ChangeState(Type type)
    {
        _currentState.Exit();
        _currentState = _states[type];
        _currentState.Enter();
    }
}
