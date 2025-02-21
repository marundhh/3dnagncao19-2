using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;

    int isJoggingHash;
    int isRunningHash;

    PlayerInput inputs;

    Vector2 currentMovement;
    bool movePressed;
    bool runPressed;

    [Header("Jump Variables")]
    [SerializeField] private float jumpHeight = 1.5f;
    public float gravity;
    [SerializeField] private float walkingGravity = -0.6f;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 10f; // Tốc độ xoay

    [Header("Camera Reference")]
    [SerializeField] private Transform mainCamera;

    public bool isSwimming;
    public bool isUnderWater;
    public float swimGravity = -0.8f;

    private void Awake()
    {
        inputs = new PlayerInput();

        inputs.Player.Move.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movePressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        inputs.Player.Sprint.performed += ctx => runPressed = ctx.ReadValueAsButton();

        // nút nhảy
        inputs.Player.Jump.performed += ctx => Jump();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        isJoggingHash = Animator.StringToHash("isJog");
        isRunningHash = Animator.StringToHash("isRun");

        // Nếu chưa gán camera từ Inspector
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;

        // Áp dụng trọng lực khi ở mặt đất
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Giữ nhân vật sát mặt đất
        }

        // Áp dụng trọng lực
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        Movement();
        RotateMove();
    }

    void RotateMove()
    {
        if (movePressed)
        {
            // Sử dụng mainCamera thay vì Camera.main
            Vector3 moveDirection = mainCamera.right * currentMovement.x +
                                    mainCamera.forward * currentMovement.y;

            // Giữ nguyên vector y của playerVelocity để không ảnh hưởng đến gravity
            Vector3 horizontalMovement = new Vector3(moveDirection.x, 0, moveDirection.z);
            horizontalMovement.Normalize();

            // Di chuyển nhân vật theo mặt phẳng ngang, KHÔNG di chuyển theo trục y
            Vector3 movement = horizontalMovement * (runPressed ? 5f : 3f) * Time.deltaTime;

            // Tạo vector di chuyển riêng biệt với trọng lực
            Vector3 movementWithoutVertical = new Vector3(movement.x, playerVelocity.y, movement.z);
            characterController.Move(movementWithoutVertical);

            // Xoay mượt mà về phía di chuyển
            if (horizontalMovement != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(horizontalMovement, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void Movement()
    {
        if (isSwimming)
        {
            if (isUnderWater)
            {
                gravity = swimGravity;
            }
            else
            {
                playerVelocity.y = 0;
            }
        }
        else
        {
            gravity = walkingGravity;
        }

        bool isJogging = animator.GetBool(isJoggingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (movePressed && !isJogging)
        {
            animator.SetBool(isJoggingHash, true);
        }

        if (!movePressed && isJogging)
        {
            animator.SetBool(isJoggingHash, false);
        }

        if ((movePressed && runPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        if ((!movePressed || !runPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void Jump()
    {
        // Chỉ cho phép nhảy khi ở trên mặt đất
        if (isGrounded)
        {
            // Tính toán vận tốc nhảy
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    private void OnEnable()
    {
        inputs.Player.Enable();
    }

    private void OnDisable()
    {
        inputs.Player.Disable();
    }
}