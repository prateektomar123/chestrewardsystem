using System;
using UnityEngine;

[System.Serializable]
public class Chest
{
    public ChestType chestType;
    private IChestState currentState;
    public float remainingTime;
    public int slotIndex;
    public event Action OnStateChanged;
    private IChestState previousState;
    private float previousRemainingTime;

    public Chest(ChestType type, int slot)
    {
        chestType = type;
        slotIndex = slot;
        SetState(new LockedState());
    }

    public void SetState(IChestState newState)
    {
        
        previousState = currentState;
        previousRemainingTime = remainingTime;

        currentState = newState;
        currentState.EnterState(this);
        OnStateChanged?.Invoke();
    }

    public void Update()
    {
        Debug.Log($"Chest: Updating state for slot {slotIndex}");
        currentState.UpdateState(this);
        OnStateChanged?.Invoke();
    }

    public void StartTimer()
    {
        Debug.Log($"Chest: Starting timer for slot {slotIndex}");
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

    public int CalculateGemCost()
    {
        if (remainingTime <= 0) return 0;

        float remainingMinutes = remainingTime / 60f;
        int gemCost = Mathf.CeilToInt(remainingMinutes);
        return gemCost;
    }

    public void UndoUnlockWithGems()
    {
        if (previousState != null)
        {
            remainingTime = previousRemainingTime;
            currentState = previousState;
            currentState.EnterState(this);
            OnStateChanged?.Invoke();
        }
    }
}