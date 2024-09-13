using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Flag : MonoBehaviour, ITarget
{
    [SerializeField] private LayerMask _baseLayerMask;
    [SerializeField] private Collider _collider;

    private float _spawnRadius = 1;

    public Transform Transform { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void StartPlacing()
    {
        _collider.enabled = false;
        gameObject.SetActive(true);
    }

    public void UnSelect()
    {
        _collider.enabled = true;
        gameObject.SetActive(false);
    }

    public void Plant()
    {
        _collider.enabled = true;
    }

    public bool CanPlant() => 
        Physics.OverlapSphere(transform.position, _spawnRadius, _baseLayerMask).Length == 0;
}
