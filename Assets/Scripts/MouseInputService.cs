using System;
using UnityEngine;

public class MouseInputService : MonoBehaviour
{
    public event Action RightMouseButtonClicked;
    public event Action LeftMouseButtonClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(GameConstants.LeftMouseButtonIndex))
            LeftMouseButtonClicked?.Invoke();
        else if (Input.GetMouseButtonDown(GameConstants.RightMouseButtonIndex))
            RightMouseButtonClicked?.Invoke();
    }
}
