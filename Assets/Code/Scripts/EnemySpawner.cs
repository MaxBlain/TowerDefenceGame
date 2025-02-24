using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static float timeSinceLastSpawn = 0;

    public static bool TrySpawn(int enemiesLeftToSpawn, float enemiesPerSecond)
    {
        // Update the spawn timer cooldown
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            timeSinceLastSpawn = 0f;
            return true;
        }

        return false;
    }

    // Create a new enemy
    public static GameObject SpawnEnemy(GameObject prefabToSpawn, Vector2Int position)
    {
        return Instantiate(prefabToSpawn, new Vector3(position.x, position.y, 0f), Quaternion.identity);
    }
}
