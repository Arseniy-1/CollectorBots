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

        //Vector3 targeeeeet = currentTarget.Transform.position;
        //Debug.Log(targeeeeet);
    }

    private void Move()
    {
        transform.LookAt(_currentTarget.position);
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}