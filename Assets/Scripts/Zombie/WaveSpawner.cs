using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ZombieEntry
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float spawnChance = 1f;
    }

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Zombies")]
    [SerializeField] private ZombieEntry[] zombieTypes;

    [Header("Wave Settings")]
    [SerializeField] private int startWaveSize = 5;
    [SerializeField] private int waveIncrease = 2;
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWave = 1;
    private int aliveZombies;

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            int zombiesToSpawn = startWaveSize + (currentWave - 1) * waveIncrease;

            Debug.Log($"Starting Wave {currentWave} - Spawning {zombiesToSpawn} zombies");

            for (int i = 0; i < zombiesToSpawn; i++)
            {
                SpawnZombie();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            while (aliveZombies > 0)
            {
                yield return null;
            }

            Debug.Log($"Wave {currentWave} complete!");

            currentWave++;

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void SpawnZombie()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        Debug.Log($"Chosen Spawn Point: {spawnPoint.name} at {spawnPoint.position}");

        Vector3 spawnPos = spawnPoint.position;

        GameObject zombiePrefab = ChooseZombie();

        Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
    }

    private GameObject ChooseZombie()
    {
        float total = 0f;

        foreach (var z in zombieTypes)
            total += z.spawnChance;

        float roll = Random.Range(0f, total);

        float current = 0f;

        foreach (var z in zombieTypes)
        {
            current += z.spawnChance;

            if (roll <= current)
                return z.prefab;
        }

        return zombieTypes[0].prefab;
    }

    public void NotifyZombieDied()
    {
        aliveZombies--;
    }
}