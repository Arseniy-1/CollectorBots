using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, ITarget
{
    [SerializeField] private Bot _unitPrefab;
    [SerializeField] private ResourseScaner _scaner;

    private float _unitSpawnDelay = 2.5f;
    private int _resoursesCount = 0;
    private int _unitsCount = 3;

    private List<Bot> _units = new List<Bot>();

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    private void Start()
    {
        _scaner.Work();
        StartCoroutine(CreatingStartUnits());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resourse resourse))
        {
            resourse.RaiseDestroy();
            _resoursesCount++;
        }
    }

    private IEnumerator CreatingStartUnits()
    {
        WaitForSeconds delay = new WaitForSeconds(_unitSpawnDelay);

        for (int i = 0; i < _unitsCount; i++)
        {
            CreateUnit();
            yield return delay;
        }
    }

    private void CreateUnit()
    {
        Bot unit = Instantiate(_unitPrefab);
        unit.Initialize(this);//
        _units.Add(unit);
    }
}
