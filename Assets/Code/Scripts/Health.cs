using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 10;
    [SerializeField] private int currencyWorth = 1;

    private bool isDestoryed = false;

    // Update hit points and destroy the enemy if 0
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0 && !isDestoryed)
        {
            isDestoryed = true;
            LevelManager.main.IncreaseCurrency(currencyWorth);
            WaveManager.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
