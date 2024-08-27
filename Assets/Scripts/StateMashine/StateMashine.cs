using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Idle))]
public class StateMachine : MonoBehaviour
{
    [field: SerializeField] public State CurrentState { get; private set; }

    private Dictionary<Type, State> _states = new Dictionary<Type, State>();
    private Mover _moveState;

    private void Awake()
    {
        _moveState = GetComponent<Mover>();
        _states.Add(typeof(Mover), GetComponent<Mover>());
        _states.Add(typeof(Idle), GetComponent<Idle>());
    }

    private void Start()
    {
        CurrentState.Enter();
    }

    public void StartIdle()
    {
        ChangeState(typeof(Idle));
    }

    public void StartMove(ITarget target)
    {
        _moveState.SelectTarget(target);
        ChangeState(typeof(Mover));
    }

    private void ChangeState(Type type)
    {
        CurrentState.Exit();
        CurrentState = _states[type];
        CurrentState.Enter();
    }
}
