using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private List<GameObject> enemyInstances;
    private List<Vector2Int> routeCells;

    private int currentWave = 1;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Start()
    {
        StartCoroutine(StartWave());
        enemyInstances = new List<GameObject>();
    }

    private void Update()
    {
        if (isSpawning)
        {
            // Spawn enemy on cooldown
            if (EnemySpawner.TrySpawn(enemiesLeftToSpawn, enemiesPerSecond))
            {
                enemiesLeftToSpawn--;
                enemiesAlive++;
                enemyInstances.Add(EnemySpawner.SpawnEnemy(enemyPrefabs[0], routeCells[0]));
            }

            // End the wave when all enemies have been defeated
            if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
            {
                EndWave();
            }
        }
        else
        {
            return;
        }
    }

    public void SetPathRoute(List<Vector2Int> routeCells)
    {
        this.routeCells = routeCells;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;

        // Update the amount of enemies spawned per wave
        enemiesLeftToSpawn = EnemiesPerWave();

        // Limits the enemies per second
        if (enemiesPerSecond < 2.5)
        {
            enemiesPerSecond = EnemiesPerSecond();
        }
    }

    // Reset wave values
    public void EndWave()
    {
        isSpawning = false;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    // Update enemies per wave with scaling factor
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Update enemies per second with scaling factor
    private float EnemiesPerSecond()
    {
        return enemiesPerSecond * Mathf.Pow(currentWave, (difficultyScalingFactor * 0.2f));
    }
}
