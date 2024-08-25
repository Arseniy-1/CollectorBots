using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private ITarget _base;
    [SerializeField] private float _distanceToInterract;
    [SerializeField] private float _speed;

    private StateMachine _stateMachine; 

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

    private void Awake()
    {
        _stateMachine = new StateMachineFactory(this).Create();
    }

    internal void Put() => throw new NotImplementedException();

    public void Move()
    {
        transform.LookAt(CurrentTarget.Transform.position);
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void Collect(Resourse resourse)
    {
        resourse.transform.position = _hand.transform.position;
        resourse.transform.rotation = _hand.transform.rotation;
        resourse.transform.parent = transform;
    }

    public void Initialize(ITarget target)
    {
        _base = target;
    }
}
