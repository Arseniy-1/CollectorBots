using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Base _base;

    [SerializeField] private StateMachine _stateMachine;

    public event Action<Bot> BuildCompleted;

    [field: SerializeField] public int Price { get; private set; } = 3;
    [field: SerializeField] public Resourse CurrentResourse { get; private set; } = null;
    [field: SerializeField] public ITarget CurrentTarget { get; private set; }
    public bool IsFree => _stateMachine.CurrentState is Idle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITarget target))
            Interact(target);
    }

    public void Follow(ITarget targetResourse)
    {
        CurrentTarget = targetResourse;
        _stateMachine.StartMove(CurrentTarget);
    }

    public void Initialize(Base mainBase)
    {
        _base = mainBase;
    }

    public void  SelectBase(Base @base)
    {
        _base = @base;
    }

    private void Interact(ITarget target)
    {
        if (target is Resourse resourse)
        {
            if (target == CurrentTarget)
            {
                resourse.transform.parent = transform;
                resourse.transform.position = _hand.transform.position;
                CurrentResourse = resourse;
                Follow(_base);
            }
        }
        else if (target is Base @base)
        {
            if (@base == _base && CurrentResourse != null)
            {
                @base.AddResourse(CurrentResourse);
                CurrentResourse = null;
                Stay();
            }
        }
        else if (target is Flag )
        {
            if (target == CurrentTarget)
            {
                Build();
                Stay();
            }
        }
    }

    private void Build()
    {
        BuildCompleted?.Invoke(this);
    }

    private void Stay()
    {
        _stateMachine.StartIdle();
    }
}
