using System;
using UnityEngine;

[Serializable]
public class BaseFactory : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;
    [SerializeField] private BusyResoursesDataBase _resoursesDataBase;    

    public void Create(Bot bot)
    {
        Base createdBase = Instantiate(_basePrefab, bot.transform.position, transform.rotation);
        createdBase.Initialize(bot, this, _resoursesDataBase);
        bot.SelectBase(createdBase);
    }
}
