using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Animator animator;

    [Header("Weapon")]
    [SerializeField] private float fireRate = 10f;

    private PlayerInputAction inputActions;

    private float nextFireTime;

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
        Shoot();
    }

    private void Shoot()
    {
        if (!inputActions.Player.Fire.IsPressed())
            return;

        if (Time.time < nextFireTime)
            return;

        Bullet bullet = bulletPool.GetBullet();

        if (bullet == null)
            return;

        bullet.Initialize(
            firePoint.position,
            firePoint.rotation);

        nextFireTime = Time.time + (1f / fireRate);
        animator.SetTrigger("Shoot");
    }
}