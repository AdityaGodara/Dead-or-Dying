using UnityEngine;

[CreateAssetMenu(menuName = "Game/Zombie Stats")]
public class ZombieStats : ScriptableObject
{
    public string zombieName;
    public int maxHealth = 3;
    public float moveSpeed = 2.5f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.2f;
    public float slowPerHit = 0.1f;
    public float maxSlowPercent = 0.5f;
}