using UnityEngine;

public class OldInputManager : MonoBehaviour, IInputManager
{
    public static OldInputManager Instance { get; private set; }
    Transform cameraTransform;
    float rotationSpeed = 10f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        cameraTransform = Camera.main.transform;
    }

    public bool GetLeftMouseDown()
    {
        return Input.GetMouseButtonDown(0);
    }

    public Vector2 GetMousePosition()
    {
        return Input.mousePosition;
    }

    public Vector2 GetCameraRotate()
    {
        float rotateX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotateY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        rotateY = Mathf.Clamp(rotateY, -40f, 40f);
        cameraTransform.Rotate(Vector3.up, rotateX);
        cameraTransform.Rotate(Vector3.right, rotateY);

        return new Vector2(rotateX, rotateY);
    }

    public Vector2 GetPlayerMovement()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public bool GetShiftDown()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    public bool GetFlashButtonDown()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}