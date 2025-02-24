using System.Collections;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretSlowmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 1.5f;
    [SerializeField] private float fireRate = 20f;
    [SerializeField] private float freezeTime = 0.049f;

    private float timeUntilFire;

    private void Update()
    {
        // Update shooting cooldown
        timeUntilFire += Time.deltaTime;

        // Fire if not on cooldown
        if (timeUntilFire >= 1f / fireRate)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    // Slow enemies in range
    private void FreezeEnemies()
    {
        // Search for enemies inside tower range
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(targetingRange*2, targetingRange*2), 0f, (Vector2)transform.position, 0f, enemyMask);

        // For each enemy hit set their new move speed
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                // Set new enemy speed
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    // Reset the speed after a number of seconds
    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }
}
