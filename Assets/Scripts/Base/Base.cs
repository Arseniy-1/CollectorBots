using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourseScaner))]
public class Base : MonoBehaviour, ITarget
{
    [SerializeField] private ResourseScaner _scaner;
    [SerializeField] private BotFactory _botFactory;
    [SerializeField] private Transform _spawnPoint;

    private float _unitSendDelay = 1f;
    private float _unitSpawnDelay = 2.5f;
    private int _resoursesCount = 0;
    private int _unitsCount = 3;

    private List<Bot> _units = new List<Bot>();

    public Transform Transform { get; private set; }

    public event Action<int> ResourseCountChanged;

    private void Awake()
    {
        Transform = transform;
    }

    private void Start()
    {
        StartCoroutine(CreatingStartUnits());
        StartCoroutine(SendingBots());
    }

    public void AddResourse(Resourse resourse)
    {
        resourse.RaiseDestroy();
        resourse.transform.parent = null;
        _resoursesCount++;
        ResourseCountChanged?.Invoke(_resoursesCount);
    }

    private IEnumerator CreatingStartUnits()
    {
        WaitForSeconds delay = new WaitForSeconds(_unitSpawnDelay);

        for (int i = 0; i < _unitsCount; i++)
        {
            Bot bot = _botFactory.Create();
            bot.Initialize(this);
            bot.transform.position = _spawnPoint.position;
            _units.Add(bot);

            yield return delay;
        }
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
        {
            if (bot.IsFree && resourses.Count != 0)
            {
                bot.Follow(resourses[0]);
                resourses.RemoveAt(0);
            }
        }
    }
}
