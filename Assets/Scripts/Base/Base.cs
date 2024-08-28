using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, ITarget
{
    [SerializeField] private Bot _unitPrefab;
    [SerializeField] private ResourseScaner _scaner;

    private float _unitSendDelay = 1f;
    private float _unitSpawnDelay = 2.5f;
    private int _resoursesCount = 0;
    private int _unitsCount = 3;

    private List<Bot> _units = new List<Bot>();

    public Transform Transform { get; private set; }

    public event Action<int> OnResourseCountChanged;

    private void Awake()
    {
        Transform = transform;
    }

    private void Start()
    {
        StartCoroutine(CreatingStartUnits());
        StartCoroutine(SendingBots());
    }

    public void GetResourse(Resourse resourse)
    {
        resourse.RaiseDestroy();
        resourse.transform.parent = null;
        _resoursesCount++;
        OnResourseCountChanged?.Invoke(_resoursesCount);
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
        unit.Initialize(this);
        _units.Add(unit);
    }

    private IEnumerator SendingBots()
    {
        WaitForSeconds delay = new WaitForSeconds(_unitSendDelay);

        while (enabled)
        {
            yield return delay;
            SendBots();
        }
    }

    private void SendBots()
    {
        List<Resourse> resourses = _scaner.Scan();
        List<Resourse> busyResourses = new List<Resourse>();

        foreach (Resourse resourse in resourses)
        {
            foreach (Bot bot in _units)
            {
                if (bot.HasResourse(resourse))
                {
                    busyResourses.Add(resourse);
                    break;
                }
            }
        }

        for(int i = resourses.Count - 1; i >= 0; i--)
        {
            if (busyResourses.Contains(resourses[i]))
            {
                resourses.RemoveAt(i);
            }
        }

        foreach (Bot bot in _units)
            if (bot.GetCurrentState() is Idle && resourses.Count != 0)
                bot.Follow(resourses[0]);
    }
}
