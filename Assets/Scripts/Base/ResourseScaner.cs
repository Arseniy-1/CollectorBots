using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseScaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 50f;

    public List<Resourse> Scan()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);
        List<Resourse> resourses = new List<Resourse>();

        foreach (Collider hit in hits)
            if (hit.TryGetComponent(out Resourse resourse) && resourses.Contains(resourse) == false)
                resourses.Add(resourse);
        
        return resourses;
    }
}
