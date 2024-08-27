using System.Collections;
using UnityEngine;

public class ResourseSpanwer : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 1.5f;
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
        float radiusMultiplyer = 10f;
        float spawnHeight = 0.5f;
        Vector3 spawnPoint = Random.insideUnitSphere * radiusMultiplyer;
        spawnPoint.y = spawnHeight;

        Resourse resourse = _resoursePool.Get();
        resourse.OnDisabled += PlaceInPool;

        Debug.Log("sub");
        resourse.transform.position = spawnPoint;
    }

    public void PlaceInPool(Resourse resourse)
    {
        resourse.OnDisabled -= PlaceInPool;
        _resoursePool.Release(resourse);
    }
}
