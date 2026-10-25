using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Movement : MonoBehaviour
{
    private const float SPEED_MULTIPLIER = 3f;
    private const float SPEED_DURATION = 2.0f;

    InputAction moveAction;
    Vector2 lookInput;
    Vector2 moveInput;
    float verticalVelocity;
    float gravity = -9.81f;

    [SerializeField]
    CharacterController controller;

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float lookSensitivity = 2f;

    float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        fwd.y = 0;
        right.y = 0;

        fwd.Normalize();
        right.Normalize();

        // left-right movement
        Vector3 movement = fwd * moveInput.y + right * moveInput.x;

        movement *= (float) (6f * 1f);

        // vertical movement
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        movement.y = verticalVelocity;

        controller.Move(movement * Time.deltaTime);


        // mouse looking
        float mouseX = lookInput.x * lookSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * lookSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(0.75f * -2f * gravity);
        }
    }

    public void ResetVertical() { verticalVelocity = 0f; }
}
