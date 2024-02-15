using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager
{
    bool GetLeftMouseDown();
    Vector2 GetCameraRotate();
    Vector2 GetPlayerMovement();
    bool GetShiftDown();
    bool GetFlashButtonDown();
}

