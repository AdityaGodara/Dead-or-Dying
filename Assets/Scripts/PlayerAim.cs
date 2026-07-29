using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;

    [Header("Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rotationSpeed = 15f;

    private PlayerInputAction inputActions;
    private bool isAiming;
    public bool IsAiming => isAiming;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        isAiming = inputActions.Player.AimMode.IsPressed();

        animator.SetBool("IsAiming", isAiming);

        if (isAiming)
        {
            RotateTowardsMouse();
        }
    }

    private void RotateTowardsMouse()
    {
        Vector2 mousePosition = inputActions.Player.Aim.ReadValue<Vector2>();

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }
    }
}