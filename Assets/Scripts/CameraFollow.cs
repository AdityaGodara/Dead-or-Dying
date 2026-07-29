using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;

    [Header("Follow")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSmoothTime = 0.15f;

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            followSmoothTime);
    }
}