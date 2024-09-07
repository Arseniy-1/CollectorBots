using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public event Action<int> ResourseCountChanged;

    public Transform Transform { get; private set; }

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

        IEnumerable<Resourse> TargetResourses = _units
            .Where(unit => unit.CurrentTarget is Resourse)
            .Select(unit => unit.CurrentTarget)
            .Cast<Resourse>();

        IEnumerable<Resourse> TakenResourses = _units
            .Where(unit => unit.CurrentResourse != null)
            .Select(unit => unit.CurrentResourse);

        IEnumerable<Resourse> busyResourses = TargetResourses.Concat(TakenResourses);

        resourses = resourses.Except(busyResourses).ToList();

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
