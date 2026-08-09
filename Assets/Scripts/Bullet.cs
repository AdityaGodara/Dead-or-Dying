using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private int damage = 1;

    private TrailRenderer trail;
    private float timer;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);

        timer = lifeTime;

        trail.Clear();

        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer -= Time.deltaTime;

        if (timer <= 0f)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if we hit a zombie
        ZombieAI zombie = other.GetComponentInParent<ZombieAI>();

        if (zombie != null)
        {
            zombie.TakeDamage(damage);
        }

        // Return bullet to pool
        gameObject.SetActive(false);
    }
}