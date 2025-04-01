using UnityEngine;
using System;

[System.Serializable]
public class Chest
{
    public ChestType chestType;
    private IChestState currentState;
    public float remainingTime;
    public int slotIndex;

    
    public event Action OnStateChanged;

    public Chest(ChestType type, int slot)
    {
        chestType = type;
        slotIndex = slot;
        SetState(new LockedState());
    }

    public void SetState(IChestState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
        OnStateChanged?.Invoke(); 
    }

    public void Update()
    {
        currentState.UpdateState(this);
    }

    public void StartTimer()
    {
        currentState.OnStartTimer(this);
    }

    public void UnlockWithGems()
    {
        currentState.OnUnlockWithGems(this);
    }

    public (int coins, int gems) GetRewards()
    {
        int coins = UnityEngine.Random.Range(chestType.minCoins, chestType.maxCoins + 1);
        int gems = UnityEngine.Random.Range(chestType.minGems, chestType.maxGems + 1);
        return (coins, gems);
    }
}