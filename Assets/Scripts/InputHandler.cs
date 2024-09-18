using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private MouseInputService _inputService;

    private Flag _flag;
    private Base _selectedBase;

    private void OnEnable()
    {
        _inputService.RightMouseButtonClicked += OnRightMouseButtonClick;
        _inputService.LeftMouseButtonClicked += OnLeftMouseButtonClick;
    }

    private void OnDisable()
    {
        _inputService.RightMouseButtonClicked -= OnRightMouseButtonClick;
        _inputService.LeftMouseButtonClicked -= OnLeftMouseButtonClick;
    }

    private void Update()
    {
        if (_selectedBase != null && Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            _flag.transform.position = hit.point;
        }
    }

    private void OnLeftMouseButtonClick()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
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

    private void OnRightMouseButtonClick()
    {
        if (_flag != null)
        {
            _flag.gameObject.SetActive(false);
            UnChooseBase();
        }
    }

    private void UnChooseBase()
    {
        _selectedBase = null;
        _flag = null;
    }
}
