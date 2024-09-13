using UnityEngine;

public class InputService : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Flag _flag;
    private Base _selectedBase;

    private void Update()
    {
        if (_selectedBase != null && Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            _flag.transform.position = hit.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (_selectedBase == null)
                {
                    if (hit.collider.TryGetComponent(out Base @base))
                    {
                        _selectedBase = @base;
                        _flag = _selectedBase.Flag;
                        _flag.StartPlacing();
                        _selectedBase.StopPrepeareToBuild();
                    }
                }
                else
                {
                    if (_flag.CanPlant())
                    {
                        _flag.Plant();
                        _selectedBase.StartPrepeareToBuild();
                        UnChooseBase();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (_flag != null)
            {
                _flag.gameObject.SetActive(false);
                UnChooseBase();
            }
        }
    }

    private void UnChooseBase()
    {
        _selectedBase = null;
        _flag = null;
    }
}
