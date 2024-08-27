using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private ITarget _base;
    [SerializeField] private float _distanceToInterract;
    [SerializeField] private float _speed;

    [SerializeField] private StateMachine _stateMachine;

    public ITarget CurrentTarget { get; private set; }

    public bool IsNearestToTarget
    {
        get
        {
            if (CurrentTarget == null)
                return true;

            return Vector3.Distance(transform.position, CurrentTarget.Transform.position) <= _distanceToInterract;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITarget target))
        {
            if (target == CurrentTarget)
            {
                Interact(target);
            }

            else if(target == _base)
            {

            }
        }
    }

    public State GetCurrentState() => _stateMachine.CurrentState;

    public bool HasResourse(Resourse resourse)
    {
        return resourse == GetComponentInChildren<Resourse>() || (CurrentTarget as Resourse) == resourse;
    }

    public void Follow(ITarget targetResourse)
    {
        CurrentTarget = targetResourse;
        _stateMachine.StartMove(CurrentTarget);
    }

    public void Initialize(ITarget target)
    {
        _base = target;
    }

    private void Interact(ITarget target)
    {
        if (target is Resourse resourse)
        {
            resourse.transform.parent = transform;
            resourse.transform.position = _hand.transform.position;
            Follow(_base);
        }
        else if (target is Base)
        {
            Stay();
        }
    }

    private void Stay()
    {
        _stateMachine.StartIdle();
    }
}
