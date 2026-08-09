using UnityEngine;

public class ZombieHealthNotifier : MonoBehaviour
{
    private WaveSpawner spawner;

    public void Initialize(WaveSpawner waveSpawner)
    {
        spawner = waveSpawner;
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.NotifyZombieDied();
        }
    }
}