using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class ResoursePool
{
    [SerializeField] private int _poolCapacity;
    [SerializeField] private Resourse _resoursePrefab;

    private Queue<Resourse> _resourses = new Queue<Resourse>();

    public ResoursePool()
    {
        for (int i = 0; i < _poolCapacity; i++)
            ExpandPool();
    }

    public Resourse Get()
    {
        if (_resourses.Count == 0)
            ExpandPool();

        Resourse resourse = _resourses.Dequeue();
        resourse.transform.rotation = Quaternion.identity;
        resourse.gameObject.SetActive(true);

        return resourse;
    }

    public void Release(Resourse resourse)
    {
        resourse.gameObject.SetActive(false);
        _resourses.Enqueue(resourse);
    }

    private void ExpandPool()
    {
        Resourse resourse = Object.Instantiate(_resoursePrefab);
        resourse.gameObject.SetActive(false);
        _resourses.Enqueue(resourse);
    }
}
