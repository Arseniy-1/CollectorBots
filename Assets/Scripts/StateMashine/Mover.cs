using UnityEngine;

public class Mover : State
{
    [SerializeField] private float _speed;

    private Transform _currentTarget;

    private void Update() => Move();

    public void SelectTarget(ITarget currentTarget)
    {
        if(currentTarget is MonoBehaviour target) 
            _currentTarget = target.transform;
    }

    private void Move()
    {
        Vector3 direction = _currentTarget.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speed * Time.deltaTime);

        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}