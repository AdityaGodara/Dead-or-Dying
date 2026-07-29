using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerAim playerAim;

    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 12f;
    [SerializeField] private float animationDampTime = 0.1f;

    private CharacterController controller;
    private PlayerInputAction inputActions;

    private Vector2 moveInput;
    private Vector3 moveDirection;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        //animator.SetLayerWeight(1, 0f);

        inputActions = new PlayerInputAction();

        inputActions.Player.Move.performed += ctx =>
        {
            moveInput = ctx.ReadValue<Vector2>();
        };

        inputActions.Player.Move.canceled += ctx =>
        {
            moveInput = Vector2.zero;
        };
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        MovePlayer();

        if (!playerAim.IsAiming)
        {
            RotateTowardsMovement();
        }

        UpdateAnimation();
    }

    private void MovePlayer()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        moveDirection = forward * moveInput.y +
                        right * moveInput.x;

        moveDirection.Normalize();

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void RotateTowardsMovement()
    {
        if (moveDirection.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void UpdateAnimation()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveDirection);

        animator.SetFloat(
            "MoveX",
            localMove.x,
            animationDampTime,
            Time.deltaTime);

        animator.SetFloat(
            "MoveY",
            localMove.z,
            animationDampTime,
            Time.deltaTime);

        animator.SetFloat(
            "Speed",
            moveInput.magnitude,
            animationDampTime,
            Time.deltaTime);

        //Debug.Log($"MoveX: {localMove.x}  MoveY: {localMove.z}");
    }
}