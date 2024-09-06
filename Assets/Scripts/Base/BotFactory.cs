using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class BotFactory
{
    [SerializeField] private Bot _botPrefab;

    public Bot Create()
    {
        Bot unit = Object.Instantiate(_botPrefab);

        return unit;
    }
}
