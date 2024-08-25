using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourseSpanwer : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 1.0f;
    [SerializeField] private ResoursePool _resoursePool;

    private void Start()
    {
        StartCoroutine(GeneratingResourses());
    }

    private IEnumerator GeneratingResourses()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            yield return wait;
            Spawn();
        }
    }

    public void Spawn()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, 1, transform.position.z);

        Resourse resourse = _resoursePool.Get();
        resourse.OnDisabled += PlaceInPool;

        resourse.transform.position = spawnPoint;
    }

    public void PlaceInPool(Resourse resourse)
    {
        resourse.OnDisabled -= PlaceInPool;
        _resoursePool.Release(resourse);
    }
}
