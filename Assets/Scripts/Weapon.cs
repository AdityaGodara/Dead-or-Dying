using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerAim playerAim;

    [Header("Weapon")]
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float reloadTime = 1.8f;
    [SerializeField] private AmmoUI ammoUI;

    private int currentAmmo;
    private bool isReloading;

    private PlayerInputAction inputActions;

    private float nextFireTime;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        currentAmmo = magazineSize;
        inputActions.Player.Reload.performed += _ =>
        {
            TryReload();
        };
        UpdateAmmoUI();
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
        // Must be aiming first
        if (!playerAim.IsAiming)
            return;

        Shoot();
    }

    private void Shoot()
    {
        // Must press Fire (Left Mouse)
        if (!inputActions.Player.Fire.IsPressed())
            return;

        if (isReloading)
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
        currentAmmo--;
        if (currentAmmo <= 0)
        {
            TryReload();
        }
        UpdateAmmoUI();

        animator.SetLayerWeight(1, 1f);
        animator.SetTrigger("Shoot");

    }

    private void UpdateAmmoUI()
    {
        if (ammoUI != null)
        {
            ammoUI.UpdateAmmo(currentAmmo, magazineSize);
        }
    }

    private void TryReload()
    {
        if (isReloading)
            return;

        if (currentAmmo == magazineSize)
            return;

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        animator.SetTrigger("Reload");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;

        isReloading = false;
        UpdateAmmoUI();
    }

    public void FinishReload()
    {
        currentAmmo = magazineSize;
        UpdateAmmoUI();
    }

    public void EndReload()
    {
        isReloading = false;
    }

    public void DisableUpperBodyLayer()
    {
        animator.SetLayerWeight(1, 0f);
    }
}