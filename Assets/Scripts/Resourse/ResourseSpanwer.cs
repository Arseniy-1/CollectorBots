using System.Collections;
using UnityEngine;

public class ResourseSpanwer : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 1.5f;
    [SerializeField] private ResoursePool _resoursePool;
    [SerializeField] private LayerMask _baseLayerMask;
    [SerializeField] private float _spawnRadius;

    private float _resourseCheckRadius = 4;

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

    private Vector3 GenerateRandomPosition()
    {
        float spawnHeight = 0.5f;
        Vector3 spawnPoint = Random.insideUnitSphere * _spawnRadius;
        spawnPoint.y = spawnHeight;

        return spawnPoint;
    }

    private void Spawn()
    {
        Vector3 spawnPosition = GenerateRandomPosition();

        while (Physics.OverlapSphere(spawnPosition, _resourseCheckRadius, _baseLayerMask).Length != 0)
        {
            spawnPosition = GenerateRandomPosition();
        }

        Resourse resourse = _resoursePool.Get();
        resourse.OnDisabled += PlaceInPool;

        resourse.transform.position = spawnPosition;
    }

    public void PlaceInPool(Resourse resourse)
    {
        resourse.OnDisabled -= PlaceInPool;
        _resoursePool.Release(resourse);
    }
}
