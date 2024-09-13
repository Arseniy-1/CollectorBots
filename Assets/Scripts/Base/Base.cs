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
    [SerializeField] private ResoursesDataBase _resoursesDataBase;
    [SerializeField] private BaseFactory _baseFactory;

    private int _baseBuildPrice = 5;
    private float _unitSendDelay = 1f;
    private int _resoursesCount = 3;
    private bool _isPrepearingToBuild = false;

    [SerializeField] private List<Bot> _bots = new List<Bot>();

    public event Action<int> ResourseCountChanged;

    [field: SerializeField] public Flag Flag { get; private set; }

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    private void OnEnable()
    {
        ResourseCountChanged?.Invoke(_resoursesCount);
    }

    private void Start()
    {
        TrySpawnBot();
        StartCoroutine(SendingBots());
    }

    public void Initialize(Bot startBot, BaseFactory baseFactory, ResoursesDataBase resoursesDataBase)
    {
        _resoursesDataBase = resoursesDataBase;
        _baseFactory = baseFactory;
        _bots.Add(startBot);
        _resoursesCount = 0;
        StartCoroutine(SendingBots());
    }

    public void StartPrepeareToBuild()
    {
        _isPrepearingToBuild = true;
    }

    public void StopPrepeareToBuild()
    {
        _isPrepearingToBuild = false;
    }

    public void AddResourse(Resourse resourse)
    {
        if (resourse == null)
            return;

        resourse.RaiseDestroy();
        resourse.transform.parent = null;
        _resoursesCount++;
        _resoursesDataBase.RemoveBusyResourse(resourse);
        ResourseCountChanged?.Invoke(_resoursesCount);

        if (_isPrepearingToBuild == false || _bots.Count == 1)
            TrySpawnBot();
    }

    private void TrySpawnBot()
    {
        int price = _botFactory.BotBuildPrice;

        if (_resoursesCount >= price)
        {
            CreateUnit();
            _resoursesCount -= price;

            ResourseCountChanged?.Invoke(_resoursesCount);
        }
    }

    private void EndBuilding(Bot builder)
    {
        builder.BuildCompleted -= EndBuilding;
        Flag.UnSelect();
        _baseFactory.Create(builder);
    }

    private void TryBuildBase()
    {
        if (_resoursesCount >= _baseBuildPrice && _bots[0].IsFree && _bots.Count > 1)
        {
            _resoursesCount -= _baseBuildPrice;
            _bots[0].Follow(Flag);
            _bots[0].BuildCompleted += EndBuilding;
            _bots.RemoveAt(0);
            _isPrepearingToBuild = false;
            ResourseCountChanged?.Invoke(_resoursesCount);
        }
    }

    private void CreateUnit()
    {
        Bot bot = _botFactory.Create();
        bot.Initialize(this);
        bot.transform.position = _spawnPoint.position;
        _bots.Add(bot);
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
        TryBuildBase();
        DistributeResources();
    }

    private List<Resourse> GetFreeResourses()
    {
        List<Resourse> resourses = _scaner.Scan();

        resourses = resourses.Except(_resoursesDataBase.GetBusyResourses).ToList();

        return resourses;
    }
    private void DistributeResources()
    {
        List<Resourse> resourses = GetFreeResourses();

        foreach (Bot bot in _bots)
        {
            if (bot.IsFree && resourses.Count != 0)
            {
                bot.Follow(resourses[0]);
                _resoursesDataBase.AddBusyResourse(resourses[0]);
                resourses.RemoveAt(0);
            }
        }
    }
}
