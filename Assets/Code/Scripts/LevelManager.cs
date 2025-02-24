using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public int currency;

    private void Awake()
    {
        main = this;
    }

    // Starting variables and call to generate a starting map
    private void Start()
    {
        currency = 20;
    }

    // Increase currency
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    // Spend currency
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
