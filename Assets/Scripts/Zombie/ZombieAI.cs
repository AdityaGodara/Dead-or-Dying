using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ZombieStats stats;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;

    private NavMeshAgent agent;

    private int currentHealth;
    private float currentSpeed;
    private float totalSlowPercent;
    private bool isAttacking;
    private bool isDead;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentHealth = stats.maxHealth;
        currentSpeed = stats.moveSpeed;

        agent.speed = currentSpeed;
        agent.stoppingDistance = stats.attackRange;
    }

    private void Update()
    {
        if (isDead || player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= stats.attackRange)
        {
            agent.isStopped = true;

            if (!isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }
        else
        {
            agent.isStopped = false;
            //agent.SetDestination(player.position);
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        int attackIndex = Random.Range(0, 2);

        animator.SetInteger("AttackIndex", attackIndex);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(stats.attackCooldown);

        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        ApplySlow();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void ApplySlow()
    {
        if (totalSlowPercent >= stats.maxSlowPercent)
            return;

        totalSlowPercent += stats.slowPerHit;
        totalSlowPercent = Mathf.Min(totalSlowPercent, stats.maxSlowPercent);

        currentSpeed = stats.moveSpeed * (1f - totalSlowPercent);

        agent.speed = currentSpeed;
    }

    private void Die()
    {
        isDead = true;

        agent.isStopped = true;
        agent.enabled = false;

        animator.SetTrigger("Die");

        Destroy(gameObject, 3f);
    }
}