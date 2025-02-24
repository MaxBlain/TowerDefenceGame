using UnityEngine;
using UnityEditor;
using Unity.Mathematics;

public class TurretProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float fireRate = 1f;

    private Transform target;
    private float timeUntilFire;
    private bool aimingAtEnemy;

    private void Update()
    {
        // Update shooting cooldown
        timeUntilFire += Time.deltaTime;

        // Check there is a valid target
        if (target == null)
        {
            FindTarget();
            return;
        }

        aimingAtEnemy = false;
        // Rotate towards that target
        RotateTowardsTarget();

        // Check the target is in range
        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            // Fire if not on cooldown
            if (timeUntilFire >= 1f / fireRate && aimingAtEnemy == true)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    // Create and shoot bullet
    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    // Search for the closest enemy and target it
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask); 
        if (hits.Length > 0)
        {
            target = hits[0].transform;
            for (int i = 0; i < hits.Length - 1; i++)
            {
                float oldDistance = Vector3.Distance(transform.position, hits[i].transform.position);
                float newDistance = Vector3.Distance(transform.position, hits[i + 1].transform.position);
                if (newDistance < oldDistance)
                {
                    target = hits[i + 1].transform;
                }
            }
        }
    }

    // Checks the target is within the turret range
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    // Rotates to turret to point at the enemy
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        // Allow the turret to shoot if the rotation is correct
        if (turretRotationPoint.rotation == targetRotation)
        {
            aimingAtEnemy = true;
        }
    }
}