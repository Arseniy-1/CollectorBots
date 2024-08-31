using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourseScaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 50f;

    public List<Resourse> Scan()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);
        HashSet<Resourse> resourses = new HashSet<Resourse>();

        foreach (Collider hit in hits)
            if (hit.TryGetComponent(out Resourse resourse))
                resourses.Add(resourse);
        
        return resourses.ToList();
    }
}
