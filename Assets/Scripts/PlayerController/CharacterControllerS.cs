using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerS : MonoBehaviour
{
    private static CharacterControllerS instance;
    private CharacterController characterController;
    private IInputManager inputManager;

    public float walkSpeed = 3f;
    public float sprintSpeed = 8f;
    public float sprintAcceleration = 2f;
    public float stoppingSpeed = 6f;
    public float currentSpeed;
    private bool isSprinting;

    public static CharacterControllerS Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterControllerS>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("CharacterControllerS");
                    instance = obj.AddComponent<CharacterControllerS>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        characterController = GetComponent<CharacterController>();
        inputManager = FindObjectOfType<OldInputManager>();

        // inputManager = FindObjectOfType<NewInputManager>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleActions();
    }

    private void HandleMovement()
    {
        Vector2 movementInput = inputManager.GetPlayerMovement();
        Vector3 moveDirection = transform.forward * movementInput.y + transform.right * movementInput.x;
        float speed = isSprinting ? currentSpeed : walkSpeed;
        characterController.Move(moveDirection * speed * Time.deltaTime);
        Debug.Log(speed);
    }

    private void HandleRotation()
    {
        Vector2 rotateInput = inputManager.GetCameraRotate();
        transform.Rotate(Vector3.up, rotateInput.x);
        transform.Rotate(Vector3.right, rotateInput.y);
    }

    private void HandleActions()
    {
        if (inputManager.GetFlashButtonDown())
        {
            TurnFlashlight();
        }

        if (inputManager.GetShiftDown())
        {
            StartSprint();
            Debug.Log("sprint");
        }
        else
        {
            StopSprint();
        }
    }

    private void TurnFlashlight()
    {
        // Implement flashlight logic
    }

    private void StartSprint()
    {
        isSprinting = true;
        currentSpeed = Mathf.MoveTowards(currentSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
    }

    private void StopSprint()
    {
        isSprinting = false;
        currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed, stoppingSpeed * Time.deltaTime);
    }

}
