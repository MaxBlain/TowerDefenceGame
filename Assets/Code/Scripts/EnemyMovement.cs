using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private List<Vector2Int> routeCells;
    private Vector2 targetPos;
    private int pathIndex = 1;

    private float baseSpeed;

    private void Start()
    {
        routeCells = GridManager.route;
        targetPos = routeCells[0];
        baseSpeed = moveSpeed;
    }

    private void Update()
    {
        if (routeCells != null)
        {
            if (Vector2.Distance(rb.position, targetPos) < 0.05f)
            {
                targetPos = routeCells[pathIndex];
                pathIndex++;
                if (pathIndex > routeCells.Count - 1)
                {
                    WaveManager.onEnemyDestroy.Invoke();
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (routeCells != null)
        {
            Vector2 direction = (targetPos - rb.position).normalized;
            rb.linearVelocity = moveSpeed * direction;
        }
    }

    // Update speed due to slows or effects
    public void UpdateSpeed(float newSpeedFactor)
    {
        moveSpeed *= newSpeedFactor;
    }

    // Reset speed after slows or effects
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
