using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int poolSize = 60;

    private readonly List<Bullet> bullets = new();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, transform);

            bullet.gameObject.SetActive(false);

            bullets.Add(bullet);
        }
    }

    public Bullet GetBullet()
    {
        foreach (Bullet bullet in bullets)
        {
            if (!bullet.gameObject.activeInHierarchy)
                return bullet;
        }

        return null;
    }
}