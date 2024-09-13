using System.Collections.Generic;
using UnityEngine;

public class ResoursesDataBase : MonoBehaviour
{
    List<Resourse> _busyResourses = new List<Resourse>();

    public List<Resourse> GetBusyResourses => _busyResourses;

    public void AddBusyResourse(Resourse resourse)
    {
        if (resourse == null)
            return;

        _busyResourses.Add(resourse);
    }

    public void RemoveBusyResourse(Resourse resourse)
    {
        if (resourse == null)
            return;

        if (_busyResourses.Contains(resourse))
            _busyResourses.Remove(resourse);
    }
}
