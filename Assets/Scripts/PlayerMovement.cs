using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float Speed = 6.0f;
    [SerializeField] float crouchSpeed = 3.0f; // Speed while crouching
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] float crouchHeight = 1.0f; // Height when crouching
    [SerializeField] float standingHeight = 2.0f; // Height when standing
    [SerializeField] float sprintSpeed = 18f; // Speed while sprinting
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;


    public float jumpHeight = 6f;
    float velocityY;
    bool isGrounded;
    bool isCrouching = false;

    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();
        UpdateCrouch();
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        // Reset vertical velocity when grounded
        if (isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        // Jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocityY += gravity * Time.deltaTime;

        // Movement input
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        // Speed logic
        float currentSpeed = Speed;

        if (isCrouching)
            currentSpeed = crouchSpeed;
        else if (Input.GetKey(sprintKey))
            currentSpeed = sprintSpeed;

        // Final move vector
        Vector3 move = (transform.right * currentDir.x + transform.forward * currentDir.y) * currentSpeed;
        move.y = velocityY;

        // Apply movement
        controller.Move(move * Time.deltaTime);
    }



    void UpdateCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
            controller.height = crouchHeight; // Reduce height
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            controller.height = standingHeight; // Reset to standing height
        }
    }
}
