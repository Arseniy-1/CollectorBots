using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseScaner : MonoBehaviour
{
    [SerializeField] private float _scanDelay = 1.5f;
    [SerializeField] private float _scanRadius = 50f;

    private Queue<Resourse> _resourses = new Queue<Resourse>();

    public Queue<Resourse> GetResourses => _resourses; //Безопасность

    public void Work()
    {
        StartCoroutine(Scaning());
    }

    private void Scan()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

        foreach (Collider hit in hits)
            if (hit.TryGetComponent(out Resourse resourse) && _resourses.Contains(resourse) == false)
                _resourses.Enqueue(resourse);
    }

    private IEnumerator Scaning()
    {
        WaitForSeconds delay = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            Scan();

            yield return delay;
        }
    }
}
