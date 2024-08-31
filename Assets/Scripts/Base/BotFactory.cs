using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class BotFactory
{
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Base _mainBase;

    public void Initialize(Base currentBase)
    {
        _mainBase = currentBase;
    }

    public Bot Create()
    {
        Bot unit = Object.Instantiate(_botPrefab);
        unit.Initialize(_mainBase);

        return unit;
    }
}
