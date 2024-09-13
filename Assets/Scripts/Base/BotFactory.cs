using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class BotFactory
{
    [SerializeField] private Bot _botPrefab;

    public int BotBuildPrice => _botPrefab.Price;

    public Bot Create() => Object.Instantiate(_botPrefab);
}
