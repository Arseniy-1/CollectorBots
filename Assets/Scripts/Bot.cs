using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Base _base;

    [SerializeField] private StateMachine _stateMachine;

    [field: SerializeField] public Resourse CurrentResourse { get; private set; } = null;
    [field: SerializeField] public ITarget CurrentTarget { get; private set; }
    public bool IsFree => _stateMachine.CurrentState is Idle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITarget target))
            Interact(target);
    }

    public bool HasResourse(Resourse resourse)
    {
        return CurrentResourse == resourse || (CurrentTarget as Resourse) == resourse;
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
        else if (target is Base mainBase && mainBase == _base)
        {
            mainBase.AddResourse(CurrentResourse);
            CurrentResourse = null;
            Stay();
        }
    }

    private void Stay()
    {
        _stateMachine.StartIdle();
    }
}
