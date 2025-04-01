using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coins;
    public int gems;
    public PlayerData()
    {
        coins = 0;
        gems = 100;
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log($"Added {amount} coins. Total: {coins}");
    }
    public void AddGems(int amount)
    {
        gems += amount;
        Debug.Log($"Added {amount} gems. Total: {gems}");
    }
    public bool SpendGems(int amount)
    {
        if (gems >= amount)
        {
            gems -= amount;
            Debug.Log($"Spent {amount} gems. Remaining: {gems}");
            return true;
        }
        Debug.Log("Not enough gems!");
        return false;
    }
}