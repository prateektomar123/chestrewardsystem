using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    private int coins;
    private int gems;

    
    public event Action OnPlayerDataChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        coins = 50;
        gems = 10; 
        NotifyDataChanged();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log($"Added {amount} coins. Total: {coins}");
        NotifyDataChanged();
    }

    public void AddGems(int amount)
    {
        gems += amount;
        Debug.Log($"Added {amount} gems. Total: {gems}");
        NotifyDataChanged();
    }

    public bool SpendGems(int amount)
    {
        if (gems >= amount)
        {
            gems -= amount;
            Debug.Log($"Spent {amount} gems. Remaining: {gems}");
            NotifyDataChanged();
            return true;
        }
        return false;
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetGems()
    {
        return gems;
    }

    private void NotifyDataChanged()
    {
        OnPlayerDataChanged?.Invoke();
    }
}